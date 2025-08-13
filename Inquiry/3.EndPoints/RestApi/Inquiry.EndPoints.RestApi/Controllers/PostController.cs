using Inquiry.Core.ApplicationService.Contracts.ExternalServices;
using Inquiry.Core.ApplicationService.Dtos.Posts;
using Inquiry.Core.ApplicationService.Dtos.Test;
using Inquiry.Core.ApplicationService.Mapping.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace Inquiry.EndPoints.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController :  BaseApiController
    {
        private readonly ILogger<PostController> _logger;
        private readonly IMappingService _mappingService;
        private readonly IPostInquiryService _postInquiryService;

        public PostController(ILogger<PostController> logger, IMappingService mappingService, IPostInquiryService postInquiryService)
        {
            _logger = logger;
            _mappingService = mappingService;
            _postInquiryService = postInquiryService;
        }


        [HttpGet("GetPost/{id}")]
        public async Task<IActionResult> Get([FromRoute]int id)
        {
            var result = await _postInquiryService.GetPostByIdAsync(id);
            return OkResponse<PostDto>(result);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var posts=await _postInquiryService.GetAllAsync();

            return PagedResponse<PostDto>(posts,1,1,posts.Count());    
        }
    }
}
