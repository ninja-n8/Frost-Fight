using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LJ_Next3 : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(MiniGo());
        }
    }
    IEnumerator MiniGo()
    {

        print(Time.time);
        yield return new WaitForSeconds(4);
        print(Time.time);

        SceneManager.LoadScene("Main Menu");
    }
}
