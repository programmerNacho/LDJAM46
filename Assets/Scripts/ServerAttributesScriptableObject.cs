using UnityEngine;

namespace LDJAM46
{
    [CreateAssetMenu(fileName = "ServerAttributes", menuName = "Server/Attributes", order = 1)]
    public class ServerAttributesScriptableObject : ScriptableObject, IServerAttributes
    {
        [SerializeField]
        private float maxVisualizationsPerSecond;
        [SerializeField]
        private float maxTemperature;
        [SerializeField]
        private float minTemperature;
        [SerializeField]
        private float maxTemperatureRaiseVelocity;
        [SerializeField]
        private float maxTemperatureDecreaseVelocity;

        public float MaxVisualizationsPerSecond
        {
            get
            {
                return maxVisualizationsPerSecond;
            }

            private set
            {
                maxVisualizationsPerSecond = value;
            }
        }
        public float MaxTemperature
        {
            get
            {
                return maxTemperature;
            }

            private set
            {
                maxTemperature = value;
            }
        }
        public float MinTemperature
        {
            get
            {
                return minTemperature;
            }

            private set
            {
                minTemperature = value;
            }
        }
        public float MaxTemperatureRaiseVelocity
        {
            get
            {
                return maxTemperatureRaiseVelocity;
            }

            private set
            {
                maxTemperatureRaiseVelocity = value;
            }
        }
        public float MaxTemperatureDecreaseVelocity
        {
            get
            {
                return maxTemperatureDecreaseVelocity;
            }

            private set
            {
                maxTemperatureDecreaseVelocity = value;
            }
        }
    }
}
