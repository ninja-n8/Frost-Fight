using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NG_TornadoPlatform : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        StartCoroutine(Exist());
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    IEnumerator Exist()
    {
        yield return new WaitForSeconds(4.0f);
        Destroy(this.gameObject);
        yield return null;
    }
}
