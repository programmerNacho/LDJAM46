using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
        categoryText.text = "Category: " + server.pornCategory;
    }

    private void Update()
    {
        temperatureText.text = "Temperature: " + server.CurrentTemperature.ToString("F2");
        changeTemperatureText.text = "Chan. Vel. Temp: " + server.CurrentTemperatureChangeVelocity.ToString("F2");
        visualizationsText.text = "Visualizations Per Second: " + server.CurrentVisualizationPerSecond;
        totalVisualizationsText.text = "Total Visualizations: " + server.CurrentTotalVisualizationInt;
    }
}
