using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Original base code from Will Chestnut's BossCam.cs

public class NG_CameraPosSet : MonoBehaviour
{
    public Camera mainCam;
    [SerializeField] Vector2 camPos;
    [SerializeField] public float normalSize;
    [SerializeField] public float roomSize;
    [SerializeField] bool inRoom = false;

    float smoothVelocity;

    [SerializeField] NG_Player player;
    [SerializeField] NG_CameraFollowPlayer camFollowPlayer;
    [SerializeField] NG_CameraFollow camFollow;

    public bool InRoom
    { get { return inRoom; } set { inRoom = value; } }

    // Use this for initialization
    void Start()
    {
        mainCam = Camera.main;
        camFollowPlayer = mainCam.GetComponent<NG_CameraFollowPlayer>();
        camFollow = mainCam.GetComponent<NG_CameraFollow>();

        normalSize = mainCam.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        float t = 0;
        t += Time.deltaTime;
        /*if (!inRoom)
        {
            //mainCam.orthographicSize = normalSize;
            camFollow.enabled = false;
            camFollowPlayer.enabled = true;
        }
        if (inRoom)
        {
            //mainCam.orthographicSize = Mathf.Lerp(normalSize, 20, Time.deltaTime/camFollowPlayer.lookSmoothtimeX);
            camFollow.enabled = true;
            camFollowPlayer.enabled = false;
        }*/
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            player = collision.GetComponent<NG_Player>();
            camFollowPlayer.CamPosSet = this;
            inRoom = true;
            StartCoroutine(CameraShift());
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            inRoom = false;
            StartCoroutine(CameraShift());
        }
    }

    IEnumerator CameraShift()
    {
        if (!inRoom)
        {
            camFollow.Target = new Vector2(player.transform.position.x + camFollowPlayer.CurrentLookAheadX, player.transform.position.y);
            Debug.Log("shift to player");
            camFollowPlayer.outRoomBounds = true;
            yield return new WaitForSeconds(0.5f);
            camFollow.enabled = false;
        }
        if (inRoom)
        {
            camFollow.Target = this.transform.position;
            yield return new WaitForSeconds(0.5f);
            camFollow.enabled = true;
            camFollowPlayer.outRoomBounds = false;

        }
        yield return null;
    }
}
