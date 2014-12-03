using UnityEngine;
using System.Collections;

public class Follower : MonoBehaviour
{
    private LeaderManager leaderManager;
    public float followSpeed = 5.0f;
    
    public float followRadius = 2.5f;

    // Use this for initialization
    void Start()
    {
        leaderManager = GameObject.Find("LeaderManager").GetComponent<LeaderManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forceVector;

        forceVector = leaderManager.Leader.transform.position - this.transform.position;

        float dist = forceVector.magnitude;

        forceVector.Normalize();
        
        Vector3 forceDir = forceVector;

        forceVector *= followSpeed;

        if (dist <= followRadius)
        {
            forceVector -= forceDir * (followRadius / dist) * followSpeed;
        }
        this.rigidbody.AddForce(forceVector);
    }
}
