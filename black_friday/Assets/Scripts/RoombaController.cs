using UnityEngine;
using System.Collections;

public class RoombaController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // If the user presses 'E' toggle the roomba's
        // activation state
        if(Input.GetKeyDown(KeyCode.E))
        {
            Wander.doWander = !Wander.doWander;
        }
    }
}
