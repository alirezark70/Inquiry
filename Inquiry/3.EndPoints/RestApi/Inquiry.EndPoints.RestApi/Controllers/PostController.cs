using Inquiry.Core.ApplicationService.Contracts.ExternalServices;
using Inquiry.Core.ApplicationService.Dtos.Posts;
using Inquiry.Core.ApplicationService.Dtos.Test;
using Inquiry.Core.ApplicationService.Mapping.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inquiry.EndPoints.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController :  BaseApiController
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IMappingService _mappingService;
        private readonly IPostInquiryService _postInquiryService;

        public PostController(ILogger<WeatherForecastController> logger, IMappingService mappingService, IPostInquiryService postInquiryService)
        {
            _logger = logger;
            _mappingService = mappingService;
            _postInquiryService = postInquiryService;
        }


        [HttpGet("GetPost/{id}")]
        public async Task<PostDto?> Get([FromRoute]int id)
        {
            var result = await _postInquiryService.GetPostByIdAsync(id);
            return result;
        }

    }
}
