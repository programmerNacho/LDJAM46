using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LDJAM46
{
    public class Server : MonoBehaviour
    {
        public int TotalVisualizations { get; private set; }
        public float VisualizationsPerSecond { get; private set; }
        public float Temperature { get; private set; }
        public float TemperatureChangePerSecond { get; private set; }
        public bool IsBlocked { get; private set; }
        public PornCategory PornCategory
        {
            get
            {
                return pornCategory;
            }
        }

        [SerializeField]
        private PornCategory pornCategory;
        [SerializeField]
        private float initialVisualizationsPerSecond;
        [SerializeField]
        private ServerAttributesScriptableObject attributes;
        [SerializeField]
        private float blockCooldownTime;

        private float totalVisualizationsFraction;

        private ServerModificationManager modificationManager;
        private ServerModification modification;

        public void ChangeVisualizationsPerSecond(float visualizationsPerSecondChange)
        {
            if (!IsBlocked)
            {
                VisualizationsPerSecond += visualizationsPerSecondChange;
            }
            VisualizationsPerSecond = Mathf.Clamp(VisualizationsPerSecond, 0f, attributes.MaxVisualizationsPerSecond);
        }

        private void Start()
        {
            InitializeVariables();
        }

        private void InitializeVariables()
        {
            TotalVisualizations = 0;
            VisualizationsPerSecond = initialVisualizationsPerSecond;
            Temperature = attributes.MinTemperature;
            TemperatureChangePerSecond = 0f;
            IsBlocked = false;
            totalVisualizationsFraction = 0f;
            modificationManager = GetComponent<ServerModificationManager>();
        }

        private void Update()
        {
            modification = modificationManager.ProcessModifications();
            CalculateTemperatureChangePerSecond();
            CalculateTemperature();
            CheckTemperatureLimits();
            AddVisualizationsPerSecondToTotal();
        }

        private void CalculateTemperatureChangePerSecond()
        {
            TemperatureChangePerSecond = Mathf.Lerp(attributes.MaxTemperatureDecreaseVelocity, attributes.MaxTemperatureRaiseVelocity, VisualizationsPerSecond / attributes.MaxVisualizationsPerSecond);
            TemperatureChangePerSecond += modification.TemperatureChangePerSecondBoost;
        }

        private void CalculateTemperature()
        {
            Temperature += TemperatureChangePerSecond * Time.deltaTime;
            Temperature = Mathf.Clamp(Temperature, attributes.MinTemperature, attributes.MaxTemperature);
            Temperature += modification.InstantTemperatureChangeBoost;
        }

        private void CheckTemperatureLimits()
        {
            if(Temperature >= attributes.MaxTemperature)
            {
                Block();
            }
        }

        private void Block()
        {
            if(!IsBlocked)
            {
                VisualizationsPerSecond = 0f;
                StartCoroutine(BlockCooldown());
            }
        }

        private IEnumerator BlockCooldown()
        {
            IsBlocked = true;
            yield return new WaitForSeconds(blockCooldownTime);
            IsBlocked = false;
        }

        private void AddVisualizationsPerSecondToTotal()
        {
            totalVisualizationsFraction += VisualizationsPerSecond * Time.deltaTime;
            TotalVisualizations = Mathf.RoundToInt(totalVisualizationsFraction);
        }
    }
}
