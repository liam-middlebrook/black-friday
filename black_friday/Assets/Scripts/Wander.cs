using UnityEngine;
using System.Collections;

public class Wander : MonoBehaviour
{
    public enum Direction
    {
        NORTH,
        SOUTH,
        EAST,
        WEST,
    }

    System.Collections.Generic.Dictionary<Direction, Vector3> directionValues = new System.Collections.Generic.Dictionary<Direction, Vector3>();

    public Direction currentDirection;

    public float sameDirection = 0.35f;
    public float oppisiteDirection = 0.5f;
    public float orthoDirection = 0.75f;

    void Start()
    {
        directionValues[Direction.NORTH] = Vector3.forward;
        directionValues[Direction.SOUTH] = Vector3.back;
        directionValues[Direction.EAST] = Vector3.right;
        directionValues[Direction.WEST] = Vector3.left;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 steeringForce = Vector3.zero;
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

        this.rigidbody.AddForce(steeringForce);
    }
}
