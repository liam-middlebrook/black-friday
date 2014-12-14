using UnityEngine;
using System.Collections;

public class Separation : MonoBehaviour
{
    public float forceAmt = 6.0f;
    // Use this for initialization
    void Start()
    {
        SphereCollider trigger = this.gameObject.AddComponent<SphereCollider>();
        trigger.isTrigger = true;

        trigger.radius = 2.0f;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag != "Separate")
            return;

        Vector3 diff = other.transform.position - this.transform.position;
        diff.y = 0;
        diff.Normalize();

        other.rigidbody.AddForce(diff * forceAmt);
        //this.rigidbody.AddForce(-diff * forceAmt);
    }
}
