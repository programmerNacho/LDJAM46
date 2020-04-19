using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ObjectEvent : UnityEvent<Collider>
{
}
public class EV_DoorObjectDetect : MonoBehaviour
{
    
    public ObjectEvent objectDetected;
    public UnityEvent onLeaveRoom;
    private Room room;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            objectDetected.Invoke(other);
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
