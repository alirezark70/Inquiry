using AutoMapper;
using Inquiry.Core.ApplicationService.Dtos.Test;
using Inquiry.Core.ApplicationService.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Inquiry.EndPoints.RestApi.Controllers
{
    
    public class WeatherForecastController : BaseApiController
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IMapper _mapper;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecastTest> Get()
        {

            var list= Enumerable.Range(1, 5).Select(index => new WeatherForecastTest
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();

            var one = list.FirstOrDefault();

            //var two = one?.MapTo<WeatherForecastDto>(_mapper);
            var two = _mapper.Map<WeatherForecastDto>(one);


            return list;
        }
    }
}
