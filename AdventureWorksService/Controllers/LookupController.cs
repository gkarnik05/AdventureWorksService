using AdventureWorksService.WebApi.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdventureWorksService.WebApi.Contract;

namespace AdventureWorksService.WebApi.Controllers
{
    [Route("api/lookup")]
    public class LookupController : Controller
    {
        private readonly ILookupService _lookupService;

        public LookupController(ILookupService lookupService)
        {
            this._lookupService = lookupService;
        }

        [HttpGet("states")]
        [ProducesResponseType(typeof(IList<VStateProvinceCountryRegion>), StatusCodes.Status200OK)]
        public async Task<IList<VStateProvinceCountryRegion>> GetStates()
        {
            return await _lookupService.GetStates();
        }

    }
}
