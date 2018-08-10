using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public GameObject fallPlat;
	public GameObject fallTrig;

    public Transform fallPoint;

    //Iceball Freeze stuff
    public Color normalColor;
    [SerializeField] float fallTime = 3;
    [SerializeField] float waitTime = 2;
    [SerializeField] bool frozen;
    NG_StatManager playerStats;

    private void Start()
    {
        playerStats = GameObject.Find("PlayerCharacter").GetComponent<NG_StatManager>();
        normalColor = fallPlat.GetComponent<SpriteRenderer>().color;
    }

    private void Update()
    {
        if (!frozen)
            fallPlat.GetComponent<SpriteRenderer>().color = normalColor;
        else
            fallPlat.GetComponent<SpriteRenderer>().color = new Color(0.2784472f, 0, 1, 1);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //If the script holder comes into contact with a GameObject labelled as "Player", start the Falling() Coroutine
        if (other.tag == "Player")
        {
            StartCoroutine(Falling(fallTime));
        }

        if (other.name == "Iceball 1(Clone)")
        {
            StartCoroutine(Freeze());
        }
    }

    IEnumerator Falling(float fallSeconds)
    {
        float normalFallSeconds = fallSeconds;
		UnityEngine.Object.Destroy(fallTrig.GetComponent<BoxCollider2D>());
        //Wait 2 seconds before giving the fallPlat a Rigidbody2D
        yield return new WaitForSeconds(waitTime);

        if(fallPlat.GetComponent<Rigidbody2D>() == null)
            fallPlat.AddComponent<Rigidbody2D>();
        Rigidbody2D rb = fallPlat.GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        float tmpGrav = fallPlat.GetComponent<Rigidbody2D>().gravityScale;
        if (!frozen)
        {
            fallPlat.GetComponent<Rigidbody2D>().gravityScale = tmpGrav;
            fallSeconds = normalFallSeconds;
        }
        else
        {
            fallPlat.GetComponent<Rigidbody2D>().gravityScale = fallPlat.GetComponent<Rigidbody2D>().gravityScale / 10;
            fallSeconds = fallSeconds * 3;
        }

        yield return new WaitForSeconds(fallSeconds);

        frozen = false;

        //remove the RIgidbody2D from fallPlat
        //UnityEngine.Object.Destroy(fallPlat.GetComponent<Rigidbody2D>());
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
		fallTrig.AddComponent<BoxCollider2D>();

		BoxCollider2D boxCollider2D = fallTrig.GetComponent<BoxCollider2D> ();
		boxCollider2D.isTrigger = true;

        //Return the fallPlat to its original position
        fallPlat.transform.position = fallPoint.transform.position;

    }

    IEnumerator Freeze()
    {
        frozen = true;
        yield return new WaitForSeconds(playerStats.FreezeTime * 2);
        yield return null;
    }
}
