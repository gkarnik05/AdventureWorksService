using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdventureWorksService.WebApi.Interfaces;
using AdventureWorksService.WebApi.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace AdventureWorksService.WebApi.Controllers
{
    [Route("/api/sales")]
    public class SalesController : Controller
    {
        private readonly ISalesService _salesService;

        public SalesController(ISalesService salesService)
        {
            this._salesService = salesService;
        }

        [HttpGet("customers")]
        [ProducesResponseType(typeof(IList<Contract.VIndividualCustomer>), StatusCodes.Status200OK)]
        public async Task<IList<VIndividualCustomer>> GetIndividualCustomers()
        {
            return await _salesService.GetIndividualCustomer();
        }

        [HttpGet("salespersons")]
        [ProducesResponseType(typeof(IList<Contract.VSalesPerson>), StatusCodes.Status200OK)]
        public async Task<IList<VSalesPerson>> GetSalesPersons()
        {
            return await _salesService.GetSalesPerson();
        }

        [HttpGet("sales")]
        [ProducesResponseType(typeof(IList<Contract.VSalesPersonSalesByFiscalYears>), StatusCodes.Status200OK)]
        public async Task<IList<VSalesPersonSalesByFiscalYears>> GetSalesPersonSales()
        {
            return await _salesService.GetSalesPersonSalesByFiscalYears();
        }

        [HttpGet("storeaddresses")]
        [ProducesResponseType(typeof(IList<Contract.VStoreWithAddresses>), StatusCodes.Status200OK)]
        public async Task<IList<VStoreWithAddresses>> GetStoreWithAddresses()
        {
            return await _salesService.GetStoreWithAddresses();
        }

        [HttpGet("storecontacts")]
        [ProducesResponseType(typeof(IList<Contract.VStoreWithContacts>), StatusCodes.Status200OK)]
        public async Task<IList<VStoreWithContacts>> GetStoreWithContacts()
        {
            return await _salesService.GetStoreWithContacts();
        }

        [HttpGet("storedemographics")]
        [ProducesResponseType(typeof(IList<Contract.VStoreWithDemographics>), StatusCodes.Status200OK)]
        public async Task<IList<VStoreWithDemographics>> GetStoreWithDemographics()
        {
            return await _salesService.GetStoreWithDemographics();
        }
    }
}
