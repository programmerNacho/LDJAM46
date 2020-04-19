using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace LDJAM46
{
    public class ServerStateCanvas : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI categoryText;
        [SerializeField]
        private TextMeshProUGUI temperatureText;
        [SerializeField]
        private TextMeshProUGUI changeTemperatureText;
        [SerializeField]
        private TextMeshProUGUI visualizationsText;
        [SerializeField]
        private TextMeshProUGUI totalVisualizationsText;

        private Server server;

        private void Start()
        {
            server = GetComponent<Server>();
            categoryText.text = "Category: " + server.PornCategory;
        }

        private void Update()
        {
            temperatureText.text = "Temperature: " + server.Temperature.ToString("F2");
            changeTemperatureText.text = "Temperature Change: " + server.TemperatureChangePerSecond.ToString("F2");
            visualizationsText.text = "Visualizations Per Second: " + Mathf.RoundToInt(server.VisualizationsPerSecond);
            totalVisualizationsText.text = "Total Visualizations: " + server.TotalVisualizations;
        }
    }
}
