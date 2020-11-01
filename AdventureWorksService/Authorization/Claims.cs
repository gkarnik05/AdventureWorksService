using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AdventureWorksService.WebApi.Authorization
{
    internal static class Claims
    {
        internal const string ScopeClaimType = "http://schemas.microsoft.com/identity/claims/scope";
        internal const string AppPermissionOrRolesClaimType = ClaimTypes.Role;
    }
}
