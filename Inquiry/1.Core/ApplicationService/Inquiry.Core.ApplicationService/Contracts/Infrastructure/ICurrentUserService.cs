using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inquiry.Core.ApplicationService.Contracts.Infrastructure
{
    public interface ICurrentUserService
    {
        string? UserId { get; }

        string? UserName { get; }

        bool IsAuthenticated { get; }

        IReadOnlyList<string> Roles { get; }
        IReadOnlyList<string> Permissions { get; }

        bool IsInRole(string roleName);

        bool HasPermission(string permissionName);
    }
}
