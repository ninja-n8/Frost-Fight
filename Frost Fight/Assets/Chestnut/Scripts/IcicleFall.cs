using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcicleFall : MonoBehaviour {

    public float fallHeight = 40f;
    public Vector2 fallSpeed;
    public GameObject origin;
    public float fallVelocity;
    public LayerMask icicleMask;
    public CheckpointManager CPM;
    //reference to StatManager (Health, Stamina, etc.)
    public NG_StatManager PlayerStats;
    public SoundManager Sound;
    public GameObject Burst;
    public GameObject drops;
    
	// Use this for initialization
	void Start () {
        //Find NG_StatManager on Player
        PlayerStats = FindObjectOfType<NG_StatManager>();
        Sound = FindObjectOfType<SoundManager>();
        CPM = FindObjectOfType<CheckpointManager>();
        //StartCoroutine(drip());
    }
	
	// Update is called once per frame
	void Update () {
        RaycastHit2D hit = Physics2D.Raycast(origin.transform.position, Vector2.down, fallHeight, icicleMask);
        //GetComponent<Rigidbody2D>().AddForce(fallSpeed);

        Debug.DrawRay(origin.transform.position, Vector2.down * fallHeight, Color.red);

        if (hit)
        {
            Debug.Log("Icicle " + hit.transform);

            if (hit.transform.gameObject.tag == "Player")
            {
                fall();
            }
            if (hit.transform.gameObject.tag == "Tripper")
            {
                fall();
            }
            else
            {
                Debug.Log("I hope this helps");
            }
        }
    }

    public void fall()
    {
        fallSpeed = Vector2.down*fallVelocity;
        GetComponent<Rigidbody2D>().AddForce(fallSpeed);
        Sound.icicleSound();
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            PlayerStats.Health.CurrentVal--;
            Sound.hurt();
            GameObject.Destroy(this.gameObject);
            Instantiate(Burst, transform.position, transform.rotation);
        }
        else
        {
            GameObject.Destroy(this.gameObject);
            Instantiate(Burst, transform.position, transform.rotation);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Destroy")
        {
            GameObject.Destroy(this.gameObject);
            Instantiate(Burst, transform.position, transform.rotation);
        }

        if(collision.gameObject.tag == "SnoP")
            GameObject.Destroy(this.gameObject);
    }

    public IEnumerator drip()
    {
        yield return new WaitForSeconds(2);
        Instantiate(drops,origin.transform.position,origin.transform.rotation);
        Sound.drip();
        StartCoroutine(drip());
    }
}
