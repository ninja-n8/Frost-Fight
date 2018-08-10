using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBoss : MonoBehaviour
{
    public float jumpHeight;
    public Transform[] jumpPoint;
    Rigidbody2D rb;
    public int jumpChoice;
    [SerializeField] GameObject projectile;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, jumpHeight);
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.velocity.y == 0)
        {
            GameObject.Instantiate(projectile, transform.position, transform.rotation);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        jumpChoice = Random.Range(1, 4);
        if (jumpChoice == 1)
        {
            this.gameObject.transform.position = jumpPoint[0].position;
            rb.velocity = new Vector2(0, jumpHeight);
        }
        if (jumpChoice == 2)
        {
            this.gameObject.transform.position = jumpPoint[1].position;
            rb.velocity = new Vector2(0, jumpHeight);
        }
        if (jumpChoice == 3)
        {
            this.gameObject.transform.position = jumpPoint[2].position;
            rb.velocity = new Vector2(0, jumpHeight);
        }
    }
}
