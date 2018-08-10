using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBall : MonoBehaviour {

	//public GameObject snowBallBurst;

	public float throwSpeed;

	private Rigidbody2D RB;

    public SoundManager SM;

    private NG_StatManager stat;

    public float NRG;

    //Getting a reference to the player -NG
    private NG_Player player;

    private NG_Weapon weapon;

    public bool boss;

    // Use this for initialization
    void Start () {
        SM = FindObjectOfType<SoundManager>();
		RB = GetComponent<Rigidbody2D>();
        stat = FindObjectOfType<NG_StatManager>();
        SM.snoSound();

        //Assigning the player script -NG
        player = FindObjectOfType<NG_Player>();
        weapon = FindObjectOfType<NG_Weapon>();
        if (!boss)
        {
            stat.Energy.CurrentVal -= NRG;
        }
        player.CurBallCost = NRG;
        //Check to see if the player is facing left -NG
        if (player.transform.localScale.x < 0)
            throwSpeed = -throwSpeed;
        
    }
	
	// Update is called once per frame
	void Update () {
        StartCoroutine(die());
        //RB.velocity = new Vector2 (throwSpeed * transform.localScale.x, RB.velocity.y);
        transform.Translate(Vector3.right * Time.deltaTime * throwSpeed);
        
    }

	public void OnTriggerEnter2D(Collider2D other){

        if(other.gameObject.layer == 4)
        {
            return;
        }

        if (other.gameObject.layer == 13)
            return;

        if (other.gameObject.layer == 15)
            return;

        if (other.gameObject.layer == 17)
            return;

        if (other.gameObject.tag == "noHit" && this.gameObject.tag == "SnoP")
        {
            return;
        }

        if (other.gameObject.tag == "Player" && boss)
        {
            SM.hurt();
            stat.Health.CurrentVal--;
        }

        if (other.gameObject.tag == "MainCamera")
            return;

        if (other.gameObject.tag == "Enemy" && !boss)
            Destroy(gameObject);
        if (other.gameObject.tag == "Enemy" && boss)
            return;
        else
        { Destroy(gameObject); }
	}

    public IEnumerator die()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

}
