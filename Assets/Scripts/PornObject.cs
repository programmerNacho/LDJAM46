using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LDJAM46
{
    [System.Serializable]
    public class PornObjectStats
    {
        public PornCategory category;
        public float percentageBonus;
    }

    [System.Serializable]
    public class PornObjectInfo
    {
        public List<PornObjectStats> categoryPercentages = new List<PornObjectStats>();
    }

    public class PornObject : MonoBehaviour
    {
        public PornObjectInfo pornObjectInfo;
    }
}
