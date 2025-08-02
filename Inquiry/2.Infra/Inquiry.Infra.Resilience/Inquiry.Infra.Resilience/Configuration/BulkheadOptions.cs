namespace Inquiry.Infra.Resilience.Configuration
{
    public class BulkheadOptions
    {
        public int MaxParallelization { get; set; } = 10;
        public int MaxQueuingActions { get; set; } = 20;
    }
}
