using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class NG_PlatformController : NG_RaycastController
{
    public LayerMask passengerMask;

    public Vector3[] localWaypoints;
    Vector3[] globalWaypoints;

    public float speed;
    public bool cyclic;
    public bool reversed; //Make the platform move from the top of the list of waypoints to the bottom -NG
    public float waitTime;
    [Range(0, 2)] public float easeAmount;

    [SerializeField] float platformX;
    [SerializeField] float platformY;

    [SerializeField] int fromWaypointIndex;
    [SerializeField] int toWaypointIndex;
    float percentBetweenWaypoints;
    float nextMoveTime;

    List<PassengerMovement> passengerMovement;
    Dictionary<Transform, NG_CharacterController2D> passengerDictionary = new Dictionary<Transform, NG_CharacterController2D>();
    List<GameObject> railList = new List<GameObject>();
    bool maxRailsReached;

    [SerializeField] GameObject rail;
    [SerializeField] float railPieces;
    private Vector3 railPos;

    // Use this for initialization
    public override void Start()
    {
        base.Start();

        globalWaypoints = new Vector3[localWaypoints.Length];
        for (int i = 0; i < localWaypoints.Length; i++)
        {
            globalWaypoints[i] = localWaypoints[i] + transform.position;
        }
        RailSpawn();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateRaycastOrigins();

        Vector3 velocity = CalculatePlatformMovement();

        //CalculatePassengerMovement(velocity);

        //MovePassengers(true);
        transform.Translate(velocity);
        //MovePassengers(false);

        if (railList.Count == railPieces * localWaypoints.Length)
            maxRailsReached = true;
    }

    float Ease(float x)
    {
        float a = easeAmount + 1;
        return Mathf.Pow(x, a) / (Mathf.Pow(x, a) + Mathf.Pow(1 - x, a));
    }

    Vector3 CalculatePlatformMovement()
    {
        if (Time.time < nextMoveTime)
            return Vector3.zero;

        fromWaypointIndex %= globalWaypoints.Length;
        toWaypointIndex = (!reversed) ? (fromWaypointIndex + 1) % globalWaypoints.Length : (fromWaypointIndex) % globalWaypoints.Length - 1;
        if (reversed)
        {
            if (fromWaypointIndex <= 0 && reversed)
                toWaypointIndex = globalWaypoints.Length - 1;
            if (fromWaypointIndex == -1)
            {
                fromWaypointIndex = globalWaypoints.Length - 1;
                toWaypointIndex = globalWaypoints.Length - 2;
            }
        }

        float distanceBetweenWaypoints = Vector3.Distance(globalWaypoints[fromWaypointIndex], globalWaypoints[toWaypointIndex]);
        percentBetweenWaypoints += Time.deltaTime * speed / distanceBetweenWaypoints;
        percentBetweenWaypoints = Mathf.Clamp01(percentBetweenWaypoints);
        float easedPercentBetweenWaypoints = Ease(percentBetweenWaypoints);

        Vector3 newPos = Vector3.Lerp(globalWaypoints[fromWaypointIndex], globalWaypoints[toWaypointIndex], easedPercentBetweenWaypoints);

        if (percentBetweenWaypoints >= 1)
        {
            percentBetweenWaypoints = 0;
            if (!reversed)
                fromWaypointIndex++;
            else
                fromWaypointIndex--;

            if (!cyclic)
            {
                if (fromWaypointIndex >= globalWaypoints.Length - 1)
                {
                    fromWaypointIndex = 0;
                    System.Array.Reverse(globalWaypoints);
                }
            }

            nextMoveTime = Time.time + waitTime;
        }

        RailSpawn();

        return newPos - transform.position;
    }

    void MovePassengers(bool beforeMovePlatform)
    {
        foreach (PassengerMovement passenger in passengerMovement)
        {
            if (!passengerDictionary.ContainsKey(passenger.transform))
            {
                passengerDictionary.Add(passenger.transform, passenger.transform.GetComponent<NG_CharacterController2D>());
                Debug.Log("Adding passenger");
            }

            if (passenger.moveBeforePlatform == beforeMovePlatform)
                passengerDictionary[passenger.transform].Move(passenger.velocity, passenger.standingOnPlatform);
        }
    }

    void CalculatePassengerMovement(Vector3 velocity)
    {
        HashSet<Transform> movedPassengers = new HashSet<Transform>();
        passengerMovement = new List<PassengerMovement>();

        float directionX = Mathf.Sign(velocity.x);
        float directionY = Mathf.Sign(velocity.y);

        platformX = directionX;
        platformY = directionY;

        //Vertically moving platform
        if (velocity.y != 0)
        {
            float rayLength = Mathf.Abs(velocity.y) + (skinWidth * 6);

            for (int i = 0; i < verticalRayCount; i++)
            {
                Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
                rayOrigin += Vector2.right * (verticalRaySpacing * i);
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, passengerMask);

                Debug.DrawRay(rayOrigin, Vector2.up * directionY, Color.red);

                if (hit)
                {
                    if (!movedPassengers.Contains(hit.transform))
                    {
                        movedPassengers.Add(hit.transform);
                        float pushX = (directionY == 1) ? velocity.x : 0;
                        float pushY = velocity.y - (hit.distance - skinWidth) * directionY;

                        passengerMovement.Add(new PassengerMovement(hit.transform, new Vector3(pushX, pushY), directionY == 1, true));
                    }
                }
            }
        }

        //Horizontally moving platform
        if (velocity.x != 0)
        {
            float rayLength = Mathf.Abs(velocity.x) + (skinWidth);

            for (int i = 0; i < horizontalRayCount; i++)
            {
                Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
                rayOrigin += Vector2.up * (horizontalRaySpacing * i);
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, passengerMask);

                Debug.DrawRay(rayOrigin, Vector2.right * directionX, Color.red);

                if (hit && hit.distance != 0)
                {
                    if (!movedPassengers.Contains(hit.transform))
                    {
                        movedPassengers.Add(hit.transform);
                        float pushX = velocity.x - (hit.distance - skinWidth) * directionX;
                        float pushY = -skinWidth;

                        passengerMovement.Add(new PassengerMovement(hit.transform, new Vector3(pushX, pushY), true, false));
                    }
                }
            }
        }

        //Passenger on top of a horizontally or downward moving platform
        if (directionY == -1 || velocity.y == 0 && velocity.x != 0)
        {
            float rayLength = (skinWidth * 2);

            for (int i = 0; i < verticalRayCount; i++)
            {
                Vector2 rayOrigin = raycastOrigins.topLeft + Vector2.right * (verticalRaySpacing * i);
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up, rayLength, passengerMask);

                Debug.DrawRay(rayOrigin, Vector2.up, Color.red);

                if (hit)
                {
                    if (!movedPassengers.Contains(hit.transform))
                    {
                        movedPassengers.Add(hit.transform);
                        float pushX = velocity.x;
                        float pushY = velocity.y - Mathf.Pow((hit.distance - skinWidth), 2);

                        passengerMovement.Add(new PassengerMovement(hit.transform, new Vector3(pushX, pushY), true, false));
                    }
                }
            }
        }

        if (directionY == -1 && velocity.x != 0)
        {
            float rayLength = (skinWidth * 2);

            for (int i = 0; i < verticalRayCount; i++)
            {
                Vector2 rayOrigin = raycastOrigins.topLeft + Vector2.right * (verticalRaySpacing * i);
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up, rayLength, passengerMask);

                Debug.DrawRay(rayOrigin, Vector2.up, Color.red);

                if (hit)
                {
                    if (!movedPassengers.Contains(hit.transform))
                    {
                        movedPassengers.Add(hit.transform);
                        float pushX = velocity.x - (hit.distance - skinWidth) * directionX;
                        float pushY = velocity.y - (hit.distance - skinWidth);

                        passengerMovement.Add(new PassengerMovement(hit.transform, new Vector3(pushX, pushY), true, true));
                    }
                }
            }
        }
    }

    struct PassengerMovement
    {
        public Transform transform;
        public Vector3 velocity;
        public bool standingOnPlatform;
        public bool moveBeforePlatform;

        public PassengerMovement(Transform _transform, Vector3 _velocity, bool _standingOnPlatform, bool _moveBeforePlatform)
        {
            transform = _transform;
            velocity = _velocity;
            standingOnPlatform = _standingOnPlatform;
            moveBeforePlatform = _moveBeforePlatform;
        }
    }

    private void OnDrawGizmos()
    {
        if (localWaypoints != null)
        {
            Gizmos.color = Color.red;
            float size = 0.3f;

            for (int i = 0; i < localWaypoints.Length; i++)
            {
                Vector3 globalWaypointPos = (Application.isPlaying) ? globalWaypoints[i] : localWaypoints[i] + transform.position;
                Gizmos.DrawLine(globalWaypointPos - Vector3.up * size, globalWaypointPos + Vector3.up * size);
                Gizmos.DrawLine(globalWaypointPos - Vector3.left * size, globalWaypointPos + Vector3.left * size);
            }
        }
    }

    public void RailSpawn()
    {
        if (percentBetweenWaypoints == 0)
        {
            for (int i = 0; i < railPieces; i++)
            {
                if (rail == null)
                    break;
                else
                {
                    if (fromWaypointIndex != globalWaypoints.Length && fromWaypointIndex != globalWaypoints.Length - 1)
                        railPos = Vector3.Lerp(globalWaypoints[fromWaypointIndex + 1], globalWaypoints[toWaypointIndex], i * 0.25f);
                    else if (fromWaypointIndex == globalWaypoints.Length - 1)
                        railPos = Vector3.Lerp(globalWaypoints[0], globalWaypoints[toWaypointIndex], i * 0.25f);
                    else if (fromWaypointIndex == globalWaypoints.Length)
                        railPos = Vector3.Lerp(globalWaypoints[1], globalWaypoints[toWaypointIndex], i * 0.25f);
                    if (!maxRailsReached)
                    {
                        Instantiate(rail, railPos, Quaternion.identity);
                        railList.Add(rail);
                    }
                    else
                        break;
                }
            }
        }
    }
}
