namespace Inquiry.Infra.ExternalServices.Resilience.Configuration
{
    public class RetryOptions
    {
        public int MaxRetryAttempts { get; set; } = 3;
        public double BackoffMultiplier { get; set; } = 2.0;
        public TimeSpan InitialDelay { get; set; } = TimeSpan.FromSeconds(1);
        public TimeSpan MaxDelay { get; set; } = TimeSpan.FromSeconds(30);
        public bool UseJitter { get; set; } = true;
    }


}
