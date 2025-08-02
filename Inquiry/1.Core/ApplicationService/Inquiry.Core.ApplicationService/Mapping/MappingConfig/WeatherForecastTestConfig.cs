using Inquiry.Core.ApplicationService.Dtos.Test;
using Inquiry.Core.ApplicationService.Mapping.Contracts;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inquiry.Core.ApplicationService.Mapping.MappingConfig
{
    public class WeatherForecastTestConfig : IMappingConfig
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<WeatherForecastTest, WeatherForecastDto>()
            .IgnoreNullValues(true);
        }
    }
}
