using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrashManager : MonoBehaviour
{
    public UnityEvent trashClean;


    [SerializeField] private float maxGrabDistance;
    [SerializeField] private LayerMask interactionLayer;

    [SerializeField] List<GameObject> trashs = new List<GameObject>();
    List<GameObject> currentTrashs = new List<GameObject>();
    private bool cleaningMode = false;

    private Camera mainCamera;
    //─────────────────────────────────────────────
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

        if (Input.GetMouseButtonDown(0))
            //if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = mainCamera.ViewportPointToRay(Vector3.one * 0.5f);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, maxGrabDistance, interactionLayer, QueryTriggerInteraction.Ignore))
            {
                if (currentTrashs.Contains(hit.transform.gameObject))
                {
                    bool hasremove = currentTrashs.Remove(hit.transform.gameObject);
                    hit.transform.gameObject.SetActive(false);
                }
            }
        }
    }
    public void GenerateTrash()
    {
        currentTrashs.Clear();
        cleaningMode = true;

        do
        {
            //currentTrashs.Clear();
            foreach (var item in trashs)
            {
                if (Random.Range(0, 2) == 0)
                {
                    item.SetActive(true);
                        currentTrashs.Add(item);
                }
                else
                {
                    item.SetActive(false);
                }
            }
        } while (currentTrashs.Count < 2);
    }

}
