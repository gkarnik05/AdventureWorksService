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
    [Route("api/employee")]
    public class EmployeeController
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            this._employeeService = employeeService;
        }


        [HttpGet("all")]
        [ProducesResponseType(typeof(IList<VEmployee>),StatusCodes.Status200OK)]
        public async Task<IList<Contract.VEmployee>> GetAllEmployees()
        {
            return await _employeeService.GetAllEmployee();
        }

        [HttpGet("department")]
        [ProducesResponseType(typeof(IList<VEmployeeDepartment>), StatusCodes.Status200OK)]
        public async Task<IList<Contract.VEmployeeDepartment>> GetEmployeeDepartment()
        {
            return await _employeeService.GetEmployeeDepartment();
        }

        [HttpGet("department/history")]
        [ProducesResponseType(typeof(IList<VEmployeeDepartmentHistory>), StatusCodes.Status200OK)]
        public async Task<IList<Contract.VEmployeeDepartmentHistory>> GetEmployeeDepartmentHistory()
        {
            return await _employeeService.GetEmployeeDepartmentHistory();
        }

        [HttpGet("additionalcontactinfo")]
        [ProducesResponseType(typeof(IList<VAdditionalContactInfo>), StatusCodes.Status200OK)]
        public async Task<IList<VAdditionalContactInfo>> GetEmployeeAdditionalContact()
        {
            return await _employeeService.GetEmployeeAdditionalContact();
        }
        
    }
}
