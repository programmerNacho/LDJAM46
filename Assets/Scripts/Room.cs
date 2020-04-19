using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    [Header("Doors")]
    [SerializeField] Animator lDoor;
    [SerializeField] Animator rDoor;
    [SerializeField] Alarm alarm;

    [Header("Timers")]
    [SerializeField] float TimeFilming;
    [SerializeField] float TimeGiveMe;
    float currentTime;

    // -- Variables --
    int estado;
    int contadorObjeto = 0;

    private void Start()
    {
        currentTime = TimeFilming;
    }

    private void Update()
    {
        if (estado == 0 || estado == 1)
            UpdateTime();
        Debug.Log(currentTime);

        if (Input.GetKeyDown(KeyCode.Keypad1))
            State(1);
        else if (Input.GetKeyDown(KeyCode.Keypad2))
            State(2);
        else if (Input.GetKeyDown(KeyCode.Keypad0))
            State(0);
    }
    #region █ MAIN █
    private void State(int _stat)
    {
        switch (_stat)
        {
            case 0:             // Filming
                estado = 0;
                currentTime = TimeFilming;
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
            case 2:             // Clean
                estado = 2;
                OpenDoor();
                break;
            default:
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
        alarm.Play();
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

}
