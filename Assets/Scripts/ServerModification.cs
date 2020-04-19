using UnityEngine;
using System;

namespace LDJAM46
{
    [Serializable]
    public class ServerModification
    {
        public float VisualizationsPerSecondBoost;
        public float MaxTemperatureBoost;
        public float MinTemperatureBoost;
        public float InstantTemperatureChangeBoost;
        public float TemperatureChangePerSecondBoost;
        public float BlockCooldownTimeBoost;
    }
}
