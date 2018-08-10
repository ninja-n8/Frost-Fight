using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchHit : MonoBehaviour
{

    public GameObject offSwitch;
    public bool Exit;
    public bool turnBack;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (turnBack == true)
        {
            turnBack = false;
            StartCoroutine(Reset());
        }
    }

    //Changing this to OnTriggerEnter to work with the new NG_Player stuff -NG
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            offSwitch.SetActive(false);
            Exit = true;

        }

    }

    /*private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            this.gameObject.transform.rotation = Quaternion.Euler(0, 0, rot);
            Exit = true;
            
        }
        if (turnBack == true)
        {
            StartCoroutine(Reset());
        }
    }*/
    public IEnumerator Reset()
    {
        
        yield return new WaitForSeconds(10);
        offSwitch.SetActive(true);
        Exit = false;

    }

}
