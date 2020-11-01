using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventureWorksService.WebApi.Authorization
{
    internal class ActionAuthorizationRequirement : IAuthorizationRequirement
    {
        public ActionAuthorizationRequirement(string action)
        {
            Action = action;
        }

        public string Action { get; }
    }
}
