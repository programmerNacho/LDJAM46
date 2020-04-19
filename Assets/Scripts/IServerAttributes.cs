namespace LDJAM46
{
    public interface IServerAttributes
    {
        float MaxVisualizationsPerSecond { get; }
        float MaxTemperature { get; }
        float MinTemperature { get; }
        float MaxTemperatureRaiseVelocity { get; }
        float MaxTemperatureDecreaseVelocity { get; }
    }
}

