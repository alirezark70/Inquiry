using Inquiry.Core.ApplicationService.Dtos.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inquiry.Core.ApplicationService.Contracts.ExternalServices
{
    public interface IPostInquiryService
    {
        Task<PostDto?> GetPostByIdAsync(int id, CancellationToken cancellationToken = default);
        //Task<IEnumerable<PostDto>> SearchPostsAsync(PostSearchCriteria criteria, CancellationToken cancellationToken = default);
    }
}
