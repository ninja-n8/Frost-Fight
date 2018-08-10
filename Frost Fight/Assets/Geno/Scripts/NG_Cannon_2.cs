using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NG_Cannon_2 : MonoBehaviour
{
    //Instructions:
    //1. In the inspector for this script, assign the barrel & target spots as the Barrel and Target child objects 
    //      on this specific cannon. Don't use another cannon's objects, or else this cannon won't work.
    //2. Manually drag the Target child to where you want the player to go.
    //3. Set the speed you want the player to travel at in the inspector.
    //***DO NOT ASSIGN THE PLAYER SPOT IN THE SCRIPT'S INSPECTOR! It is automatically assigned when the player jumps into the cannon.

    [SerializeField] private GameObject player; //player will be null until the player jumps into the cannon, don't worry about it -NG
    [SerializeField] private Transform target; //Manually drag the target object to where you want the player to be shot to. -NG
    [SerializeField] private Transform barrel;

    [SerializeField] private float speed; //This sets how fast the player will go. -NG
    [SerializeField] private bool readyToFire = false;
    [SerializeField] private bool fired = false;
    [SerializeField] private float tempRot;
    private float playerGravity;

    // Use this for initialization
    void Start()
    {
        //target = GameObject.Find("Target").transform;
        //barrel = GameObject.Find("Barrel").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && readyToFire == true)
        {
            Firing();
        }

        if (readyToFire == true && player != null)
            player.transform.position = barrel.position;

        if (fired == true)
            player.transform.position = Vector2.MoveTowards(player.transform.position, target.transform.position, speed);
            //player.GetComponent<NG_CharacterController2D>().Move(target.transform.position, target.transform.position, false);

        if (player != null)
        {
            if (player.transform.position == target.position)
            {
                fired = false;
                player.GetComponent<Rigidbody2D>().gravityScale = 0.1f;
                player = null;
                barrel.GetChild(0).parent = null;
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (player == null)
            {
                player = collision.gameObject;
                Debug.Log("Player is set");
            }

            LoadUp();
        }
    }

    public void LoadUp()
    {
        if (player == null)
            return;

        tempRot = player.transform.rotation.z;
        player.transform.SetParent(barrel);

        playerGravity = player.GetComponent<Rigidbody2D>().gravityScale;
        player.GetComponent<Rigidbody2D>().gravityScale = 0;

        player.transform.position = barrel.position;

        readyToFire = true;
    }

    public void Firing()
    {
        if (player == null)
            return;

        readyToFire = false;
        fired = true;
    }
}
