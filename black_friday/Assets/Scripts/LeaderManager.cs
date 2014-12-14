using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LeaderManager : MonoBehaviour
{
    // The leader that all followers will follow
    private GameObject leader = null;

    public GameObject Leader
    {
        get { return leader; }
        set { leader = value; }
    }

    // A static list of all pathnodes that exist
    public static List<Wander> wanderers = new List<Wander>();

    // Assign a new leader to the pack
    // @param _leader The gameobject that will become the new leader
    public void AssignLeader(GameObject _leader)
    {
        // Set the new leader and make it green
        leader = _leader;
        leader.renderer.material.color = Color.green;

        // Make the leader have more powerful separation
        leader.GetComponent<Separation>().forceAmt *= 5;

        // Loop through all the wanderers
        for (int i = 0; i < wanderers.Count; i++)
        {
            // If this isn't the leader
            if (wanderers[i].gameObject != leader)
            {
                // Convert the wanderer into a follower and remove it from the list
                wanderers[i].gameObject.AddComponent<Follower>();
                Destroy(wanderers[i]);
                wanderers.RemoveAt(i--);
            }
        }
    }
}
