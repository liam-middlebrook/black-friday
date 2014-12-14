using UnityEngine;
using System.Collections;

public class Follower : MonoBehaviour
{
    // A local instance of the leader manager to keep track of who to follow
    private LeaderManager leaderManager;

    // The speed at which to follow the leader
    public float followSpeed = 8.0f;
    
    // The ideal distance to follow the leader at
    public float followRadius = 5.0f;

    // Use this for initialization
    void Start()
    {
        leaderManager = GameObject.Find("LeaderManager").GetComponent<LeaderManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // If the roombas are disabled do nothing
        if (!Wander.doWander)
            return;
        
        // Get the vector towards the leader
        Vector3 forceVector;
        forceVector = leaderManager.Leader.transform.position - this.transform.position;
        forceVector.Normalize();
        
        // Multiply by the follow speed and apply it as a force
        forceVector *= followSpeed;
        this.rigidbody.AddForce(forceVector);
    }
}
