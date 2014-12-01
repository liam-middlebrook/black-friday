using UnityEngine;
using System.Collections;

public class FlowFieldActor : MonoBehaviour
{
    FlowFieldManager manager;

    float lastCall = 0.0f;
    // Use this for initialization
    void Start()
    {
        manager = GameObject.Find("FlowField").GetComponent<FlowFieldManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastCall > 0.05f)
        {
            lastCall = Time.time;
            Vector2 position2D = new Vector2(this.transform.position.x, this.transform.position.z);
            Vector2 force2D = manager.GetForce2D(position2D);
            Debug.Log(force2D);
            this.rigidbody.AddForce(new Vector3(force2D.x, 0, force2D.y));
        }
    }
}
