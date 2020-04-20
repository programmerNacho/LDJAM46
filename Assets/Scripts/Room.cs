using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using LDJAM46;

public class Room : MonoBehaviour
{

    [Header("Doors")]
    [SerializeField] Animator lDoor;
    [SerializeField] Animator rDoor;
    [SerializeField] Alarm alarm;
    [SerializeField] EV_DoorObjectDetect objectDetect;
    [SerializeField] TrashManager trashManager;

    [Header("Timers")]
    [SerializeField] float TimeFilmingMin;
    [SerializeField] float TimeFilmingMax;
    [SerializeField] float TimeGiveMe;
    float currentTime;
    float currentTimeObject;

    // -- Variables --
    public int estado;
    private int contadorObjeto = 0;
    private bool isCleaned = false;
    private RoomResultsManager roomResultManager;

    private void Start()
    {
        roomResultManager = GetComponent<RoomResultsManager>();
        currentTime = UnityEngine.Random.Range(TimeFilmingMin, TimeFilmingMax);

        objectDetect.objectDetected.AddListener(EventObjectIn);
        objectDetect.onLeaveRoom.AddListener(OnLeaveRoom);

        trashManager = GetComponent<TrashManager>();
        trashManager.trashClean.AddListener(EventClean);
    }

    private void Update()
    {
        if (estado == 0 || estado == 1)
            UpdateTime();

        if (Input.GetKeyDown(KeyCode.Keypad1))
            State(1);
        else if (Input.GetKeyDown(KeyCode.Keypad2))
            State(2);
        else if (Input.GetKeyDown(KeyCode.Keypad0))
            State(0);
    }
    #region █ MAIN █
    private void State(int _stat = -1)
    {
        switch (_stat)
        {
            case 0:             // Filming
                estado = 0;
                currentTime = UnityEngine.Random.Range(TimeFilmingMin, TimeFilmingMax);
                CloseAllDoors();
                break;
            case 1:             // Object
                estado = 1;
                if (contadorObjeto++ < 3)
                {
                    currentTime = TimeGiveMe;
                    OpenDoorHalf();
                }
                else
                    State(2);
                break;
            case 2:             // Clean Mode
                estado = 2;
                roomResultManager.CalculateResults();
                trashManager.GenerateTrash();
                isCleaned = false;
                OpenDoor();
                break;
            default:
                contadorObjeto = 0;
                isCleaned = false;
                State(0);
                break;
        }
    }
    #endregion
    #region █ Open/Close DOORS █
    private void CloseAllDoors()
    {
        alarm.Stop();
        lDoor.SetBool("isHalfOpen", false);
        lDoor.SetBool("isOpen", false);
        rDoor.SetBool("isHalfOpen", false);
        rDoor.SetBool("isOpen", false);
    }
    private void OpenDoor()
    {
        CloseAllDoors();
        alarm.PlayRed();
        lDoor.SetBool("isOpen", true);
        rDoor.SetBool("isOpen", true);
    }
    private void OpenDoorHalf()    {
        CloseAllDoors();
        alarm.Play();
        lDoor.SetBool("isHalfOpen", true);
        rDoor.SetBool("isHalfOpen", true);
    }
    #endregion
    #region █ TIMER █
    private void UpdateTime()
    {
        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            switch (estado)
            {
                // Countdown to Give Me
                case 0:
                    State(1);
                    break;
                // Countdown for Recive Object.
                case 1:
                    State(0);
                    break;
                default:
                    break;
            }
        }
    }
    #endregion
    #region █ EVENTS █
    private void EventObjectIn(Collider _value)
    {
        if (estado == 1)
        {
            Debug.Log("Objeto Dentro");
            roomResultManager.AddPornObjectInfo(_value.GetComponent<PornObject>().pornObjectInfo);
            Destroy(_value.gameObject, 2);
            State(0);
        }
    }
    public void EventClean()
    {
        isCleaned = true;
        Debug.Log("Habitación Limpia");
    }
    public void OnLeaveRoom()
    {
        Debug.Log(isCleaned);
        if (isCleaned)
        {
            State();
        }
    }
    #endregion
}
