using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (NG_CharacterController2D))]
public class NG_Player : MonoBehaviour
{
    public float maxJumpHeight = 4;
    public float minJumpHeight = 1;
    public float timeToJumpApex = 0.4f;
    [SerializeField] float moveSpeed = 6;

    [SerializeField] Vector2 wallJumpClimb;
    [SerializeField] Vector2 wallJumpOff;
    [SerializeField] Vector2 wallLeap;
    [SerializeField] float wallSlideSpeedMax = 3;
    [SerializeField] float wallStickTime = 0.25f;
    float timeToWallUnstick;
    [SerializeField] float wallTimer = 2.0f;

    [SerializeField] float accelerationTimeAirborne = 0.2f;
    float accelerationTimeGrounded; //Might be able to change this when on ice platforms to make them more slippery. -NG
    [SerializeField] float accelerationTimeSureFeet = 0.1f;
    [SerializeField] float accelerationTimeIce = 0.3f;

    float gravity;
    float maxJumpVelocity;
    float minJumpVelocity;
    Vector3 velocity;
    float velocityXSmoothing;

    KA_JumpPickup jumpyPickup;

    //Projectile stuff
    [SerializeField] public GameObject curBall;
    [SerializeField] public GameObject snowBall;
    [SerializeField] public GameObject iceBall;
    [SerializeField] public GameObject rockBall;
    [SerializeField] public GameObject shotgun;
    [SerializeField] public GameObject explosion;
    [SerializeField] public GameObject grower;
    [SerializeField] public GameObject boomerang;
    [SerializeField] public GameObject healthpickupProj;
    [SerializeField] public GameObject iceLaunchBall;
    [SerializeField] private float curBallCost;
    [SerializeField] private Transform throwPoint;
    [SerializeField] private NG_StatManager Nrg;
    [SerializeField] private float count;
    [SerializeField] private bool canShoot = true;

    [SerializeField] private GameObject forcePush;
    [SerializeField] private float maxForce;

    private NG_CharacterController2D controller2D;
    [SerializeField] private NG_UpgradeMenu Upgrades;
    [SerializeField] private NG_UpgradeProgress upgradeProgress;
    [SerializeField] private NG_IceLaunch iceLaunch;
    private Rigidbody2D rb;
    public SoundManager sounds;
    private Animator animator;

    [SerializeField] private Transform playerGraphics;  //Reference to the player's visual graphics so we can change its direction instead of the whole player. -NG
    Vector3 graphicScale;
    [SerializeField] Transform arm;

    Vector2 directionalInput;
    bool wallSliding;
    int wallDirX;

    public float MoveSpeed
    { get { return moveSpeed; } set { moveSpeed = value; } }

    public Rigidbody2D RB
    { get { return rb; } set { rb = value; } }

    public Transform PlayerGraphics
    { get { return playerGraphics; } set { playerGraphics = value; } }

    public float CurBallCost
    { get { return curBallCost; } set { curBallCost = value; } }

    public bool CanShoot
    { get { return canShoot; } set { canShoot = value; } }

    public NG_IceLaunch IceLaunch
    { get { return iceLaunch; } set { iceLaunch = value; } }

    public Animator Animator
    { get { return animator; } set { animator = value; } }

    // Use this for initialization
    void Start()
    {
        //Finding Scripts
        controller2D = GetComponent<NG_CharacterController2D>();
        Nrg = FindObjectOfType<NG_StatManager>();
        upgradeProgress = FindObjectOfType<NG_UpgradeProgress>();
        animator = GetComponent<Animator>();

        //Finding components/Children objects
        rb = GetComponent<Rigidbody2D>();
        playerGraphics = gameObject.transform.Find("Graphics");
        if (playerGraphics == null)
            Debug.Log("No object 'Graphics' can be found as a child of the Player.");
        if (playerGraphics != null)
            graphicScale = playerGraphics.transform.localScale;
        arm = GetComponentInChildren<NG_ThrowArm>().transform;

        curBall = snowBall;
        canShoot = true;

        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
        print("Gravity: " + gravity + " Jump Velocity: " + maxJumpVelocity);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CalculateVelocity();
        HandleWallSliding();

        //Flipping the graphics child object -NG
        /*if (controller2D.collisions.faceDir < 0f)
            playerGraphics.localScale = new Vector3(-graphicScale.x, graphicScale.y, graphicScale.z);
        else if (controller2D.collisions.faceDir > 0f)
            playerGraphics.localScale = new Vector3(graphicScale.x, graphicScale.y, graphicScale.z);*/
            

        //Changing how fast the player can change direction based on whether we are on ice or not. -NG
        if (controller2D.IceCheck)
            accelerationTimeGrounded = accelerationTimeIce;
        else
            accelerationTimeGrounded = accelerationTimeSureFeet;

        if (!wallSliding)
        {
            animator.SetBool("WallSlide", false);
            if (arm.eulerAngles.z <= 90f || arm.eulerAngles.z >= 270f)
            {
                playerGraphics.localScale = new Vector3(graphicScale.x, graphicScale.y, graphicScale.z);
                arm.GetChild(0).GetComponent<SpriteRenderer>().flipY = false;
            }
            if (arm.eulerAngles.z >= 90f && arm.eulerAngles.z <= 270f)
            {
                playerGraphics.localScale = new Vector3(-graphicScale.x, graphicScale.y, graphicScale.z);
                arm.GetChild(0).GetComponent<SpriteRenderer>().flipY = true;
            }
        }
        else
            animator.SetBool("WallSlide", true);

        //Stunning the player when knocked back by enemy -NG
        if (controller2D.KnockBackTimer <= 0)
        {
            //Ice Launch stuff
            if(iceLaunch != null)
            {
                if (iceLaunch.Launching)
                {
                    Debug.Log("Launching the player");
                    if (iceLaunch.LaunchRight == true)
                        controller2D.Move(iceLaunch.LaunchPower * Time.deltaTime, new Vector2(iceLaunch.LaunchPower.x, iceLaunch.LaunchPower.y));
                    else if (iceLaunch.LaunchRight == false)
                    {
                        controller2D.Move(new Vector2(-iceLaunch.LaunchPower.x, iceLaunch.LaunchPower.y) * Time.deltaTime, new Vector2(-iceLaunch.LaunchPower.x, iceLaunch.LaunchPower.y));
                        //velocity.x += (-iceLaunch.LaunchPower.x * Time.deltaTime);
                        //velocity.y += (iceLaunch.LaunchPower.y * Time.deltaTime);
                    }

                }
                else
                {
                    rb.velocity = Vector2.zero;
                    controller2D.Move(velocity * Time.deltaTime, directionalInput);
                }
            }
            else
            {
                rb.velocity = Vector2.zero;
                controller2D.Move(velocity * Time.deltaTime, directionalInput);
            }
        }
        else
        {
            animator.SetTrigger("Hurt");
            if (controller2D.KnockedFromRight)
                controller2D.Move(new Vector2((-controller2D.KnockBack)/10, (controller2D.KnockBack/3)/10), false);
            if (!controller2D.KnockedFromRight)
                controller2D.Move(new Vector2((controller2D.KnockBack) / 10, (controller2D.KnockBack / 3) / 10), false);
            controller2D.KnockBackTimer -= Time.deltaTime;
        }

        if (controller2D.collisions.above || controller2D.collisions.below)
        {
            if (controller2D.collisions.slidingDownMaxSlope)
                velocity.y += controller2D.collisions.slopeNormal.y * -gravity * Time.deltaTime;
            else
            {
                rb.velocity = Vector2.zero;
                velocity.y = 0;
            }
        }

        CanThePlayerShoot();
    }

    public void SetDirectionalInput(Vector2 input)
    {
        directionalInput = input;
    }

    public void OnJumpInputDown()
    {
        animator.SetBool("Grounded", false);
        controller2D.OnFallingPlat = false;
        if (wallSliding)
        {
            if (wallDirX == directionalInput.x)
            {
                velocity.x = -wallDirX * wallJumpClimb.x;
                velocity.y = wallJumpClimb.y;
            }
            else if (directionalInput.x == 0)
            {
                velocity.x = -wallDirX * wallJumpOff.x;
                velocity.y = wallJumpOff.y;
            }
            else
            {
                velocity.x = -wallDirX * wallLeap.x;
                velocity.y = wallLeap.y;
            }
        }
        if (controller2D.collisions.below || this.GetComponentInChildren<KA_JumpPickup>() != null || controller2D.jumpNum == 1)
        {
            if (controller2D.collisions.slidingDownMaxSlope)
            {
                if(directionalInput.x != -Mathf.Sign(controller2D.collisions.slopeNormal.x)) //not jumping against max slope
                {
                    velocity.y = maxJumpVelocity * controller2D.collisions.slopeNormal.y;
                    velocity.x = maxJumpVelocity * controller2D.collisions.slopeNormal.x;
                }
            }
            else
                velocity.y = maxJumpVelocity;
        }
    }

    public void OnJumpInputUp()
    {
        controller2D.jumpNum = 0;
        if (velocity.y > minJumpVelocity)
            velocity.y = minJumpVelocity;
    }

    void HandleWallSliding()
    {
        wallDirX = (controller2D.collisions.left) ? -1 : 1;
        wallSliding = false;
        if ((controller2D.collisions.left || controller2D.collisions.right) && !controller2D.collisions.below && velocity.y < 0)
        {
            wallSliding = true;

            if (velocity.y < -wallSlideSpeedMax)
                velocity.y = -wallSlideSpeedMax;

            if (timeToWallUnstick > 0)
            {
                velocityXSmoothing = 0;
                velocity.x = 0;

                if (directionalInput.x != wallDirX && directionalInput.x != 0)
                    timeToWallUnstick -= Time.deltaTime;
                else
                    timeToWallUnstick = wallStickTime;
            }
            else
                timeToWallUnstick = wallStickTime;
        }
    }

    void CalculateVelocity()
    {
        float targetVelocityX = directionalInput.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller2D.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne); //Smoothing out a change in direction. -NG
        velocity.y += gravity * Time.deltaTime;

        animator.SetFloat("Speed", Mathf.Abs(velocity.x));

        //TornadoPlat stuff
        if (controller2D.InEyeOfStorm)
            velocity.y += -(gravity / 0.99f) * Time.deltaTime;
        if (controller2D.OnFallingPlat)
            velocity.y += controller2D.FallingPlat.GetComponent<Rigidbody2D>().velocity.y;
    }

    void CanThePlayerShoot()
    {
        count++;

        if (count >= 26)
        {
            count = 0;
        }
        if (count == 25)
        {
            Nrg.Energy.CurrentVal++;
        }

        if (Nrg.Energy.CurrentVal <= 0)
        {
            canShoot = false;
        }

        if (Nrg.Energy.CurrentVal >= curBallCost || Nrg.Energy.CurrentVal == Nrg.Energy.MaxVal)
        {
            canShoot = true;
        }
    }
}
