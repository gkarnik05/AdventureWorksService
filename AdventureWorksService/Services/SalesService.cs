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
using Microsoft.AspNetCore.Authorization;
using AdventureWorksService.WebApi.Authorization;

namespace AdventureWorksService.WebApi.Services
{
    [Authorize(Actions.SalesRead)]
    public class SalesService : ISalesService
    {
        private readonly IMapper _mapper;
        private readonly Func<AdventureWorks2017DbContext> _awDbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public SalesService(IMapper mapper, Func<AdventureWorks2017DbContext> awDbContext, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _awDbContext = awDbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IList<VIndividualCustomer>> GetIndividualCustomer()
        {
            using (var dbContext = _awDbContext())
            {
                var customers = await dbContext.Set<Contract.VIndividualCustomer>().AsQueryable()
                                .ToListAsync();

                return customers;
            }
        }

        public async Task<IList<VSalesPerson>> GetSalesPerson()
        {
            using (var dbContext = _awDbContext())
            {
                var salesPersons = await dbContext.Set<Contract.VSalesPerson>().AsQueryable()
                                .ToListAsync();

                return salesPersons;
            }
        }

        public async Task<IList<VSalesPersonSalesByFiscalYears>> GetSalesPersonSalesByFiscalYears()
        {
            using (var dbContext = _awDbContext())
            {
                var salesPersonSales = await dbContext.Set<Contract.VSalesPersonSalesByFiscalYears>().AsQueryable()
                                .ToListAsync();

                return salesPersonSales;
            }
        }

        public async Task<IList<VStoreWithAddresses>> GetStoreWithAddresses()
        {
            using (var dbContext = _awDbContext())
            {
                var storeAddresses = await dbContext.Set<Contract.VStoreWithAddresses>().AsQueryable()
                                .ToListAsync();

                return storeAddresses;
            }
        }

        public async Task<IList<VStoreWithContacts>> GetStoreWithContacts()
        {
            using (var dbContext = _awDbContext())
            {
                var storeContacts = await dbContext.Set<Contract.VStoreWithContacts>().AsQueryable()
                                .ToListAsync();

                return storeContacts;
            }
        }

        public async Task<IList<VStoreWithDemographics>> GetStoreWithDemographics()
        {
            using (var dbContext = _awDbContext())
            {
                var storeDemographics = await dbContext.Set<Contract.VStoreWithDemographics>().AsQueryable()
                                .ToListAsync();

                return storeDemographics;
            }
        }
    }
}
