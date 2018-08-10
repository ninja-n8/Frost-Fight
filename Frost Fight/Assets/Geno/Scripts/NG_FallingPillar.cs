using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NG_FallingPillar : MonoBehaviour
{
    Collider2D tripWire;
    Rigidbody2D rb;

    float inactiveGravity;
    [SerializeField] float fallspeed;
    [SerializeField] float startTime;
    [SerializeField] Vector3 startPos;
    [SerializeField] bool posReset;

    //Iceball Freeze stuff
    Color normalColor;
    [SerializeField] bool frozen;
    NG_StatManager playerStats;

    private void Awake()
    {
        startPos = gameObject.transform.position;
        normalColor = GetComponent<SpriteRenderer>().color;
    }

    // Use this for initialization
    void Start()
    {
        playerStats = GameObject.Find("PlayerCharacter").GetComponent<NG_StatManager>();
        tripWire = gameObject.transform.GetChild(0).GetComponent<Collider2D>();
        rb = gameObject.GetComponent<Rigidbody2D>();

        inactiveGravity = rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (!frozen)
            GetComponent<SpriteRenderer>().color = normalColor;
        else
            GetComponent<SpriteRenderer>().color = new Color(0.2784472f, 0, 1, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            StartCoroutine(Fall(startTime, fallspeed));
            Debug.Log("Falling!");
        }

        if (collision.name == "Iceball 1(Clone)")
        {
            StartCoroutine(Freeze());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            rb.gravityScale = inactiveGravity;
        }
    }

    IEnumerator Freeze()
    {
        frozen = true;
        yield return new WaitForSeconds(playerStats.FreezeTime * 2);
        yield return null;
    }

    IEnumerator Fall(float time, float gravity)
    {
        time = startTime;
        if (!frozen)
        {
            yield return new WaitForSeconds(time);
            rb.gravityScale = gravity;
        }
        else
        {
            yield return new WaitForSeconds(time * 2);
            rb.gravityScale = gravity / 5;
        }

        yield return new WaitForSeconds(2);
        frozen = false;
        yield return new WaitForSeconds(2);

        if (posReset)
        {
            rb.gravityScale = inactiveGravity;
            rb.velocity = Vector2.zero;
            StartCoroutine(PosReset(this.transform.position, startPos, 2.0f));
        }
    }

    IEnumerator PosReset(Vector3 a, Vector3 b, float time)
    {
        float i = 0.0f;
        float rate = (1.0f / time) * fallspeed;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            this.transform.position = Vector3.Lerp(a, b, i);
            yield return null;
        }

        /*rb.gravityScale = -fallspeed;

        if (transform.position.y == startPos.y)
        {
            rb.gravityScale = inactiveGravity;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            yield return new WaitForSeconds(1f);
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }*/
    }
}
