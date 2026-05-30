namespace CdbCalculator;

public class DynamicRates
{
    private const decimal MinRate = 0.0080m;
    private const decimal MaxRate = 0.0090m;
    private const int Magnitude = 10;
    private const decimal InitialRate = 0.0085m;
    private const decimal PercentDivisor = 100000m;

    private readonly Random _random = new();

    public decimal CurrentRate { get; private set; } = InitialRate;

    public decimal UpdateRate()
    {
        decimal distanceToMax = MaxRate - CurrentRate;
        decimal distanceToMin = CurrentRate - MinRate;
        
        decimal adjustment = (Math.Abs(distanceToMin) - Math.Abs(distanceToMax));

        int deltaMin = (int)(-10 - (adjustment * Magnitude));
        int deltaMax = (int)(10 - (adjustment * Magnitude));

        int trend = _random.Next(deltaMin, deltaMax + 1);

        CurrentRate += trend / PercentDivisor;

        return Math.Clamp(CurrentRate, MinRate, MaxRate);
    }
}