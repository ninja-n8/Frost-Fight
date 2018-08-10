using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballPlatform : MonoBehaviour
{
    public GameObject iceGem;
    public GameObject platform;
	public GameObject snowPlat;

    public float platDelay;
    public float gemDelay;

    bool frozen;
    Color normalColor;
    Color frozenColor = new Color(0.08962262f, 0.8721379f, 1, 1);
    Color endColor = new Color(0.5f, 0.5f, 0.8f, 0.7f);
    float lerpSpeed = 1f;

    //at start set iceGem to active and platform to inactive
    void Start()
    {
        iceGem.SetActive(true);
        platform.SetActive(false);
        normalColor = platform.GetComponent<SpriteRenderer>().color;
    }

    private void Update()
    {
        if (!frozen)
        {
            platform.GetComponent<SpriteRenderer>().color = normalColor;
            platform.gameObject.tag = "Ground";
        }
        if (frozen)
        {
            platform.GetComponent<SpriteRenderer>().color = frozenColor;
            platform.gameObject.tag = "Ice";
        }
    }

    //if the script holder comes into contact with a GameObject tagged as "SnoP", "IceP", or "RockP", start the GemHit() Coroutine
    void OnTriggerEnter2D(Collider2D other)
    {
      

        if (other.tag == "IceP")
        {
            frozen = true;
            StartCoroutine(GemHit());
        }

       
    }

    IEnumerator GemHit()
    {
        //set iceGem as inactive, and set platform to active
        iceGem.SetActive(false);
		UnityEngine.Object.Destroy(snowPlat.GetComponent<CircleCollider2D>());
        platform.SetActive(true);

        //wait for platDelay to expire and set platform to inactive
        if (!frozen)
        {
            //wait 2 seconds before setting the fragilePlat to inactive
            float i = 0.0f;
            float rate = (1.0f / platDelay) * lerpSpeed;
            while (i < 1.0f)
            {
                i += Time.deltaTime * rate;
                platform.GetComponent<SpriteRenderer>().color = Color.Lerp(normalColor, endColor, i);
                yield return null;
            }
        }
        else
        {
            float i = 0.0f;
            float rate = (1.0f / platDelay / 2) * lerpSpeed;
            while (i < 1.0f)
            {
                i += Time.deltaTime * rate;
                platform.GetComponent<SpriteRenderer>().color = Color.Lerp(frozenColor, endColor, i);
                yield return null;
            }
        }

        platform.SetActive(false);

        //wait for gemDelay to expire and set iceGem to active
        yield return new WaitForSeconds(gemDelay);

        frozen = false;
        iceGem.SetActive(true);
		snowPlat.AddComponent<CircleCollider2D>();
    }
}
