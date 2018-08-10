using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseControls : MonoBehaviour {

    public NG_Player PC;

    //variable to store original movespeed -NG
    private float originalMoveSpeed;

    // Use this for initialization
    void Start () {
        PC = FindObjectOfType<NG_Player>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(reverseTime());
                        
        }
    }

    IEnumerator reverseTime()
    {
        originalMoveSpeed = PC.MoveSpeed;
        PC.MoveSpeed = -PC.MoveSpeed;
        this.transform.position = Vector3.down * 100;
        yield return new WaitForSeconds(2);
        PC.MoveSpeed = originalMoveSpeed;
        Destroy(this.gameObject);
    }
}
