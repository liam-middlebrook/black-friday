using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LeaderManager : MonoBehaviour
{
    public GameObject leader = null;

    public GameObject Leader
    {
        get { return leader; }
        set { leader = value; }
    }

    // A static list of all pathnodes that exist
    public static List<Wander> wanderers = new List<Wander>();

    public void AssignLeader(GameObject _leader)
    {
        leader = _leader;
        leader.renderer.material.color = Color.green;
        leader.GetComponent<Separation>().forceAmt *= 5;

        for (int i = 0; i < wanderers.Count; i++)
        {
            if (wanderers[i].gameObject != leader)
            {
                wanderers[i].gameObject.AddComponent<Follower>();
                Destroy(wanderers[i]);
                wanderers.RemoveAt(i--);
            }
        }
    }
}
