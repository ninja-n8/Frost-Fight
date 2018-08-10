using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NG_CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector2 target;

    [SerializeField] private float followSpeed;

    [SerializeField] private Vector3 offset;
    NG_CameraPosSet camPosSet;

    public Vector2 Target
    { get { return target; } set { target = value; } }

    // Use this for initialization
    void Start()
    {
        //target = GameObject.FindGameObjectWithTag("Player");

        //offset = new Vector3(0, 0.5f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float interpolation = followSpeed * Time.deltaTime;

        Vector3 camPos = this.transform.position;

        camPos.y = Mathf.Lerp(this.transform.position.y, target.y, interpolation);
        camPos.x = Mathf.Lerp(this.transform.position.x, target.x, interpolation);

        this.transform.position = camPos + offset;
    }
}
