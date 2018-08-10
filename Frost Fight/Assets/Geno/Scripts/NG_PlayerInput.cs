using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (NG_Player))]
public class NG_PlayerInput : MonoBehaviour
{
    NG_Player player;
    NG_CharacterController2D controller2D;

    [SerializeField] private KeyCode jump;
    [SerializeField] private KeyCode throwSnow;
    [SerializeField] private KeyCode throwSnow2;
    [SerializeField] private KeyCode throwSnow3;
    [SerializeField] private KeyCode dashkey;

    public KeyCode ThrowSnow
    { get { return throwSnow; } set { throwSnow = value; } }

    public KeyCode ThrowSnow2
    { get { return throwSnow2; } set { throwSnow2 = value; } }

    public KeyCode ThrowSnow3
    { get { return throwSnow3; } set { throwSnow3 = value; } }

    public KeyCode DashKey
    { get { return dashkey; } set { dashkey = value; } }

    private void Start()
    {
        player = GetComponent<NG_Player>();
        controller2D = GetComponent<NG_CharacterController2D>();
    }

    private void Update()
    {
        Vector2 directionalInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        player.SetDirectionalInput(directionalInput);

        if (Input.GetKeyDown(jump))
            player.OnJumpInputDown();
        if (Input.GetKeyUp(jump))
            player.OnJumpInputUp();

        //Just for testing. Remove before building -NG
        //AmmoSwap();
    }

    //Just for testing. Remove before building -NG
    private void AmmoSwap()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            player.curBall = player.snowBall;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            player.curBall = player.iceBall;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            player.curBall = player.rockBall;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            //Debug.Log("Ready to place ice launcher.");
            player.curBall = player.boomerang;
        }
    }
}
