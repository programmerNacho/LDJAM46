using System.Collections;
using UnityEngine;

public class Server : MonoBehaviour
{
    [SerializeField]
    private int initialVisualizationPerSecond;
    [SerializeField]
    private float maxTemperature;
    [SerializeField]
    private float minTemperature;
    [SerializeField]
    private int maxVisualizationPerSecond;
    [SerializeField]
    private float maxTemperatureRaiseVelocity;
    [SerializeField]
    private float maxTemperatureDecreaseVelocity;
    [SerializeField]
    public PornCategory pornCategory;

    private int currentTotalVisualizationInt;
    public int CurrentTotalVisualizationInt
    {
        get
        {
            return currentTotalVisualizationInt;
        }
        private set
        {
            currentTotalVisualizationInt = value;
        }
    }

    private float currentTotalVisualizationFloat;
    public float CurrentTotalVisualizationFloat
    {
        get
        {
            return currentTotalVisualizationFloat;
        }
        private set
        {
            currentTotalVisualizationFloat = value;
        }
    }

    private float currentTemperature;
    public float CurrentTemperature
    {
        get
        {
            return currentTemperature;
        }
        private set
        {
            currentTemperature = value;
        }
    }

    private int currentVisualizationPerSecond;
    public int CurrentVisualizationPerSecond
    {
        get
        {
            return currentVisualizationPerSecond;
        }
        private set
        {
            currentVisualizationPerSecond = value;
        }
    }

    private float currentTemperatureChangeVelocity;
    public float CurrentTemperatureChangeVelocity
    {
        get
        {
            return currentTemperatureChangeVelocity;
        }
        private set
        {
            currentTemperatureChangeVelocity = value;
        }
    }

    private const float secondsBlocked = 10f;

    private void Start()
    {
        currentVisualizationPerSecond = initialVisualizationPerSecond;
    }

    private bool blocked;

    private void Update()
    {
        if(!blocked)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                currentVisualizationPerSecond += 200;
            }
            if (currentTemperature >= maxTemperature)
            {
                StartCoroutine(BlockState());
            }
        }

        currentTemperatureChangeVelocity = Mathf.Lerp(maxTemperatureDecreaseVelocity, maxTemperatureRaiseVelocity, (float)currentVisualizationPerSecond / maxVisualizationPerSecond);
        currentTemperature = Mathf.Clamp(currentTemperature + currentTemperatureChangeVelocity * Time.deltaTime, minTemperature, maxTemperature);
        currentVisualizationPerSecond = Mathf.Clamp(currentVisualizationPerSecond, 0, maxVisualizationPerSecond);
        currentTotalVisualizationFloat += currentVisualizationPerSecond * Time.deltaTime;
        currentTotalVisualizationInt = Mathf.CeilToInt(currentTotalVisualizationFloat);
    }

    private IEnumerator BlockState()
    {
        blocked = true;
        currentVisualizationPerSecond = 0;
        yield return new WaitForSeconds(secondsBlocked);
        blocked = false;
    }
}
