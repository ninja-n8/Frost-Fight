using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NG_Weapon : MonoBehaviour
{
    float fireRate = 0;
    [SerializeField] float snowFireRate = 4f;
    [SerializeField] float iceFireRate = 2.5f;
    [SerializeField] float rockFireRate = 1.5f;
    [SerializeField] float boomerangFireRate = 1f;

    [SerializeField] float damage = 10;
    [SerializeField] float rockDamageMultiplier = 2;
    [SerializeField] LayerMask whatToHit;

    [SerializeField] Image curProjDisplay;

    [SerializeField] float timeToFire = 0;
    [SerializeField] float snowTimeToFire = 0;
    [SerializeField] float rockTimeToFire = 0;
    [SerializeField] float iceTimeToFire = 0;
    [SerializeField] float boomerangTimeToFire = 0;
    [SerializeField] float timeToReload = 0;
    Transform throwPoint;

    private NG_Player player;
    private NG_PlayerInput playerInput;
    [SerializeField] private NG_CircleMenu circleMenu;

    public float Damage
    { get { return damage; } set { damage = value; } }
    public float RockDamageMultiplier
    { get { return rockDamageMultiplier; } set { rockDamageMultiplier = value; } }
    public Transform ThrowPoint
    { get { return throwPoint; } set { throwPoint = value; } }

    private void Awake()
    {
        throwPoint = gameObject.transform.Find("ThrowPoint");
        if (throwPoint == null)
            Debug.LogError("No ThrowPoint has been assigned.");

        curProjDisplay = GameObject.Find("CurProjectileDisplay").GetComponent<Image>();
    }

    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<NG_Player>();
        playerInput = FindObjectOfType<NG_PlayerInput>();
        circleMenu = FindObjectOfType<NG_CircleMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        if (circleMenu.SelectingAmmo)
            return;
        else
        {
            FireRateChange();

            /*if (Time.time < timeToFire)
                timeToReload += Time.time;
            else
                timeToReload = 0;*/

            if (fireRate == 0)
            {
                if (Input.GetKeyDown(playerInput.ThrowSnow))
                {
                    Shoot();
                }
            }
            else
            {
                if (Input.GetKey(playerInput.ThrowSnow) && Time.time > timeToFire)
                {
                    if (player.curBall == player.snowBall && Time.time > snowTimeToFire)
                    {
                        snowTimeToFire = Time.time + 1 / snowFireRate;
                        timeToReload = snowTimeToFire - Time.time;
                        Shoot();
                    }
                    if (player.curBall == player.rockBall && Time.time > rockTimeToFire)
                    {
                        rockTimeToFire = Time.time + 1 / rockFireRate;
                        timeToReload = rockTimeToFire - Time.time;
                        Shoot();
                    }
                    if (player.curBall == player.iceBall && Time.time > iceTimeToFire)
                    {
                        iceTimeToFire = Time.time + 1 / iceFireRate;
                        timeToReload = iceTimeToFire - Time.time;
                        Shoot();
                    }
                    if (player.curBall == player.boomerang && Time.time > boomerangTimeToFire)
                    {
                        boomerangTimeToFire = Time.time + 1 / boomerangFireRate;
                        timeToReload = boomerangTimeToFire - Time.time;
                        Shoot();
                    }
                    //timeToFire = Time.time + 1 / fireRate;
                    //timeToReload = timeToFire - Time.time;
                    //Shoot();
                }
            }
        }
    }

    void Shoot()
    {
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 throwPointPosition = new Vector2(throwPoint.position.x, throwPoint.position.y);

        RaycastHit2D hit = Physics2D.Raycast(throwPointPosition, mousePosition - throwPointPosition, 100, whatToHit);

        if (player.CanShoot)
        {
            player.Animator.SetTrigger("Shoot");
            GameObject.Instantiate(player.curBall, throwPoint.position, throwPoint.rotation);
            StartCoroutine(Reload(0f, 1f, timeToReload));
        }
    }

    void FireRateChange()
    {
        if (player.curBall == player.snowBall)
            fireRate = snowFireRate;
        if (player.curBall == player.iceBall)
            fireRate = iceFireRate;
        if (player.curBall == player.rockBall)
            fireRate = rockFireRate;
        if (player.curBall == player.boomerang)
            fireRate = boomerangFireRate;
    }

    IEnumerator Reload(float a, float b, float time)
    {
        float i = 0.0f;
        float rate = (1.0f / time) * 1f;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            curProjDisplay.fillAmount = Mathf.Lerp(a, b, i);
            yield return null;
        }
    }
}
