using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NG_Cannon : MonoBehaviour
{
    [SerializeField] private float horizontalBoost;
    [SerializeField] private float verticalBoost;
    [SerializeField] private Vector2 boostDirection;
    [SerializeField] private Transform target;
    [SerializeField] private bool canBoost = true;
    [SerializeField] private float speed = 2;
    [SerializeField] private float t = 1.0f;
    [SerializeField] private float curT = 2;

    [SerializeField] private WaitForSeconds boostDuration = new WaitForSeconds(0.07f);
    [SerializeField] private float cannonRange;
    private LineRenderer trajectory;
    private RaycastHit2D hit;

    [SerializeField] private GameObject player;
    private Animator anim;
    private float tempRot;
    [SerializeField] private float playerGravity;
    [SerializeField] private Transform barrel;

    // Use this for initialization
    void Start()
    {
        target = GameObject.Find("Target").transform;
        boostDirection = new Vector2(target.position.x, target.position.y);
        trajectory = barrel.GetComponent<LineRenderer>();
        hit = new RaycastHit2D();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        //t = Mathf.Lerp(0f, 1f, Time.deltaTime * curT);
        

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!canBoost)
            {
                /*curT += (2 * Time.deltaTime);
                if (curT > t)
                {
                    curT = t;
                }*/
                BlastOff();
                //StartCoroutine(BlastOff(0.5f));
                //AnimBlastOff();
            }
                
        }

        if (!canBoost && player)
            player.transform.position = barrel.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (!player)
                player = collision.gameObject;

            LoadUp();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (canBoost)
                player = null;
        }
    }

    void LoadUp()
    {
        Debug.Log("loading the cannon");
        if (player == null)
            return;

        tempRot = player.transform.rotation.z;
        player.transform.SetParent(barrel);


        playerGravity = player.GetComponent<Rigidbody2D>().gravityScale;
        player.GetComponent<Rigidbody2D>().gravityScale = 0;
        player.transform.position = barrel.position;

        canBoost = false;
    }

    /*IEnumerator BlastOff(float blastDuration)
    {
        float time = 0;

        t += Time.deltaTime * curT;
        if (t > 1)
        {
            t = 0;
        }

        while (blastDuration > time)
        {
            player.GetComponent<Rigidbody2D>().gravityScale = playerGravity;
            player.transform.position = Vector2.Lerp(new Vector2(barrel.transform.position.x, barrel.transform.position.y), boostDirection, t);
            player.transform.eulerAngles = new Vector3(0, 0, tempRot);

            if (player.transform.position == target.transform.position)
            {
                player = null;
                barrel.GetChild(0).parent = null;
                canBoost = true;
                yield return 0;
            }
        }
    }*/

    void BlastOff()
    {
        if (player == null)
            return;
        t += Time.deltaTime * curT;
        if (t > 1)
        {
            t = 0;
        }
        
        /*player.GetComponent<Rigidbody2D>().gravityScale = playerGravity;
        //player.GetComponent<Rigidbody2D>().velocity = new Vector2(horizontalBoost, verticalBoost);
        player.transform.position = Vector2.MoveTowards(player.transform.position, target.transform.position, t);
        player.transform.eulerAngles = new Vector3(0, 0, tempRot);
        //barrel.GetChild(0).gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(horizontalBoost, verticalBoost),ForceMode2D.Impulse);
        //barrel.GetChild(0).gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Cos(this.transform.localEulerAngles.z * Mathf.Deg2Rad), Mathf.Sin(this.transform.localEulerAngles.z) * verticalBoost), ForceMode2D.Impulse);
        //player.gameObject.GetComponent<Rigidbody2D>().velocity = boostDirection;*/
        if (player.transform.position != target.transform.position)
        {
            player.GetComponent<Rigidbody2D>().gravityScale = playerGravity;
            player.transform.position = Vector2.MoveTowards(player.transform.position, target.transform.position, t);
            player.transform.eulerAngles = new Vector3(0, 0, tempRot);
        }
        if (player.transform.position == target.transform.position)
        {
            player = null;
            barrel.GetChild(0).parent = null;
            canBoost = true;
        }
    }

    void AnimBlastOff()
    {

    }
}
