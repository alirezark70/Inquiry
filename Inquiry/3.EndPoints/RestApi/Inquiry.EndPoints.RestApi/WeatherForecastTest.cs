using Inquiry.Core.ApplicationService.Dtos.Test;
using Inquiry.Core.ApplicationService.Mapping.Contracts;

namespace Inquiry.EndPoints.RestApi
{
    public class WeatherForecastTest 
    {
        public DateOnly Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }

        
    }
}
