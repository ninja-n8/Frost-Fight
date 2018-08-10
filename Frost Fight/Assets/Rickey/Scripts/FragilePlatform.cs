using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragilePlatform : MonoBehaviour
{
    public GameObject fragilePlat;
    public GameObject fragTrig;

    [SerializeField] float timeToDestroy;
    [SerializeField] float timeToRestore;

    //Iceball Freeze stuff
    [SerializeField] Color normalColor;
    [SerializeField] Color frozenColor;
    [SerializeField] Color endColor;
    [SerializeField] bool frozen = false;
    private NG_StatManager playerStats;
    [SerializeField] float lerpSpeed = 1f;

    private void Start()
    {
        playerStats = GameObject.Find("PlayerCharacter").GetComponent<NG_StatManager>();
        normalColor = fragilePlat.GetComponent<SpriteRenderer>().color;
    }

    private void Update()
    {
        if (!frozen)
            fragilePlat.GetComponent<SpriteRenderer>().color = normalColor;
        else
            fragilePlat.GetComponent<SpriteRenderer>().color = frozenColor;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Test");

        //If the script holder comes into contact with a GameObject labelled as "Player", start the Fragile() Coroutine
        if (other.tag == "Player")
        {
            Debug.Log("merp");

            StartCoroutine(Fragile(timeToDestroy, timeToRestore));
        }

        if (other.name == "Iceball 1(Clone)")
        {
            StartCoroutine(Freeze());
        }
    }

    IEnumerator Fragile(float destroyTime, float restoreTime)
    {
        if (!frozen)
        {
            //wait 2 seconds before setting the fragilePlat to inactive
            float i = 0.0f;
            float rate = (1.0f / destroyTime) * lerpSpeed;
            while (i < 1.0f)
            {
                i += Time.deltaTime * rate;
                fragilePlat.GetComponent<SpriteRenderer>().color = Color.Lerp(normalColor, endColor, i);
                yield return null;
            }
        }
        else
        {
            float i = 0.0f;
            float rate = (1.0f / destroyTime / 2) * lerpSpeed;
            while (i < 1.0f)
            {
                i += Time.deltaTime * rate;
                fragilePlat.GetComponent<SpriteRenderer>().color = Color.Lerp(frozenColor, endColor, i);
                yield return null;
            }
        }

        UnityEngine.Object.Destroy(fragTrig.GetComponent<BoxCollider2D>());
        fragilePlat.SetActive(false);

        if (!frozen)
        {
            //wait 2 seconds before setting the fragilePlat to active
            yield return new WaitForSeconds(restoreTime);
        }
        else
            yield return new WaitForSeconds(restoreTime);

        frozen = false;

        fragTrig.AddComponent<BoxCollider2D>();

        BoxCollider2D boxCollider2D = fragTrig.GetComponent<BoxCollider2D>();
        boxCollider2D.isTrigger = true;

        fragilePlat.SetActive(true);
    }

    IEnumerator Freeze()
    {
        float tmpDTime = timeToDestroy;
        float tmpRTime = timeToRestore;
        frozen = true;
        timeToDestroy = timeToDestroy * 2;
        timeToRestore = timeToRestore;
        yield return new WaitForSeconds(playerStats.FreezeTime);
        timeToDestroy = tmpDTime;
        timeToRestore = tmpRTime;
        yield return null;
    }
}
