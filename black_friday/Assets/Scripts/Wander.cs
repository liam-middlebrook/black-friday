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

    public float rayDistance = 1.0f;
    public float sideRayDistance = 0.75f;

    public float wanderSpeed = 10.0f;
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

        steeringForce *= wanderSpeed;
        Debug.DrawRay(this.transform.position, this.rigidbody.velocity.normalized * rayDistance, Color.red);

        Debug.DrawRay(this.transform.position, Vector3.Project(this.rigidbody.velocity, new Vector3(rayDistance, 0, 0)).normalized * sideRayDistance, Color.green);
        Debug.DrawRay(this.transform.position, Vector3.Project(this.rigidbody.velocity, new Vector3(0, 0, rayDistance)).normalized * sideRayDistance, Color.green);

        RaycastHit hit;
        Vector3 leftRay = Vector3.Project(this.rigidbody.velocity, new Vector3(rayDistance, 0, 0));
        Vector3 rightRay = Vector3.Project(this.rigidbody.velocity, new Vector3(0, 0, rayDistance));
        if (Physics.Raycast(new Ray(this.transform.position, this.rigidbody.velocity), out hit, rayDistance) && hit.collider.gameObject.tag == "Wall")
        {
            //Debug.Log("Ray hit");
            steeringForce -= this.rigidbody.velocity.normalized;
        }
        if (Physics.Raycast(new Ray(this.transform.position, leftRay), out hit, sideRayDistance) && hit.collider.gameObject.tag == "Wall")
        {
            //Debug.Log("Ray hit");
            steeringForce -= Vector3.Project(this.rigidbody.velocity, new Vector3(rayDistance, 0, 0)).normalized;
        }
        if (Physics.Raycast(new Ray(this.transform.position, rightRay), out hit, sideRayDistance) && hit.collider.gameObject.tag == "Wall")
        {
            //Debug.Log("Ray hit");
            steeringForce -= Vector3.Project(this.rigidbody.velocity, new Vector3(0, 0, rayDistance)).normalized;
        }
        this.rigidbody.AddForce(steeringForce);
    }
}
