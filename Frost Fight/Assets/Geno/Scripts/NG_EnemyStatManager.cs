using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NG_BarFollow))]
public class NG_EnemyStatManager : MonoBehaviour
{
    public float moveSpeed = 3f;
    [SerializeField] private float tmpSpeed;
    [SerializeField] Stat enemyHealth;

    [SerializeField] int enemyDamage;

    Color normalColor;
    [SerializeField] bool frozen;

    private NG_BarFollow healthBarFollow;
    [SerializeField] private NG_StatManager Stat;
    [SerializeField] private NG_Weapon weapon;
    private NG_UpgradeProgress upgradeProgress;
    public bool boss;

    //Force Push variables**********************************************
    /*[SerializeField] private float pushback;
    [SerializeField] private float pushbackDistance;
    private float pushbackTimer;
    private bool pushedFromRight;*/
    //******************************************************************

    public Stat EnemyHealth
    { get { return enemyHealth; } set { enemyHealth = value; } }
    
    public int EnemyDamage
    { get { return enemyDamage; } set { enemyDamage = value; } }

    //pushback getters and setters*************************************
    /*public float Pushback
    { get { return pushback; } set { pushback = value; } }
    public float PushbackDistance
    { get { return pushbackDistance; } set { pushbackDistance = value; } }
    public float PushbackTimer
    { get { return pushbackTimer; } set { pushbackTimer = value; } }
    public bool PushedFromRight
    { get { return pushedFromRight; } set { pushedFromRight = value; } }*/
    //*******************************************************************

    //actually we are using this for initialization -NG
    private void Awake()
    {
        enemyHealth.Initialize();

        healthBarFollow = GetComponent<NG_BarFollow>();
        if (healthBarFollow == null)
            Debug.Log("NG_BarFollow.cs has not been assigned to the enemy.");
        Stat = FindObjectOfType<NG_StatManager>();
        weapon = FindObjectOfType<NG_Weapon>();
        upgradeProgress = FindObjectOfType<NG_UpgradeProgress>();
        tmpSpeed = moveSpeed;
    }

    // Use this for initialization
    void Start()
    {
        Stat = FindObjectOfType<NG_StatManager>();
        weapon = FindObjectOfType<NG_Weapon>();
        upgradeProgress = FindObjectOfType<NG_UpgradeProgress>();
        normalColor = GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        if (boss)
        {
            moveSpeed = 6;
        }
        Death();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Collider2D>().gameObject.name=="Snowball 1(Clone)")
            StartCoroutine(Damage(weapon.Damage, 1));
        if (other.GetComponent<Collider2D>().gameObject.name == "Rockball 1(Clone)")
        {
            if (upgradeProgress.RockBallUpgrade)
                StartCoroutine(Damage(weapon.Damage, weapon.RockDamageMultiplier)); //4 damage
            else
                StartCoroutine(Damage(weapon.Damage, 2)); //2 weapon damage
        }
        if (other.GetComponent<Collider2D>().gameObject.name== "Iceball 1(Clone)")
        {
            //StartCoroutine(Freeze());
            frozen = true;
            StartCoroutine(Damage(weapon.Damage, 1));
        }
        if (other.GetComponent<Collider2D>().gameObject.name == "Explosive(Clone)")
            StartCoroutine(Damage(weapon.Damage, 10)); //10 weapon damage
        if (other.GetComponent<Collider2D>().gameObject.name == "Shotgun shell(Clone)")
            StartCoroutine(Damage(weapon.Damage, 1));
        if (other.GetComponent<Collider2D>().gameObject.name == "Shotgun shell (1)(Clone)")
            StartCoroutine(Damage(weapon.Damage, 1));
        if (other.GetComponent<Collider2D>().gameObject.name == "Shotgun shell (2)(Clone)")
            StartCoroutine(Damage(weapon.Damage, 1));
        if (other.GetComponent<Collider2D>().gameObject.name == "Shotgun shell (3)(Clone)")
            StartCoroutine(Damage(weapon.Damage, 1));
        if (other.GetComponent<Collider2D>().gameObject.name == "Shotgun shell (4)(Clone)")
            StartCoroutine(Damage(weapon.Damage, 1));
        if (other.GetComponent<Collider2D>().gameObject.name == "Boomerang(Clone)")
            StartCoroutine(Damage(weapon.Damage, 1));
        if (other.GetComponent<Collider2D>().gameObject.name == "Grower(Clone)")
            StartCoroutine(Damage(weapon.Damage, 2)); //2 weapon damage
        if (other.GetComponent<Collider2D>().gameObject.name == "IceLaunchBall(Clone)")
        {
            StartCoroutine(Freeze());
            StartCoroutine(Damage(weapon.Damage, 1));
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!frozen)
            {
                Stat.Damage();
                StartCoroutine(Damage(2, 1));
            }
            else
            {
                StartCoroutine(Damage(enemyHealth.CurrentVal,1));
            }
        }
    }

    public void Death()
    {
        if (enemyHealth.CurrentVal == 0)
        {
            if (healthBarFollow == null)
                Debug.Log("NG_BarFollow.cs has not been assigned to the enemy.");
            else
                Destroy(healthBarFollow.EnemyBar);

            Destroy(gameObject);
        }
    }
    //StartCoroutine(Damage(, 1));
    IEnumerator Damage(float damage, float multiplier)
    {
        //tmpSpeed = moveSpeed;
        enemyHealth.CurrentVal -= damage * multiplier;
        moveSpeed = 0;
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.25f);
        if (!frozen)
        {
            GetComponent<SpriteRenderer>().color = normalColor;
            moveSpeed = tmpSpeed;
        }
        else
        {
            StartCoroutine(Freeze());
        }
        yield return null;
    }

    IEnumerator Freeze()
    {
        //float tmpSpeed = moveSpeed;
        //frozen = true;
        GetComponent<SpriteRenderer>().color = new Color(0.2784472f, 0, 1, 1);
        moveSpeed = tmpSpeed / 2;
        yield return new WaitForSeconds(Stat.FreezeTime);
        moveSpeed = tmpSpeed;
        GetComponent<SpriteRenderer>().color = normalColor;
        frozen = false;
        yield return null;
    }

    /*public void ForcePushed()
    {
        if(pushbackTimer <= 0)
        {
            return;
        }
        else
        {
            if (pushedFromRight)
                GetComponent<Rigidbody2D>().velocity = new Vector2(-pushback, pushback);
            if (!pushedFromRight)
                GetComponent<Rigidbody2D>().velocity = new Vector2(pushback, pushback);
            pushbackTimer -= Time.deltaTime;
        }
    }*/
}

