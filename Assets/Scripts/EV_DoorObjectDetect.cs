using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EV_DoorObjectDetect : MonoBehaviour
{
    public UnityEvent objectDetected;
    public UnityEvent onLeaveRoom;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            Destroy(other.gameObject, 2);
            objectDetected.Invoke();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<FirstPersonAIO>())
        {
            onLeaveRoom.Invoke();
        }
    }
}
