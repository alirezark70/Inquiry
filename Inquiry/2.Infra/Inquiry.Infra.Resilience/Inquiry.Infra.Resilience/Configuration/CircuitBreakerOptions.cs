namespace Inquiry.Infra.Resilience.Configuration
{
    public class CircuitBreakerOptions
    {
        public int FailureThreshold { get; set; } = 5;
        public TimeSpan SamplingDuration { get; set; } = TimeSpan.FromSeconds(30);
        public TimeSpan BreakDuration { get; set; } = TimeSpan.FromSeconds(30);
        public double SuccessThreshold { get; set; } = 0.5;
    }


}
