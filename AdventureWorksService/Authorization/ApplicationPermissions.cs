using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventureWorksService.WebApi.Config
{
    internal static class ApplicationPermissions
    {
        public const string EmployeesRead = "Employees.Read";
        public const string ProductsRead = "Products.Read";
        public const string SalesRead = "Sales.Read";

        public static string[] All => typeof(ApplicationPermissions)
            .GetFields()
            .Where(f => f.Name != nameof(All))
            .Select(f => f.GetValue(null) as string)
            .ToArray();
    }
}
