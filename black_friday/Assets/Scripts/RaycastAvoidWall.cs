using UnityEngine;
using System.Collections;

public class RaycastAvoidWall : MonoBehaviour
{

    public float rayDistance = 2.0f;
    public float sideRayDistance = 1.5f;

    public float forceAmt = 15.0f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Vector3 steeringForce = Vector3.zero;
        Debug.DrawRay(this.transform.position, this.rigidbody.velocity.normalized * rayDistance, Color.red);

        Debug.DrawRay(this.transform.position, Vector3.Project(this.rigidbody.velocity, new Vector3(rayDistance, 0, 0)).normalized * sideRayDistance, Color.green);
        Debug.DrawRay(this.transform.position, Vector3.Project(this.rigidbody.velocity, new Vector3(0, 0, rayDistance)).normalized * sideRayDistance, Color.blue);

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
            steeringForce -= leftRay.normalized;
        }
        if (Physics.Raycast(new Ray(this.transform.position, rightRay), out hit, sideRayDistance) && hit.collider.gameObject.tag == "Wall")
        {
            //Debug.Log("Ray hit");
            steeringForce -= rightRay.normalized;
        }
        if (Physics.Raycast(new Ray(this.transform.position, -leftRay), out hit, sideRayDistance) && hit.collider.gameObject.tag == "Wall")
        {
            //Debug.Log("Ray hit");
            steeringForce -= -leftRay.normalized;
        }
        if (Physics.Raycast(new Ray(this.transform.position, -rightRay), out hit, sideRayDistance) && hit.collider.gameObject.tag == "Wall")
        {
            //Debug.Log("Ray hit");
            steeringForce -= -rightRay.normalized;
        }

        this.rigidbody.AddForce(steeringForce * forceAmt);
    }
}
