using Inquiry.Core.ApplicationService.Contracts.Infrastructure;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Inquiry.Infra.ExternalServices.Users
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly ClaimsPrincipal? _user;


        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _user = _httpContextAccessor.HttpContext?.User;
        }

        public string? UserId => _user?.FindFirstValue(claimType: ClaimTypes.NameIdentifier);

        public string? UserName => _user?.FindFirstValue(claimType: ClaimTypes.NameIdentifier);

        public bool IsAuthenticated => _user?.Identity?.IsAuthenticated ?? false;

        public IReadOnlyList<string> Roles => _user?.FindAll(ClaimTypes.Role).Select(c=> c.Value).ToList().AsReadOnly()?? new List<string>().AsReadOnly();

        public IReadOnlyList<string> Permissions => _user?.FindAll("permission").Select(c=>c.Value).ToList().AsReadOnly()??
            new List<string>().AsReadOnly();

        public bool HasPermission(string permissionName)
        {
            return Permissions.Contains(permissionName);
        }

        public bool IsInRole(string roleName)
        {
            return _user?.IsInRole(roleName)??false;
        }
    }
}
