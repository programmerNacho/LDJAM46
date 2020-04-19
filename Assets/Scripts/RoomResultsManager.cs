using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LDJAM46
{
    public class RoomResultsManager : MonoBehaviour
    {
        [SerializeField]
        private PornCategory roomType;
        [SerializeField]
        private float visualizationsPerSecondBase;

        private List<PornObjectInfo> pornObjectsInfo;

        private void Start()
        {
            pornObjectsInfo = new List<PornObjectInfo>();
        }

        public void AddPornObjectInfo(PornObjectInfo pornObjectInfo)
        {
            pornObjectsInfo.Add(pornObjectInfo);
        }

        public void CalculateResults()
        {
            float percentageBonus = 0f;
            for (int i = 0; i < pornObjectsInfo.Count; i++)
            {
                for (int j = 0; j < pornObjectsInfo[i].categoryPercentages.Count; j++)
                {
                    if (pornObjectsInfo[i].categoryPercentages[j].category == roomType)
                    {
                        percentageBonus += pornObjectsInfo[i].categoryPercentages[j].percentageBonus;
                    }
                }
            }

            pornObjectsInfo.Clear();

            float bonus = visualizationsPerSecondBase * percentageBonus;
            float result = visualizationsPerSecondBase + bonus;
            Debug.Log(result);
        }
    }
}
