namespace CdbCalculator;

public class DynamicRates
{
    private const decimal MinRate = 0.0080m;
    private const decimal MaxRate = 0.0090m;
    private const int Magnitude = 3000;
    private const decimal InitialRate = 0.0085m;
    private const decimal PercentDivisor = 100000m;

    private readonly Random _random = new();

    public decimal CurrentRate { get; private set; } = InitialRate;

    public decimal Update()
    {
        decimal distanceToMax = MaxRate - CurrentRate;
        decimal distanceToMin = CurrentRate - MinRate;

        decimal adjustment = distanceToMin - distanceToMax;

        decimal min = (-10 - (adjustment * Magnitude));
        decimal max = (10 - (adjustment * Magnitude));

        int deltaMin = (int)Math.Round(min, MidpointRounding.AwayFromZero);
        int deltaMax = (int)Math.Round(max, MidpointRounding.AwayFromZero);

        int trend = _random.Next(deltaMin, deltaMax + 1);

        CurrentRate += trend / PercentDivisor;
        CurrentRate = Math.Clamp(CurrentRate, MinRate, MaxRate);
        return CurrentRate;
    }
}