using UnityEngine;
using System.Collections;

public class Wander : MonoBehaviour
{
    // The possible cardinal directions for movement
    public enum Direction
    {
        NORTH,
        SOUTH,
        EAST,
        WEST,
    }

    // A dictionary the stores the vectors associated to each direction
    System.Collections.Generic.Dictionary<Direction, Vector3> directionValues = new System.Collections.Generic.Dictionary<Direction, Vector3>();

    // The direction the wanderer is currently moving in
    public Direction currentDirection;

    // The chance of the wanderer keeping the same direction
    public float sameDirection = 0.35f;

    // The chance of the wanderer moving the oppisite direction
    public float oppisiteDirection = 0.5f;

    // The chance of the wanderer moving to a sideways direction
    public float orthoDirection = 0.75f;

    // The speed at which to wander
    public float wanderSpeed = 10.0f;

    // The distance to trigger leader selection
    public float leaderDist = 5.0f;

    // The amount of wanderers needed to choose a leader
    public int numForLeader = 3;

    // A timer to delay the wandering
    private float wanderTimer;
    private float wanderTime = 0.1f;

    // Whether or not the wanderers are activated
    public static bool doWander = false;

    void Start()
    {
        // Set the direction vector values in the dictionary
        directionValues[Direction.NORTH] = Vector3.forward;
        directionValues[Direction.SOUTH] = Vector3.back;
        directionValues[Direction.EAST] = Vector3.right;
        directionValues[Direction.WEST] = Vector3.left;

        // Add this to the list of wanderers
        LeaderManager.wanderers.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        // If wandering is disabled do nothing
        if (!doWander)
            return;

        Vector3 steeringForce = Vector3.zero;
        wanderTimer += Time.deltaTime;

        #region WanderLogic

        // Only change direction so many times per second
        if (wanderTimer >= wanderTime)
        {
            wanderTimer = 0.0f;

            switch (currentDirection)
            {
                case Direction.NORTH:
                    if (Random.value < sameDirection)
                    {
                        steeringForce += directionValues[currentDirection = Direction.NORTH];
                    }
                    else if (Random.value < oppisiteDirection)
                    {
                        steeringForce += directionValues[currentDirection = Direction.SOUTH];
                    }
                    else if (Random.value < orthoDirection)
                    {
                        steeringForce += directionValues[currentDirection = Direction.EAST];
                    }
                    else
                    {
                        steeringForce += directionValues[currentDirection = Direction.WEST];
                    }
                    break;
                case Direction.SOUTH:
                    if (Random.value < sameDirection)
                    {
                        steeringForce += directionValues[currentDirection = Direction.SOUTH];
                    }
                    else if (Random.value < oppisiteDirection)
                    {
                        steeringForce += directionValues[currentDirection = Direction.NORTH];
                    }
                    else if (Random.value < orthoDirection)
                    {
                        steeringForce += directionValues[currentDirection = Direction.EAST];
                    }
                    else
                    {
                        steeringForce += directionValues[currentDirection = Direction.WEST];
                    }
                    break;
                case Direction.EAST:
                    if (Random.value < sameDirection)
                    {
                        steeringForce += directionValues[currentDirection = Direction.EAST];
                    }
                    else if (Random.value < oppisiteDirection)
                    {
                        steeringForce += directionValues[currentDirection = Direction.WEST];
                    }
                    else if (Random.value < orthoDirection)
                    {
                        steeringForce += directionValues[currentDirection = Direction.NORTH];
                    }
                    else
                    {
                        steeringForce += directionValues[currentDirection = Direction.SOUTH];
                    }
                    break;
                case Direction.WEST:
                    if (Random.value < sameDirection)
                    {
                        steeringForce += directionValues[currentDirection = Direction.WEST];
                    }
                    else if (Random.value < oppisiteDirection)
                    {
                        steeringForce += directionValues[currentDirection = Direction.EAST];
                    }
                    else if (Random.value < orthoDirection)
                    {
                        steeringForce += directionValues[currentDirection = Direction.NORTH];
                    }
                    else
                    {
                        steeringForce += directionValues[currentDirection = Direction.SOUTH];
                    }
                    break;
                default:
                    break;
            }
        }

        #endregion

        // If not wandering keep same direction
        else
        {
            steeringForce += directionValues[currentDirection];
        }

        // Scale up the steeringForce to the speed of wandering
        steeringForce *= wanderSpeed;

        // Apply the force to the wanderer
        this.rigidbody.AddForce(steeringForce);
    }

    public void FixedUpdate()
    {
        // If wandering is disabled do nothing
        if (!doWander)
            return;

        // Check how many possible leaders are within the leaderRadius
        int leadCount = 0;
        foreach (Wander w in LeaderManager.wanderers)
        {
            // If this is the wander skip it
            if (w.gameObject == this.gameObject)
                continue;

            // If the wanderer is within the distance it's a possible leader
            if (Vector3.Distance(this.transform.position, w.transform.position) < leaderDist)
            {
                ++leadCount;
            }
        }

        // If there are enough wanderers choose a leader
        if (leadCount >= numForLeader)
        {
            GameObject.Find("LeaderManager").GetComponent<LeaderManager>().AssignLeader(this.gameObject);
        }
    }
}
