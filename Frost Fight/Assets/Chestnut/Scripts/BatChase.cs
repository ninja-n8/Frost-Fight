using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatChase : MonoBehaviour {
    //the things i will need in this script.
    [SerializeField]
    float speed;
    [SerializeField]
    Transform player;
    [SerializeField]
    NG_StatManager stats;
    [SerializeField]
    SoundManager SM;
    [SerializeField]
    Transform origin;
    [SerializeField]
    Animator batFlap;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        stats = FindObjectOfType<NG_StatManager>();
        SM = FindObjectOfType<SoundManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.B))
        {
            goBack();
        }
        //chase();
        goBack();
        if (transform.position == origin.transform.position)
        {
            batFlap.SetBool("Idle", true);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            stats.Damage();
            SM.hurt();
            
            GameObject.Destroy(this.gameObject);
        }
       
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            chase();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            chase();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            goBack();
        }
    }
    public void chase()
    {
        if (batFlap == null)
            return;

        batFlap.SetBool("Idle",false);
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed*2);
        
    }

    public void goBack()
    {
        transform.position = Vector2.MoveTowards(transform.position, origin.transform.position, speed);
    }
}
