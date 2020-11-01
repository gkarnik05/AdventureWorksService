using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdventureWorksService.WebApi.Models;
using AdventureWorksService.WebApi.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using AdventureWorksService.WebApi.Authorization;

namespace AdventureWorksService.WebApi.Services
{
    [Authorize(Actions.EmployeesRead)]
    public class EmployeeService : IEmployeeService
    {
        private readonly IMapper _mapper;        
        private readonly Func<AdventureWorks2017DbContext> _awDbContext;        
        private readonly IHttpContextAccessor _httpContextAccessor;
        public EmployeeService(IMapper mapper, Func<AdventureWorks2017DbContext> awDbContext, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;           
            _awDbContext = awDbContext;            
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IList<Contract.VEmployee>> GetAllEmployee()
        {            
            using (var dbContext = _awDbContext())
            {
                var employees = await dbContext.Set<Contract.VEmployee>().AsQueryable()                                
                                .ToListAsync();

                return employees;
            }
        }

        public async Task<IList<Contract.VEmployeeDepartment>> GetEmployeeDepartment()
        {
            using (var dbContext = _awDbContext())
            {
                var departments = await dbContext.Set<Contract.VEmployeeDepartment>().AsQueryable()
                                .ToListAsync();

                return departments;
            }
        }

        public async Task<IList<Contract.VEmployeeDepartmentHistory>> GetEmployeeDepartmentHistory()
        {
            using (var dbContext = _awDbContext())
            {
                var departmentHistories = await dbContext.Set<Contract.VEmployeeDepartmentHistory>().AsQueryable()
                                .ToListAsync();

                return departmentHistories;
            }
        }

        public async Task<IList<Contract.VIndividualCustomer>> GetIndividualCustomer()
        {
            using (var dbContext = _awDbContext())
            {
                var individualCustomers = await dbContext.Set<Contract.VIndividualCustomer>().AsQueryable()
                                .ToListAsync();

                return individualCustomers;
            }
        }

        public async Task<IList<Contract.VAdditionalContactInfo>> GetEmployeeAdditionalContact()
        {
            using (var dbContext = _awDbContext())
            {
                var additionalContactInfo = await dbContext.Set<Contract.VAdditionalContactInfo>().AsQueryable()
                                .ToListAsync();

                return additionalContactInfo;
            }
        }
        
    }
}
