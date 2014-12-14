using UnityEngine;
using System.Collections;

public class RaycastAvoidWall : MonoBehaviour
{
    // The distance to cast the forward ray
    public float rayDistance = 2.0f;

    // The distance to cast the side rays
    public float sideRayDistance = 1.5f;

    // The force amount to push the gameobject
    // away from the walls
    public float forceAmt = 15.0f;

    // Update is called once per frame
    void Update()
    {

        Vector3 steeringForce = Vector3.zero;

        // Draw all the rays
        Debug.DrawRay(this.transform.position, this.rigidbody.velocity.normalized * rayDistance, Color.red);
        Debug.DrawRay(this.transform.position, Vector3.Project(this.rigidbody.velocity, new Vector3(rayDistance, 0, 0)).normalized * sideRayDistance, Color.green);
        Debug.DrawRay(this.transform.position, Vector3.Project(this.rigidbody.velocity, new Vector3(0, 0, rayDistance)).normalized * sideRayDistance, Color.blue);

        // Create an empty object to store the hit data
        RaycastHit hit;

        // Calculate out the vectors for the left and right rays
        Vector3 leftRayVector = Vector3.Project(this.rigidbody.velocity, new Vector3(rayDistance, 0, 0));
        Vector3 rightRayVector = Vector3.Project(this.rigidbody.velocity, new Vector3(0, 0, rayDistance));

        // Raycast the front ray for collisions
        if (Physics.Raycast(new Ray(this.transform.position, this.rigidbody.velocity), out hit, rayDistance) && hit.collider.gameObject.tag == "Wall")
        {
            steeringForce -= this.rigidbody.velocity.normalized;
        }

        // Raycast on all orthoganal sides and check for collisions
        #region AuxillaryRays

        if (Physics.Raycast(new Ray(this.transform.position, leftRayVector), out hit, sideRayDistance) && hit.collider.gameObject.tag == "Wall")
        {
            steeringForce -= leftRayVector.normalized;
        }

        if (Physics.Raycast(new Ray(this.transform.position, rightRayVector), out hit, sideRayDistance) && hit.collider.gameObject.tag == "Wall")
        {
            steeringForce -= rightRayVector.normalized;
        }

        if (Physics.Raycast(new Ray(this.transform.position, -leftRayVector), out hit, sideRayDistance) && hit.collider.gameObject.tag == "Wall")
        {
            steeringForce -= -leftRayVector.normalized;
        }

        if (Physics.Raycast(new Ray(this.transform.position, -rightRayVector), out hit, sideRayDistance) && hit.collider.gameObject.tag == "Wall")
        {
            steeringForce -= -rightRayVector.normalized;
        }

        #endregion

        // Add the force to steer the gameobject away from the rays
        this.rigidbody.AddForce(steeringForce * forceAmt);
    }
}
