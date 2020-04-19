﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrashManager : MonoBehaviour
{
    public UnityEvent trashClean;


    [SerializeField] private float maxGrabDistance;
    [SerializeField] private LayerMask interactionLayer;

    [SerializeField] List<GameObject> trashs;
                     List<GameObject> currentTrashs;
    private bool cleaningMode = false;

    private Camera mainCamera;
    private void Start()
    {
        mainCamera = Camera.main;
    }
    private void Update()
    {
        if (cleaningMode && currentTrashs.Count <= 0)
        {
            cleaningMode = false;
            trashClean.Invoke();
        }
    }
    private void FixedUpdate()
    {
        Ray ray = mainCamera.ViewportPointToRay(Vector3.one * 0.5f);
        RaycastHit hit;


        //if(Physics.Raycast(ray, out hit, maxGrabDistance, interactionLayer, QueryTriggerInteraction.Ignore))
        //{

        //}


        if (Input.GetKeyDown(KeyCode.F))
        {
            currentTrashs[currentTrashs.Count - 1].active = false;
            currentTrashs.RemoveAt(currentTrashs.Count - 1);
        }
    }
    public void GenerateTrash()
    {
        currentTrashs = new List<GameObject>();
        cleaningMode = true;

        do
        {
            foreach (var item in trashs)
            {
                if (Random.Range(0, 2) == 0)
                {
                    item.active = true;
                    currentTrashs.Add(item);
                }
                else
                    item.active = false;
            }
        } while (currentTrashs.Count < 2 || currentTrashs.Count > 8);
    }

}