using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCam : MonoBehaviour {

    public Camera MainCam;
    public Camera MyCam;
    [SerializeField] float normalSize;
    [SerializeField] float bossSize;
    [SerializeField] bool isDead;
    [SerializeField] bool fighting;

    public NG_EnemyStatManager boss;

    // Use this for initialization
    void Start () {
        MyCam.gameObject.SetActive(false);

        normalSize = MainCam.orthographicSize;
        bossSize = normalSize + 10;
    }
	
	// Update is called once per frame
	void Update () {
        if (boss.EnemyHealth.CurrentVal == 0)
            isDead = true;

        if (isDead)
        {
            MainCam.orthographicSize = normalSize;
            if (!fighting)
                return;
            else
                AspectShrink();
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            float camVelocity = 0.0f;
            //MyCam.gameObject.SetActive(true);
            //MainCam.gameObject.SetActive(false);
            MainCam.orthographicSize = bossSize;
            if (fighting)
                return;
            else
                AspectGrowth();
        }
    }

    void AspectGrowth()
    {
        MainCam.GetComponent<NG_CameraFollowPlayer>().aspectY += 5;
        MainCam.GetComponent<NG_CameraFollowPlayer>().aspectX1610 += 5;
        MainCam.GetComponent<NG_CameraFollowPlayer>().aspectX169 += 5;
        MainCam.GetComponent<NG_CameraFollowPlayer>().aspectX54 += 5;
        MainCam.GetComponent<NG_CameraFollowPlayer>().aspectX43 += 5;
        MainCam.GetComponent<NG_CameraFollowPlayer>().aspectX32 += 5;
        fighting = true;
    }

    void AspectShrink()
    {
        MainCam.GetComponent<NG_CameraFollowPlayer>().aspectY -= 10;
        MainCam.GetComponent<NG_CameraFollowPlayer>().aspectX1610 -= 10;
        MainCam.GetComponent<NG_CameraFollowPlayer>().aspectX169 -= 10;
        MainCam.GetComponent<NG_CameraFollowPlayer>().aspectX54 -= 10;
        MainCam.GetComponent<NG_CameraFollowPlayer>().aspectX43 -= 10;
        MainCam.GetComponent<NG_CameraFollowPlayer>().aspectX32 -= 10;
        fighting = false;
    }
}
