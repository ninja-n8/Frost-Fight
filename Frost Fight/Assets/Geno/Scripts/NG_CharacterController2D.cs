using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NG_CharacterController2D : NG_RaycastController
{
    [SerializeField] float maxSlopeAngle;
    [SerializeField] float maxNormalSlopeAngle = 80.0f;
    [SerializeField] float maxIceSlopeAngle = 25.0f;

    [SerializeField] private bool groundPositive;
    [SerializeField] bool iceCheck;
    [SerializeField] private bool waterCheck = false;
    [SerializeField] bool inEyeOfStorm;
    [SerializeField] bool onFallingPlat;
    [SerializeField] GameObject fallingPlat;
    public int jumpNum = 1;

    //PlayerController.cs knockback variables -NG
    [SerializeField] private float knockback;
    [SerializeField] private float knockbackDistance;
    [SerializeField] private float knockbackTimer;
    [SerializeField] private bool knockedFromRight;

    public CollisionInfo collisions;
    [HideInInspector] public Vector2 playerInput;

    [SerializeField] List<string> contactTags = new List<string>();

    private NG_Player playerScript; //Getting reference to the player input script so I can make the player slip on ice. -NG
    private NG_StatManager playerStats;

    public bool GroundPositive
    { get { return groundPositive; } set { groundPositive = value; } }
    public bool IceCheck
    { get { return iceCheck; } set { iceCheck = value; } }
    public bool InEyeOfStorm
    { get { return inEyeOfStorm; } set { inEyeOfStorm = value; } }
    public bool OnFallingPlat
    { get { return onFallingPlat; } set { onFallingPlat = value; } }
    public GameObject FallingPlat
    { get { return fallingPlat; } set { fallingPlat = value; } }

    //knockback getters and setters*************************************
    public float KnockBack
    { get { return knockback; } set { knockback = value; } }
    public float KnockBackDistance
    { get { return knockbackDistance; } set { knockbackDistance = value; } }
    public float KnockBackTimer
    { get { return knockbackTimer; } set { knockbackTimer = value; } }
    public bool KnockedFromRight
    { get { return knockedFromRight; } set { knockedFromRight = value; } }
    //*******************************************************************

    public List<string> ContactTags
    { get { return contactTags; } set { contactTags = value; } }

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        collisions.faceDir = 1;
        playerScript = GetComponent<NG_Player>(); //Getting reference to the player input script so I can make the player slip on ice. -NG
        playerStats = GetComponent<NG_StatManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (iceCheck)
            maxSlopeAngle = maxIceSlopeAngle;
        else
            maxSlopeAngle = maxNormalSlopeAngle;

        if (contactTags.Count >= 2)
            playerStats.FallPillarDamage();

        if (!collisions.below)
            playerScript.Animator.SetBool("Grounded", false);
        else
            playerScript.Animator.SetBool("Grounded", true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "IceLauncher" || collision.gameObject.name == "IceLauncher(Clone)")
        {
            playerScript.IceLaunch = collision.GetComponent<NG_IceLaunch>();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 17)
        {
            inEyeOfStorm = true;
            jumpNum = 1;
        }
        else
            inEyeOfStorm = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

    }

    //Old PlayerController.cs code that handled knockback when touching an enemy -NG
    private void OnCollisionEnter2D(Collision2D collision)
    {
        contactTags.Add(collision.gameObject.tag);

        if (collision.gameObject.tag == "Enemy")
        {
            knockbackTimer = knockbackDistance;
            if (transform.position.x < collision.transform.position.x)
                knockedFromRight = true;
            else
                knockedFromRight = false;
        }

        /*if (collision.gameObject.tag == "Boulder")
        {
            Rigidbody2D boulderRB = collision.gameObject.GetComponent<Rigidbody2D>();
            
            if (transform.position.x < collision.transform.position.x)
            {
                boulderRB.AddForce(new Vector2(3500, 0), ForceMode2D.Force);
                Debug.Log("pushing right");
            }
            else
            {
                boulderRB.AddForce(new Vector2(-3500, 0), ForceMode2D.Force);
                Debug.Log("pushing left");
            }
        }*/
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        contactTags.Remove(collision.gameObject.tag);

        if (collision.gameObject.tag == "Boulder")
        {
            Rigidbody2D boulderRB = collision.gameObject.GetComponent<Rigidbody2D>();
            boulderRB.velocity = Vector2.zero;
        }

        if (collision.gameObject.name == "BalanceBeam")
        {
            Rigidbody2D balanceRB = collision.transform.gameObject.GetComponent<Rigidbody2D>();

            if (collisions.below)
                balanceRB.velocity = Vector2.zero;
        }

        if(collision.gameObject.GetComponent<NG_PlatformController>() != null)
            transform.parent = null;

        if (collision.gameObject.layer == 16)
        {
            transform.parent = null;
            onFallingPlat = false;
            fallingPlat = null;
        }
    }

    public void Move(Vector2 moveAmount, bool standingOnPlatform)
    {
        Move(moveAmount, Vector2.zero, standingOnPlatform);
    }

    public void Move(Vector2 moveAmount, Vector2 input, bool standingOnPlatform = false)
    {
        UpdateRaycastOrigins();

        collisions.Reset();
        collisions.moveAmountOld = moveAmount;
        playerInput = input;
        
        if (moveAmount.y < 0)
            DescendSlope(ref moveAmount);

        if (moveAmount.x != 0)
            collisions.faceDir = (int)Mathf.Sign(moveAmount.x);

        HorizontalCollisions(ref moveAmount);

        if(moveAmount.y != 0)
            VerticalCollisions(ref moveAmount);

        transform.Translate(moveAmount);

        if (standingOnPlatform)
            collisions.below = true;
    }

    void HorizontalCollisions(ref Vector2 moveAmount)
    {
        float directionX = collisions.faceDir;
        float rayLength = Mathf.Abs(moveAmount.x) + skinWidth;

        if(Mathf.Abs(moveAmount.x) < skinWidth)
            rayLength = 2 * skinWidth;

        for (int i = 0; i < horizontalRayCount; i++)
        {
            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

            Debug.DrawRay(rayOrigin, Vector2.right * directionX, Color.red);

            if (hit)
            {
                //This was for if the player was inside an object (which would make our rays 0 spaces long)
                //It would make the rays ignore the thing we were inside and move on to the next one, but it
                //made the player go through walls sometimes. -NG
                if (hit.distance == 0)
                    continue;

                //Checking to see if we are touching an ice platform. -NG
                if (hit.transform.gameObject.tag == "Ice")
                    iceCheck = true;
                else
                    iceCheck = false;

                float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);

                if (i == 0 && slopeAngle <= maxSlopeAngle)
                {
                    //Going straight from descending to ascending a slope (or vice versa) sometimes makes the player 
                    //slow down a lot for a moment. This if statement fixes that. -NG
                    if (collisions.descendingSlope)
                    {
                        collisions.descendingSlope = false;
                        moveAmount = collisions.moveAmountOld;
                    }

                    float distanceToSlopeStart = 0;
                    if(slopeAngle != collisions.slopeAngleOld)
                    {
                        distanceToSlopeStart = hit.distance - skinWidth;
                        moveAmount.x -= distanceToSlopeStart * directionX;
                    }
                    ClimbSlope(ref moveAmount, slopeAngle, hit.normal);
                    moveAmount.x += distanceToSlopeStart * directionX;
                }
                if (!collisions.climbingSlope || slopeAngle > maxSlopeAngle)
                {
                    moveAmount.x = (hit.distance - skinWidth) * directionX;
                    rayLength = hit.distance;

                    if (collisions.climbingSlope)
                        moveAmount.y = Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(moveAmount.x);

                    collisions.left = directionX == -1;
                    collisions.right = directionX == 1;
                }

                if(hit.transform.gameObject.tag == "Boulder")
                {
                    Rigidbody2D boulderRB = hit.transform.gameObject.GetComponent<Rigidbody2D>();
                    
                    if(collisions.right)
                        boulderRB.AddForce(new Vector2(20, 0), ForceMode2D.Force);
                    if(collisions.left)
                        boulderRB.AddForce(new Vector2(-20, 0), ForceMode2D.Force);
                }
            }
        }
    }

    void VerticalCollisions(ref Vector2 moveAmount)
    {
        float directionY = Mathf.Sign(moveAmount.y);
        float rayLength = Mathf.Abs(moveAmount.y) + skinWidth;

        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i + moveAmount.x);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);

            Debug.DrawRay(rayOrigin, Vector2.up * directionY, Color.red);

            if (hit)
            {
                //Debug.Log(hit.transform);

                if (collisions.above)
                    Debug.Log("above");

                if (collisions.below)
                    jumpNum = 1;
                if (hit.collider.tag == "Through")
                {
                    if(directionY == 1 || hit.distance == 0)
                        continue;
                    if (collisions.fallingThroughPlatform)
                        continue;
                    if (playerInput.y == -1)
                    {
                        collisions.fallingThroughPlatform = true;
                        Invoke("ResetFallingThroughPlatform", 0.5f);
                        continue;
                    }
                }

                //Checking to see if we are on top of an ice platform. -NG
                if (hit.transform.gameObject.tag == "Ice")
                    iceCheck = true;
                else
                    iceCheck = false;

                moveAmount.y = (hit.distance - skinWidth) * directionY;
                rayLength = hit.distance;

                if (collisions.climbingSlope)
                    moveAmount.x = moveAmount.y / Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Sign(moveAmount.x);

                collisions.below = directionY == -1;
                collisions.above = directionY == 1;

                if (hit.transform.gameObject.name == "BalanceBeam")
                {
                    Rigidbody2D balanceRB = hit.transform.gameObject.GetComponent<Rigidbody2D>();

                    if (collisions.below)
                        balanceRB.AddForceAtPosition(new Vector2(0, -2.5f), transform.position, ForceMode2D.Force);
                }

                if (hit.transform.gameObject.GetComponent<NG_PlatformController>() != null)
                    transform.parent = hit.transform;
                if (hit.transform.gameObject.layer == 16 && collisions.below)
                {
                    //transform.parent = hit.transform;
                    //moveAmount.y = hit.rigidbody.velocity.y;
                    onFallingPlat = true;
                    fallingPlat = hit.transform.gameObject;
                }

                //FallingPillar squish
                /*if(hit.transform.gameObject.layer == 8)
                {
                    RaycastHit2D upAndDown = Physics2D.Raycast(raycastOrigins.topLeft + (Vector2.right * (verticalRaySpacing * i + moveAmount.x)), Vector2.up, rayLength, collisionMask);
                    Debug.DrawRay(raycastOrigins.topLeft + (Vector2.right * (verticalRaySpacing * i + moveAmount.x)), Vector2.up, Color.red);
                    if (upAndDown)
                    {
                        if (upAndDown.transform.gameObject.GetComponent<NG_FallingPillar>() != null)
                            playerStats.Health.CurrentVal = 0;
                    }
                }*/
            }
        }

        if (collisions.climbingSlope)
        {
            float directionX = Mathf.Sign(moveAmount.x);
            rayLength = Mathf.Abs(moveAmount.x) + skinWidth;
            Vector2 rayOrigin = ((directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight) + Vector2.up * moveAmount.y;
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

            if (hit)
            {
                float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
                if (slopeAngle != collisions.slopeAngle)
                {
                    moveAmount.x = (hit.distance - skinWidth) * directionX;
                    collisions.slopeAngle = slopeAngle;
                    collisions.slopeNormal = hit.normal;
                }
            }
        }
    }

    void ClimbSlope(ref Vector2 moveAmount, float slopeAngle, Vector2 slopeNormal)
    {
        float moveDistance = Mathf.Abs(moveAmount.x);
        float climbmoveAmountY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;

        if (moveAmount.y <= climbmoveAmountY)
        {
            moveAmount.y = climbmoveAmountY;
            moveAmount.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(moveAmount.x);
            collisions.below = true;
            collisions.climbingSlope = true;
            collisions.slopeAngle = slopeAngle;
            collisions.slopeNormal = slopeNormal;
        }
    }

    void DescendSlope(ref Vector2 moveAmount)
    {
        RaycastHit2D maxSlopeHitLeft = Physics2D.Raycast(raycastOrigins.bottomLeft, Vector2.down, Mathf.Abs(moveAmount.y) + skinWidth, collisionMask);
        RaycastHit2D maxSlopeHitRight = Physics2D.Raycast(raycastOrigins.bottomRight, Vector2.down, Mathf.Abs(moveAmount.y) + skinWidth, collisionMask);
        if(maxSlopeHitLeft ^ maxSlopeHitRight)
        {
            SlideDownMaxSlope(maxSlopeHitLeft, ref moveAmount);
            SlideDownMaxSlope(maxSlopeHitRight, ref moveAmount);
        }

        if (!collisions.slidingDownMaxSlope)
        {
            float directionX = Mathf.Sign(moveAmount.x);
            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomRight : raycastOrigins.bottomLeft;
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, -Vector2.up, Mathf.Infinity, collisionMask);

            if (hit)
            {
                float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
                if (slopeAngle != 0 && slopeAngle <= maxSlopeAngle)
                {
                    if (Mathf.Sign(hit.normal.x) == directionX)
                    {
                        if (hit.distance - skinWidth <= Mathf.Tan(slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(moveAmount.x))
                        {
                            float moveDistance = Mathf.Abs(moveAmount.x);
                            float descendmoveAmountY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;
                            moveAmount.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(moveAmount.x);
                            moveAmount.y -= descendmoveAmountY;

                            collisions.slopeAngle = slopeAngle;
                            collisions.descendingSlope = true;
                            collisions.below = true;
                            collisions.slopeNormal = hit.normal;
                        }
                    }
                }
            }
        }
    }

    void SlideDownMaxSlope(RaycastHit2D hit, ref Vector2 moveAmount)
    {
        if (hit)
        {
            float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
            if(slopeAngle > maxSlopeAngle)
            {
                moveAmount.x = Mathf.Sign(hit.normal.x) * (Mathf.Abs(moveAmount.y) - hit.distance) / Mathf.Tan(slopeAngle * Mathf.Deg2Rad) / 1.75f;

                collisions.slopeAngle = slopeAngle;
                collisions.slidingDownMaxSlope = true;
                collisions.slopeNormal = hit.normal;
            }
        }
    }

    void ResetFallingThroughPlatform()
    {
        collisions.fallingThroughPlatform = false;
    }

    public struct CollisionInfo
    {
        public bool above, below;
        public bool left, right;

        public bool climbingSlope;
        public bool descendingSlope;
        public bool slidingDownMaxSlope;

        public float slopeAngle, slopeAngleOld;
        public Vector2 slopeNormal;
        public Vector2 moveAmountOld;
        public int faceDir;
        public bool fallingThroughPlatform;

        public void Reset()
        {
            above = below = false;
            left = right = false;
            climbingSlope = false;
            descendingSlope = false;
            slidingDownMaxSlope = false;
            slopeNormal = Vector2.zero;

            slopeAngleOld = slopeAngle;
            slopeAngle = 0;
        }
    }
}
