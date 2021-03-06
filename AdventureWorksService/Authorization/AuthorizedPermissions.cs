﻿using AdventureWorksService.WebApi.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventureWorksService.WebApi.Authorization
{
    internal static class AuthorizedPermissions
    {
        /// <summary>
        /// Contains the allowed delegated permissions for each action.
        /// If the caller has one of the allowed ones, they should be allowed
        /// to perform the action.
        /// </summary>
        public static IReadOnlyDictionary<string, string[]> DelegatedPermissionsForActions = new Dictionary<string, string[]>
        {
            [Actions.EmployeesRead] = new[] { DelegatedPermissions.EmployeesRead },
            [Actions.ProductsRead] = new[] { DelegatedPermissions.ProductsRead },
            [Actions.SalesRead] = new [] {DelegatedPermissions.SalesRead}
        };

        /// <summary>
        /// Contains the allowed application permissions for each action.
        /// If the caller has one of the allowed ones, they should be allowed
        /// to perform the action.
        /// </summary>
        public static IReadOnlyDictionary<string, string[]> ApplicationPermissionsForActions = new Dictionary<string, string[]>
        {
            [Actions.EmployeesRead] = new[] { ApplicationPermissions.EmployeesRead },
            [Actions.ProductsRead] = new[] { ApplicationPermissions.ProductsRead },
            [Actions.SalesRead] = new[] {ApplicationPermissions.SalesRead}
        };
    }
}
