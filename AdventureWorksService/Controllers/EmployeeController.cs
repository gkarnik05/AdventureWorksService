using AdventureWorksService.WebApi.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventureWorksService.WebApi.Controllers
{
    [Route("api/employee")]
    public class EmployeeController
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            this._employeeService = employeeService;
        }


        [HttpGet("all")]
        [ProducesResponseType(typeof(IList<Contract.VEmployee>),StatusCodes.Status200OK)]
        public async Task<IList<Contract.VEmployee>> GetAllEmployees()
        {
            return await _employeeService.GetAllEmployees();
        }
    }
}
