using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventureWorksService.WebApi.Authorization
{
    internal static class DelegatedPermissions
    {
        public const string EmployeesRead = "Employees.Read";
        public const string ProductsRead = "Products.Read";
        public const string SalesRead = "Sales.Read";

        public static string[] All => typeof(DelegatedPermissions)
            .GetFields()
            .Where(f => f.Name != nameof(All))
            .Select(f => f.GetValue(null) as string)
            .ToArray();
    }
}
