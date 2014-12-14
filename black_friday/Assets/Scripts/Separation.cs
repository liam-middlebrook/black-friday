using UnityEngine;
using System.Collections;

public class Separation : MonoBehaviour
{
    // The amount of force to separate gameobjects by
    public float forceAmt = 6.0f;

    // Use this for initialization
    void Start()
    {
        //Add a sphere collider as a trigger to the seperator with a radius of 2
        SphereCollider trigger = this.gameObject.AddComponent<SphereCollider>();
        trigger.isTrigger = true;
        trigger.radius = 2.0f;
    }


    void OnTriggerStay(Collider other)
    {
        // If the gameobject that this collided with isn't another separating
        // body then ignore the collision
        if (other.gameObject.tag != "Separate")
            return;

        // Get the 2D distance between the game objects
        Vector3 diff = other.transform.position - this.transform.position;
        diff.y = 0;
        diff.Normalize();

        // Apply the force of separation
        other.rigidbody.AddForce(diff * forceAmt);
    }
}
