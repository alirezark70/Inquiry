namespace Inquiry.Infra.Resilience.Configuration
{
    public class TimeoutOptions
    {
        public TimeSpan RequestTimeout { get; set; } = TimeSpan.FromSeconds(10);
    }

}
