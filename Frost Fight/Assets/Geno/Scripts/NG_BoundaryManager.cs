using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NG_BoundaryManager : MonoBehaviour
{
    private BoxCollider2D managerBox; //Boundary Manager box collider
    private Transform player; //Player transform
    public GameObject boundary; //boundary for each individual boundary manager object

    // Use this for initialization
    void Start()
    {
        managerBox = GetComponent<BoxCollider2D>();
        player = GameObject.Find("PlayerCharacter").transform;
    }

    // Update is called once per frame
    void Update()
    {
        ManageBoundary();
    }

    void ManageBoundary()
    {
        if (managerBox.bounds.min.x < player.position.x && player.position.x < managerBox.bounds.max.x &&
            managerBox.bounds.min.y < player.position.y && player.position.y < managerBox.bounds.max.y)
        {
            boundary.SetActive(true);
            Debug.Log("In " + this.name);
        }
        else
            boundary.SetActive(false);
    }
}
