using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    [SerializeField] List<GameObject> spawnList;

    public void RespawnObject(GameObject _object)
    {
        _object.transform.position = spawnList[Random.Range(0,spawnList.Count-1)].transform.position;
        _object.SetActive(true);
        Debug.Log("Respawn");
    }
}
