using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glass : MonoBehaviour
{
    [SerializeField] bool glass;
    [SerializeField] bool fire;
    [SerializeField] bool tornado;

    SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Use this for initialization
    void Start()
    {
        if (glass)
        {
            sprite.color = new Color(0.986207f, 1, 0, 1); //Replace with a statement that changes the sprite once we have art for each wall type.
        }
        if (fire)
        {
            sprite.color = Color.red; //Replace with a statement that changes the sprite once we have art for each wall type.
        }
        if (tornado)
        {
            sprite.color = Color.green; //Replace with a statement that changes the sprite once we have art for each wall type.
        }

        if (!glass && !fire && !tornado)
            glass = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (glass)
        {
            if (other.gameObject.tag != "RockP")
                Debug.Log("Try rockball");
            else
            {
                Debug.Log("Throwing stones");
                Destroy(gameObject);
            }
        }
        if (fire)
        {
            if (other.gameObject.tag != "IceP")
                Debug.Log("Try iceball");
            else
            {
                Debug.Log("Throwing ice");
                Destroy(gameObject);
            }
        }
        if (tornado)
        {
            if (other.gameObject.tag != "BoomP")
                Debug.Log("Try boomerang");
            else
            {
                Debug.Log("G'day Mate");
                Destroy(gameObject);
            }
        }
    }
}
