using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //Original code from Garrett Rickey from month 1803

	[SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float iceSlide = 0.95f;

    //dash variables*****************************************
    [SerializeField] private Vector2 dashSpeed = new Vector2(20,0); 
    [SerializeField] private float dashCoolDown = 2f;
    [SerializeField] private bool canDash = true;
    [SerializeField] private bool isDashing;
    //********************************************************

    //wall jump variables*************************************
    [SerializeField] private float wallCheckDistance = 1.0f;
    [SerializeField] private float wallJumpSpeed = 2.0f;
    [SerializeField] private bool jumpOffWall = false;
    //********************************************************

    [SerializeField] private float launchPower;

    [SerializeField] private float knockback;
    [SerializeField] private float knockbackDistance;
    [SerializeField] private float knockbackTimer;
    [SerializeField] private bool knockedFromRight;

    [SerializeField] private KeyCode left;
    [SerializeField] private KeyCode right;
    [SerializeField] private KeyCode jump;
    [SerializeField] private KeyCode throwSnow;
    [SerializeField] private KeyCode throwSnow2;
    [SerializeField] private KeyCode throwSnow3;
    [SerializeField] private KeyCode dashkey;

	private Rigidbody2D rb;

	public Transform groundCheckPoint;
    private Transform wallCheckPoint;
	public float radiusCheck;
	public LayerMask isGround;
 


    [SerializeField] private bool groundPositive;
    [SerializeField] private bool wallPositive;
    [SerializeField] private bool iceCheck;
    [SerializeField] private bool waterCheck = false;
    public int jumpNum = 1;

    private Animator anim;
    [SerializeField] private Transform playerGraphics;  //Reference to the player's visual graphics so we can change its direction instead of the whole player. -NG

    [SerializeField] private float fallMultiplier = 2.8f;
    [SerializeField] private float jumpMultiplier = 2f;
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
    [SerializeField] private GameObject forcePush;
    [SerializeField] private float maxForce;
    [SerializeField] private Transform throwPoint;
    [SerializeField] private NG_StatManager Nrg;
    [SerializeField] private float count;
    [SerializeField] private bool canShoot;
    [SerializeField] private BatChase BC;

    //private NG_StatManager PlayerStats;
    [SerializeField] private NG_UpgradeMenu Upgrades;
    [SerializeField] private NG_UpgradeProgress upgradeProgress;
    [SerializeField] private NG_IceLaunch iceLaunch;
    public SoundManager sounds;
    [SerializeField]
    IcicleFall icicle;

    public float MoveSpeed
    { get {return moveSpeed;} set {moveSpeed = value;} }

    public float JumpForce
    { get {return jumpForce;} set {jumpForce = value;} }

    public float IceSlide
    { get {return iceSlide;} set {iceSlide = value;} }

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

    public float FallMultiplier
    { get {return fallMultiplier;} set {fallMultiplier = value;} }

    public float JumpMultiplier
    { get {return jumpMultiplier;} set {jumpMultiplier = value;} }

    public KeyCode ThrowSnow
    { get {return throwSnow;} set {throwSnow = value;} }

    public KeyCode ThrowSnow2
    { get {return throwSnow2;} set {throwSnow2 = value;} }

    public KeyCode ThrowSnow3
    { get {return throwSnow3;} set {throwSnow3 = value;} }

    public KeyCode DashKey
    { get { return dashkey; } set { dashkey = value; } }

    public Rigidbody2D RB
    { get { return rb; } set { rb = value; } }

    public bool GroundPositive
    { get { return groundPositive; } set { groundPositive = value; } }

    public Transform PlayerGraphics
    { get { return playerGraphics; } set { playerGraphics = value; } }

    public GameObject CurBall
    { get { return curBall; } set { curBall = value; } }

    public GameObject SnowBall
    { get {return snowBall;} set {snowBall = value;} }

    public GameObject IceBall
    { get {return iceBall;} set {iceBall = value;} }

    public GameObject RockBall
    { get {return rockBall;} set {rockBall = value;} }

    public float CurBallCost
    { get { return curBallCost; } set { curBallCost = value; } }

    public bool CanShoot
    { get { return canShoot; } set { canShoot = value; } }



    // Use this for initialization
    void Start ()
    {

		RB = GetComponent<Rigidbody2D>();
        curBall = snowBall;
		anim = GetComponent<Animator>();
        canShoot = true;
        playerGraphics = gameObject.transform.Find("Graphics");
        if (playerGraphics == null)
            Debug.Log("No object 'Graphics' can be found as a child of the Player.");
        forcePush = this.transform.Find("ForcePush").gameObject;
        wallCheckPoint = playerGraphics.Find("WallCheck");
       

        //Find NG_StatManager on Player
        Nrg = FindObjectOfType<NG_StatManager>();

        //var upgrades = GameObject.Find("UpgradeMenu");
        //Upgrades = upgrades.GetComponent<NG_UpgradeMenu>();
        upgradeProgress = FindObjectOfType<NG_UpgradeProgress>();

        //iceLaunch = FindObjectOfType<NG_IceLaunch>();
        //if (iceLaunch == null)
            //return;

        BC = FindObjectOfType<BatChase>();
        icicle = FindObjectOfType<IcicleFall>();
    }
    
    // Update is called once per frame
    void Update ()
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

        if (Nrg.Energy.CurrentVal >= curBallCost|| Nrg.Energy.CurrentVal== Nrg.Energy.MaxVal)
        {
            canShoot = true;
        }

        groundPositive = Physics2D.OverlapCircle (groundCheckPoint.position, radiusCheck, isGround);

        if (iceCheck == true)
            groundPositive = true;
        if (waterCheck == true)
            groundPositive = true;

        //Stunning the player when knocked back by enemy -NG
        if(knockbackTimer <= 0)
        {
            if(!isDashing)
                HorizontalMovement();
        }
        else
        {
            if (knockedFromRight)
                RB.velocity = new Vector2(-knockback, knockback);
            if (!knockedFromRight)
                RB.velocity = new Vector2(knockback, knockback);
            knockbackTimer -= Time.deltaTime;
        }

        //Jumping mechanic
        if (groundPositive == true)
            jumpNum = 1;

        Jump();
        //WallJump();

        //Dash ability -NG
        dashSpeed = new Vector2(20, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        if (canDash && Input.GetKeyDown(dashkey))
        {
            if(upgradeProgress.DashUnlocked)
                StartCoroutine(Dash(0.3f));
        }

        if (isDashing)
        {
            Color tmp = playerGraphics.gameObject.GetComponent<SpriteRenderer>().color;
            tmp = Color.gray;
            playerGraphics.gameObject.GetComponent<SpriteRenderer>().color = tmp;
        }
        if(!isDashing)
        {
            Color tmp = playerGraphics.gameObject.GetComponent<SpriteRenderer>().color;
            tmp = Color.white;
            playerGraphics.gameObject.GetComponent<SpriteRenderer>().color = tmp;
        }
        
        if (RB.velocity.y < 0)
        {
            RB.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

        else if (RB.velocity.y > 0 && !Input.GetKeyDown(jump))
        {
            RB.velocity += Vector2.up * Physics2D.gravity.y * (jumpMultiplier - 1) * Time.deltaTime;
        }

        AmmoSwap();
        ForcePush();

        if (Input.GetKeyDown(throwSnow))
        {
            //snoThro();
			/*GameObject ballClone = (GameObject) Instantiate (snowBall, throwPoint.position, throwPoint.rotation); 
			//ballClone.transform.localScale = transform.localScale;

			anim.SetTrigger("Throw");
            
		}

		if(Input.GetKeyDown(throwSnow2)){

			GameObject ballClone = (GameObject) Instantiate (iceBall, throwPoint.position, throwPoint.rotation); 
			ballClone.transform.localScale = transform.localScale;

			anim.SetTrigger("Throw");

		}

		if(Input.GetKeyDown(throwSnow3)){

			GameObject ballClone = (GameObject) Instantiate (rockBall, throwPoint.position, throwPoint.rotation); 
			ballClone.transform.localScale = transform.localScale;

			anim.SetTrigger("Throw");*/

		}

		if(RB.velocity.x<0f)
        {
			playerGraphics.localScale = new Vector3 (-1, 1, 1);
		}

		else if(RB.velocity.x>0f)
        {
			playerGraphics.localScale = new Vector3 (1, 1, 1);
		}

		anim.SetFloat("Speed", Mathf.Abs(RB.velocity.x));

		anim.SetBool ("Grounded", groundPositive);

	}
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "SnoP")
        {
            curBall = snowBall;            
            GameObject.Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "IceP")
        {
            curBall = iceBall;
            GameObject.Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "RockP")
        {
            curBall = rockBall;
            GameObject.Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Icicle")
        {
            KnockBackTimer = KnockBackDistance;
            if (transform.position.x > other.transform.position.x)
                KnockedFromRight = true;
            else
                KnockedFromRight = false;

            icicle.fall();
        }

        //adding health when touching healthpickup
        if (other.gameObject.tag == "HealthPickup")
        {
            Destroy(other.gameObject);
            Nrg.Health.CurrentVal += 5;
        }

        if(other.gameObject.name == "IceLauncher" || other.gameObject.name == "IceLauncher(Clone)")
        {
            iceLaunch = other.GetComponent<NG_IceLaunch>();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ice")
            iceCheck = true;

        if (collision.gameObject.name == "WaterBody")
        {
            waterCheck = true;
        }

        if (collision.gameObject.tag == "Bat")
        {
            BC.chase();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bat")
        {
            BC.goBack();
        }

        if (collision.gameObject.name == "WaterBody")
        {
            waterCheck = false;
        }

        if (collision.gameObject.tag == "Ice")
            iceCheck = false;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            KnockBackTimer = KnockBackDistance;
            if (transform.position.x > collision.transform.position.x)
                KnockedFromRight = true;
            else
                KnockedFromRight = false;

            sounds.hurt();
        }
    }

    public void snoThro()
    {
        if (canShoot == true)
        {
            GameObject.Instantiate(curBall, throwPoint.position, throwPoint.rotation);
            sounds.snoSound();
            //ballClone.transform.localScale = transform.localScale;
            /*if (curBall = snowBall)
            {                
                Nrg.Energy.CurrentVal=Nrg.Energy.CurrentVal-1;
            }

            else if (curBall = iceBall)
            {
                Nrg.Energy.CurrentVal = Nrg.Energy.CurrentVal-1;
            }

            else if (curBall = rockBall)
            {
                Nrg.Energy.CurrentVal=Nrg.Energy.CurrentVal-1;
            }*/
            
           anim.SetTrigger("Throw");
        }
    }

    //Hold all movement controls -NG
    private void HorizontalMovement()
    {
        if (Input.GetKey(left))
        {
            if (iceCheck)
            {
                RB.AddForce(new Vector2(-moveSpeed * 0.8f, RB.velocity.y), ForceMode2D.Force);
            }
            else
            {
                RB.velocity = new Vector2(-moveSpeed, RB.velocity.y);
            }
        }

        else if (Input.GetKey(right))
        {
            if (iceCheck)
            {
                RB.AddForce(new Vector2(moveSpeed * 0.8f, RB.velocity.y), ForceMode2D.Force);
            }
            
            else
            {
                RB.velocity = new Vector2(moveSpeed, RB.velocity.y);
            }
        }

        else
        {
            if (iceCheck)
                RB.velocity = new Vector2(RB.velocity.x * iceSlide, RB.velocity.y);
            if (iceLaunch.Launching)
            {
                Debug.Log("Launching the player");
                if (iceLaunch.LaunchRight == true)
                    RB.velocity = new Vector2(iceLaunch.LaunchPower.x, iceLaunch.LaunchPower.y);
                else if (iceLaunch.LaunchRight == false)
                    RB.velocity = new Vector2(-iceLaunch.LaunchPower.x, iceLaunch.LaunchPower.y);
            }
            else
                RB.velocity = new Vector2(0, RB.velocity.y);
        }
    }

    //Jumping controls -NG
    private void Jump()
    {
        if (Input.GetKeyDown(jump) && groundPositive)
        {
            RB.velocity = new Vector2(RB.velocity.x, jumpForce);
            sounds.jumpSound();
        }

        else if (Input.GetKeyUp(jump) && jumpNum > 0 && RB.velocity.y > 1)
        {
            RB.velocity = new Vector2(RB.velocity.x, fallMultiplier);
            jumpNum = 0;
        }
    }

    private void WallJump()
    {
        Physics2D.queriesStartInColliders = false;
        RaycastHit2D wallHit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, wallCheckDistance);

        if(Input.GetKeyDown(jump) && !groundPositive && wallHit.collider != null)
        {
            jumpOffWall = true;
            RB.velocity = new Vector2(wallJumpSpeed * wallHit.normal.x, wallJumpSpeed);
            playerGraphics.transform.localScale = playerGraphics.transform.localScale.x == 1 ? new Vector2(-1, 1) : Vector2.one;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * transform.localScale.x * wallCheckDistance);
    }

    IEnumerator Dash(float dashDuration)
    {
        var dashCollider = GetComponent<Collider2D>();
        dashCollider.enabled = !dashCollider.enabled;

        float time = 0;
        canDash = false;
        isDashing = true;

        while(dashDuration > time)
        {
            if (throwPoint.transform.position.x > this.transform.position.x)
            {
                time += Time.deltaTime;
                //RB.velocity = new Vector2(dashSpeed.x, dashSpeed.y);
                transform.Translate(Vector3.right * Time.deltaTime * dashSpeed.x, throwPoint);
                yield return 0;
            }
            if (throwPoint.transform.position.x < this.transform.position.x)
            {
                time += Time.deltaTime;
                //RB.velocity = new Vector2(-dashSpeed.x, dashSpeed.y);
                transform.Translate(Vector3.right * Time.deltaTime * dashSpeed.x, throwPoint);
                yield return 0;
            }
            /*time += Time.deltaTime;
            RB.velocity = new Vector2(dashSpeed.x, dashSpeed.y);
            yield return 0;*/
        }
        //yield return new WaitForSeconds(dashCoolDown);
        canDash = true;
        isDashing = false;
        dashCollider.enabled = !dashCollider.enabled;
    }

    private void AmmoSwap()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            curBall = snowBall;
        }

        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("The 2 button works");
            if (upgradeProgress.IceBall == true)
            {
                curBall = iceBall;
                Debug.Log("The if statement works");
            }
            else
            {
                return;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("The 3 button works");
            if (upgradeProgress.RockBall == true)
            {
                curBall = rockBall;
                Debug.Log("The if statement works");
            }
            else
            {
                return;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Debug.Log("Ready to place ice launcher.");
            curBall = iceLaunchBall;
        }
    }

    private void ForcePush()
    {
        float willPower = Nrg.Energy.CurrentVal / Nrg.Energy.MaxVal;
        float allTheEnergy = Nrg.Energy.CurrentVal;

        if (Input.GetKeyDown(throwSnow2))
        {
            forcePush.SetActive(true);
            maxForce = forcePush.GetComponent<PointEffector2D>().forceMagnitude;
            forcePush.GetComponent<PointEffector2D>().forceMagnitude *= willPower;
            Nrg.Energy.CurrentVal -= allTheEnergy;
        }
        if (Input.GetKeyUp(throwSnow2))
        {
            forcePush.GetComponent<PointEffector2D>().forceMagnitude = maxForce;
            forcePush.SetActive(false);
        }
    }
}
