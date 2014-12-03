using UnityEngine;
using System.Collections;

public class LeaderManager : MonoBehaviour
{
    public GameObject leader = null;

    public GameObject Leader
    {
        get { return leader; }
        set { leader = value; }
    }
}
