using Inquiry.Core.ApplicationService.Common.Security;
using Inquiry.Core.ApplicationService.Contracts.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inquiry.Core.ApplicationService.Features.Products
{
    public record CreateProductCommand(string Name, decimal Price) : IRequest<int>;

    [Authorize(Roles ="Admin,ProductManager",Permissions = "Product.Create")]
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly ICurrentUserService _currentUserService;

        public CreateProductCommandHandler(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            //var product = new Product
            //{
            //    Name = request.Name,
            //    Price = request.Price,
            //    CreatedBy = _currentUserService.UserId,
            //    Created = DateTime.UtcNow
            //};

            //_context.Products.Add(product);
            //await _context.SaveChangesAsync(cancellationToken);

            return 1;
        }
    }
}
