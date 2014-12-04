using UnityEngine;
using System.Collections;

public class Follower : MonoBehaviour
{
    private LeaderManager leaderManager;
    public float followSpeed = 8.0f;
    
    public float followRadius = 5.0f;

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

        forceVector.Normalize();
        
        forceVector *= followSpeed;
        this.rigidbody.AddForce(forceVector);
    }
}
