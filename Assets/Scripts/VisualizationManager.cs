﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LDJAM46
{
    public class VisualizationManager : MonoBehaviour
    {
        [SerializeField]
        private Server[] servers;
        [SerializeField]
        private float minTimeWithoutUpdating;
        [SerializeField]
        private float maxTimeWithoutUpdating;
        [SerializeField]
        private float minPenalizationVisualization;
        [SerializeField]
        private float maxPenalizationVisualization;

        private float[] timeWithoutUpdatingServer;

        private void Start()
        {
            timeWithoutUpdatingServer = new float[servers.Length];
        }

        public void AddVisualizationsPerSecond(PornCategory pornCategory, float visualizations)
        {
            for (int i = 0; i < servers.Length; i++)
            {
                if(servers[i].PornCategory == pornCategory)
                {
                    servers[i].ChangeVisualizationsPerSecond(visualizations);
                    timeWithoutUpdatingServer[i] = 0f;
                }
            }
        }

        private void Update()
        {
            for (int i = 0; i < timeWithoutUpdatingServer.Length; i++)
            {
                timeWithoutUpdatingServer[i] += Time.deltaTime;
            }
            for (int i = 0; i < timeWithoutUpdatingServer.Length; i++)
            {
                if(timeWithoutUpdatingServer[i] >= minTimeWithoutUpdating)
                {
                    float visualizationsChange = Mathf.Lerp(Mathf.Abs(minPenalizationVisualization), Mathf.Abs(maxPenalizationVisualization), (timeWithoutUpdatingServer[i] - minTimeWithoutUpdating) / maxTimeWithoutUpdating);
                    servers[i].ChangeVisualizationsPerSecond(-visualizationsChange * Time.deltaTime);
                }
            }

            bool lost = true;

            for (int i = 0; i < servers.Length; i++)
            {
                if(servers[i].VisualizationsPerSecond != 0f)
                {
                    lost = false;
                    break;
                }
            }

            if(lost)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
