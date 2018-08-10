using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NG_PressurePlate : MonoBehaviour
{
    [SerializeField] private bool weighted;
    [SerializeField] private GameObject mechanism;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Rigidbody2D>() == null)
            return;
        else
        {
            if (collision.GetComponent<Rigidbody2D>().mass >= 2)
            {
                Debug.Log("The thing is activated.");
                this.transform.localPosition = Vector2.MoveTowards(this.transform.localPosition, new Vector2(this.transform.localPosition.x, -0.35f), 0.5f);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Rigidbody2D>() == null)
            return;
        else
        {
            if (collision.GetComponent<Rigidbody2D>().mass >= 2)
            {
                this.transform.localPosition = Vector2.MoveTowards(this.transform.localPosition, new Vector2(this.transform.localPosition.x, -0.057f), 0.5f);
            }
        }
    }
}
