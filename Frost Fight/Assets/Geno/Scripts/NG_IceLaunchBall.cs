using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NG_IceLaunchBall : MonoBehaviour
{
    [SerializeField] private float throwSpeed;
    [SerializeField] private GameObject iceLauncher;

    private Rigidbody2D RB;

    public SoundManager SM;

    private NG_StatManager stat;

    public float NRG;

    //Getting a reference to the player -NG
    private NG_Player player;

    private NG_Weapon weapon;

    // Use this for initialization
    void Start()
    {
        SM = FindObjectOfType<SoundManager>();
        RB = GetComponent<Rigidbody2D>();
        stat = FindObjectOfType<NG_StatManager>();
        SM.snoSound();

        //Assigning the player script -NG
        player = FindObjectOfType<NG_Player>();
        weapon = FindObjectOfType<NG_Weapon>();

        stat.Energy.CurrentVal -= NRG;
        player.CurBallCost = NRG;
        //Check to see if the player is facing left -NG
        if (player.transform.localScale.x < 0)
            throwSpeed = -throwSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * throwSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 4)
        {
            return;
        }

        if (collision.gameObject.layer == 15)
        {
            return;
        }

        if (collision.gameObject.tag == "noHit")
        {
            return;
        }

        if(collision.gameObject.layer == 8)
        {
            GameObject.Instantiate(iceLauncher, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z), collision.gameObject.transform.rotation);
        }

        Destroy(gameObject);
    }
}
