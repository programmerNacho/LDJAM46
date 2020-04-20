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

        //if (Input.GetMouseButtonDown(0))
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = mainCamera.ViewportPointToRay(Vector3.one * 0.5f);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, maxGrabDistance, interactionLayer, QueryTriggerInteraction.Ignore))
            {
                if (currentTrashs.Contains(hit.transform.gameObject))
                {
                    bool hasremove = currentTrashs.Remove(hit.transform.gameObject);
                    hit.transform.gameObject.SetActive(false);
                    Debug.Log("=== " + hasremove);
                }
            }
        }
    }
    public void GenerateTrash()
    {
        currentTrashs.Clear();
        cleaningMode = true;

        //currentTrashs.Add(trashs[0]);
        //currentTrashs.Add(trashs[1]);
        //trashs[2].SetActive(false);
        //trashs[3].SetActive(false);
        //trashs[4].SetActive(false);
        //trashs[5].SetActive(false);
        //trashs[6].SetActive(false);
        //trashs[7].SetActive(false);
        //trashs[8].SetActive(false);
        //trashs[9].SetActive(false);

        do
        {
            //currentTrashs.Clear();
            foreach (var item in trashs)
            {
                if (Random.Range(0, 2) == 0)
                {
                    item.SetActive(true);
                    //if (!currentTrashs.Contains(item))
                    //{
                        currentTrashs.Add(item);
                    //}
                }
                else
                {
                    item.SetActive(false);
                }
            }
        } while (currentTrashs.Count < 2);
        //} while (currentTrashs.Count < 2 || currentTrashs.Count > 8);
    }

}
