using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NG_CameraFollowPlayer : MonoBehaviour
{
    public NG_CharacterController2D target;
    public float verticalOffset;
    public float lookAheadDistX;
    public float lookSmoothtimeX;
    public float verticalSmoothTime;

    public float aspectY;
    public float aspectX1610;
    public float aspectX169;
    public float aspectX54;
    public float aspectX43;
    public float aspectX32;

    public Vector2 focusAreaSize;
    
    FocusArea focusArea;

    float currentLookAheadX;
    float targetLookAheadX;
    float lookAheadDirX;
    float smoothLookVelocityX;
    float smoothVelocityY;

    float lerpSpeed = 5.0f;

    bool lookAheadStopped;
    NG_CameraPosSet camPosSet;
    public bool outRoomBounds;

    private BoxCollider2D cameraBox; //box collider of the camera

    public float CurrentLookAheadX
    { get { return currentLookAheadX; } set { currentLookAheadX = value; } }
    public NG_CameraPosSet CamPosSet
    { get { return camPosSet; } set { camPosSet = value; } }

    private void Start()
    {
        focusArea = new FocusArea(target.boxCollider.bounds, focusAreaSize);
        outRoomBounds = true;
        cameraBox = GetComponent<BoxCollider2D>();

        //Automatiacally finding the player. We can comment this out if we want something else to be followed.
        //target = FindObjectOfType<NG_CharacterController2D>();
    }

    private void FixedUpdate()
    {
        focusArea.Update(target.boxCollider.bounds);

        AspectRatioBoxChange();

        if (GameObject.Find("Boundary"))
        {
            if (outRoomBounds)
            {
                Vector2 focusPosition = focusArea.center + Vector2.up * verticalOffset;

                if (focusArea.velocity.x != 0)
                {
                    lookAheadDirX = Mathf.Sign(focusArea.velocity.x);
                    if (Mathf.Sign(target.playerInput.x) == Mathf.Sign(focusArea.velocity.x) && target.playerInput.x != 0)
                    {
                        lookAheadStopped = false;
                        targetLookAheadX = lookAheadDirX * lookAheadDistX;
                    }
                    else
                    {
                        if (!lookAheadStopped)
                        {
                            lookAheadStopped = true;
                            targetLookAheadX = currentLookAheadX + (lookAheadDirX * lookAheadDistX - currentLookAheadX) / 4f;
                        }
                    }
                }

                currentLookAheadX = Mathf.SmoothDamp(currentLookAheadX, targetLookAheadX, ref smoothLookVelocityX, lookSmoothtimeX);

                focusPosition.y = Mathf.SmoothDamp(transform.position.y, focusPosition.y, ref smoothVelocityY, verticalSmoothTime);
                focusPosition += Vector2.right * currentLookAheadX;
                //transform.position = (Vector3)focusPosition + Vector3.forward * -20;
                transform.position = new Vector3(Mathf.Clamp(focusPosition.x, GameObject.Find("Boundary").GetComponent<BoxCollider2D>().bounds.min.x + cameraBox.size.x / 2, GameObject.Find("Boundary").GetComponent<BoxCollider2D>().bounds.max.x - cameraBox.size.x /2),
                    Mathf.Clamp(focusPosition.y, GameObject.Find("Boundary").GetComponent<BoxCollider2D>().bounds.min.y + cameraBox.size.y / 2, GameObject.Find("Boundary").GetComponent<BoxCollider2D>().bounds.max.y - cameraBox.size.y / 2),
                    transform.position.z);
                /*if(camPosSet != null)
                    StartCoroutine(CameraSizeShift(camPosSet.roomSize, camPosSet.normalSize, 0.5f));
                if (camPosSet == null)
                    return;*/
            }
            /*if (!outRoomBounds)
            {
                StartCoroutine(CameraSizeShift(camPosSet.normalSize, camPosSet.roomSize, 0.5f));
            }*/
        }
        else
        {
            if (outRoomBounds)
            {
                Vector2 focusPosition = focusArea.center + Vector2.up * verticalOffset;

                if (focusArea.velocity.x != 0)
                {
                    lookAheadDirX = Mathf.Sign(focusArea.velocity.x);
                    if (Mathf.Sign(target.playerInput.x) == Mathf.Sign(focusArea.velocity.x) && target.playerInput.x != 0)
                    {
                        lookAheadStopped = false;
                        targetLookAheadX = lookAheadDirX * lookAheadDistX;
                    }
                    else
                    {
                        if (!lookAheadStopped)
                        {
                            lookAheadStopped = true;
                            targetLookAheadX = currentLookAheadX + (lookAheadDirX * lookAheadDistX - currentLookAheadX) / 4f;
                        }
                    }
                }

                currentLookAheadX = Mathf.SmoothDamp(currentLookAheadX, targetLookAheadX, ref smoothLookVelocityX, lookSmoothtimeX);

                focusPosition.y = Mathf.SmoothDamp(transform.position.y, focusPosition.y, ref smoothVelocityY, verticalSmoothTime);
                focusPosition += Vector2.right * currentLookAheadX;
                transform.position = (Vector3)focusPosition + Vector3.forward * -20;
                //transform.position = new Vector3(Mathf.Clamp(focusPosition.x, GameObject.Find("Boundary").GetComponent<BoxCollider2D>().bounds.min.x, GameObject.Find("Boundary").GetComponent<BoxCollider2D>().bounds.max.x),
                    //Mathf.Clamp(focusPosition.y, GameObject.Find("Boundary").GetComponent<BoxCollider2D>().bounds.min.y, GameObject.Find("Boundary").GetComponent<BoxCollider2D>().bounds.max.y),
                    //transform.position.z);
                /*if(camPosSet != null)
                    StartCoroutine(CameraSizeShift(camPosSet.roomSize, camPosSet.normalSize, 0.5f));
                if (camPosSet == null)
                    return;*/
            }
            /*if (!outRoomBounds)
            {
                StartCoroutine(CameraSizeShift(camPosSet.normalSize, camPosSet.roomSize, 0.5f));
            }*/
        }
    }

    /*IEnumerator CameraSizeShift(float a, float b, float time)
    {
        float i = 0.0f;
        float rate = (1.0f / time) * lerpSpeed;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            Camera.main.orthographicSize = Mathf.Lerp(a, b, i);
            yield return new WaitForSeconds(0.5f);
        }
    }*/

    void AspectRatioBoxChange() //The size of the camera projection rect (orthographic size) = 14
    {
        //16:10 ratio
        if(Camera.main.aspect >= (1.6f) && Camera.main.aspect < 1.7f)
            cameraBox.size = new Vector2(aspectX1610, aspectY);

        //16:9 ratio
        if (Camera.main.aspect >= (1.7f) && Camera.main.aspect < 1.8f)
            cameraBox.size = new Vector2(aspectX169, aspectY);

        //5:4 ratio
        if (Camera.main.aspect >= (1.25f) && Camera.main.aspect < 1.3f)
            cameraBox.size = new Vector2(aspectX54, aspectY);

        //4:3 ratio
        if (Camera.main.aspect >= (1.3f) && Camera.main.aspect < 1.4f)
            cameraBox.size = new Vector2(aspectX43, aspectY);

        //3:2 ratio
        if (Camera.main.aspect >= (1.5f) && Camera.main.aspect < 1.6f)
            cameraBox.size = new Vector2(aspectX32, aspectY);
    } 

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 0.4778554f, 1, 0.3f);
        Gizmos.DrawCube(focusArea.center, focusAreaSize);
    }

    struct FocusArea
    {
        public Vector2 center;
        public Vector2 velocity;
        float left, right;
        float top, bottom;

        public FocusArea(Bounds targetBounds, Vector2 size)
        {
            left = targetBounds.center.x - size.x / 2;
            right = targetBounds.center.x + size.x / 2;
            bottom = targetBounds.min.y;
            top = targetBounds.min.y + size.y;

            velocity = Vector2.zero;
            center = new Vector2((left + right) / 2, (top + bottom) / 2);
        }

        public void Update(Bounds targetBounds)
        {
            //Moving Camera on the X.
            float shiftX = 0;
            if (targetBounds.min.x < left)
                shiftX = targetBounds.min.x - left;
            else if (targetBounds.max.x > right)
                shiftX = targetBounds.max.x - right;
            left += shiftX;
            right += shiftX;

            //Moving Camera on the Y.
            float shiftY = 0;
            if (targetBounds.min.y < bottom)
                shiftY = targetBounds.min.y - bottom;
            else if (targetBounds.max.y > top)
                shiftY = targetBounds.max.y - top;
            top += shiftY;
            bottom += shiftY;
            center = new Vector2((left + right) / 2, (top + bottom) / 2);
            velocity = new Vector2(shiftX, shiftY);
        }
    }
}
