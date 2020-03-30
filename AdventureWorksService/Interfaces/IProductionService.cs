using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdventureWorksService.WebApi.Contract;

namespace AdventureWorksService.WebApi.Interfaces
{
    public interface IProductionService
    {
        Task<IList<Contract.VProductAndDescription>> GetAllProducts();
    }
}
