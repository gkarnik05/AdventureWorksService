using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdventureWorksService.WebApi.Contract;

namespace AdventureWorksService.WebApi.Interfaces
{
    public interface ISalesService
    {
        Task<IList<VIndividualCustomer>> GetIndividualCustomer();
        
        Task<IList<VSalesPerson>> GetSalesPerson();

        Task<IList<VSalesPersonSalesByFiscalYears>> GetSalesPersonSalesByFiscalYears();

        Task<IList<VStoreWithAddresses>> GetStoreWithAddresses();

        Task<IList<VStoreWithContacts>> GetStoreWithContacts();

        Task<IList<VStoreWithDemographics>> GetStoreWithDemographics();

    }
}
