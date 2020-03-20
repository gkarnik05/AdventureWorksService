using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdventureWorksService.WebApi.Models;
using AdventureWorksService.WebApi.Interfaces;
using AutoMapper;
using AdventureWorksService.WebApi.Contract.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;

namespace AdventureWorksService.WebApi.Services
{
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

        public async Task<IList<Contract.VEmployee>> GetAllEmployees()
        {            
            using (var dbContext = _awDbContext())
            {
                var employees = await dbContext.Set<Contract.VEmployee>().AsQueryable()                                
                                .ToListAsync();

                return employees;
            }
        }
    }
}
