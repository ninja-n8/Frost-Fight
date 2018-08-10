using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NG_Waterfall : MonoBehaviour
{
    [SerializeField] GameObject iceWall;
    [SerializeField] GameObject wetSprite;
    [SerializeField] GameObject coldSprite;
    //[SerializeField] IceTrail waterFall;
    [SerializeField] bool frozen;

    Color normalColor;

    // Use this for initialization
    void Start()
    {
        iceWall = gameObject.transform.GetChild(0).gameObject;
        //waterFall = gameObject.transform.GetChild(1).GetComponent<IceTrail>();
        normalColor = iceWall.GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        if (!frozen)
        {
            gameObject.layer = 0;
            //wetSprite.GetComponent<SpriteRenderer>().enabled = true;
            this.transform.GetChild(1).gameObject.SetActive(true);
            //coldSprite.GetComponent<SpriteRenderer>().enabled = false;
            this.transform.GetChild(2).gameObject.SetActive(false);
            //iceWall.GetComponent<SpriteRenderer>().color = new Color(normalColor.r, normalColor.g, normalColor.b, 0.4352941f); //remove this line once sprites have color added by art team

        }
        else
        {
            gameObject.layer = 8;
            //wetSprite.GetComponent<SpriteRenderer>().enabled = false;
            this.transform.GetChild(1).gameObject.SetActive(false);
            //coldSprite.GetComponent<SpriteRenderer>().enabled = true;
            this.transform.GetChild(2).gameObject.SetActive(true);
            //iceWall.GetComponent<SpriteRenderer>().color = new Color(0.08962262f, 0.8721379f, 1, 1); //remove this line once sprites have color added by art team
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!frozen)
        {
            if (collision.gameObject.tag == "IceP")
            {
                frozen = true;
                //iceWall.SetActive(true);
                //waterFall.spawnStop = true;
            }
        }

        if (frozen)
        {
            if (collision.gameObject.tag == "RockP")
            {
                frozen = false;
                //iceWall.SetActive(false);
                //waterFall.spawnStop = false;
            }
        }
    }
}
