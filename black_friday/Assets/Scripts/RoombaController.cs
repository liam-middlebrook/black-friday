using UnityEngine;
using System.Collections;

public class RoombaController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Wander.doWander = !Wander.doWander;
            Debug.Log(Wander.doWander);
        }
    }
}
