using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdventureWorksService.WebApi.Models;
using AdventureWorksService.WebApi.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using AdventureWorksService.WebApi.Contract;

namespace AdventureWorksService.WebApi.Services
{
    public class LookupService : ILookupService
    {
        private readonly IMapper _mapper;
        private readonly Func<AdventureWorks2017DbContext> _awDbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LookupService(IMapper mapper, Func<AdventureWorks2017DbContext> awDbContext, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _awDbContext = awDbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IList<Contract.VStateProvinceCountryRegion>> GetStates()
        {
            using (var dbContext = _awDbContext())
            {
                var states = await dbContext.Set<Contract.VStateProvinceCountryRegion>().AsQueryable()
                                    .ToListAsync();

                return states;
            }
        }
    }
}
