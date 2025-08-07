using Inquiry.Core.ApplicationService.Contracts.ExternalServices;
using Inquiry.Core.ApplicationService.Dtos.Posts;
using Inquiry.Core.ApplicationService.Mapping.Contracts;
using MapsterMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Inquiry.Infra.ExternalServices.Posts
{
    public class PostInquiryService : IPostInquiryService
    {
        private readonly HttpClient _httpClient;
        private readonly IMappingService _mapper;
        private readonly ILogger<PostInquiryService> _logger;


        public PostInquiryService(HttpClient httpClient, IMappingService mapper, ILogger<PostInquiryService> logger)
        {
            _httpClient = httpClient;
            _mapper = mapper;
            _logger = logger;
        }




        public async Task<PostDto?> GetPostByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.GetAsync(
               $"posts/{id}",
               cancellationToken);

            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;


           // await Task.Delay(5000);
            response.EnsureSuccessStatusCode();

            var person = await response.Content.ReadFromJsonAsync<PostDto>(cancellationToken);
            return person;
        }

        //public async Task<IEnumerable<PostDto>> SearchPostsAsync(PostSearchCriteria criteria, CancellationToken cancellationToken = default)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
