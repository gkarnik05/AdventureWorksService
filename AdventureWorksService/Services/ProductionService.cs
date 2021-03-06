﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdventureWorksService.WebApi.Authorization;
using AdventureWorksService.WebApi.Contract;
using AdventureWorksService.WebApi.Interfaces;
using AdventureWorksService.WebApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorksService.WebApi.Services
{
    [Authorize(Actions.ProductsRead)]
    public class ProductionService : IProductionService
    {
        private readonly IMapper _mapper;
        private readonly Func<AdventureWorks2017DbContext> _awDbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductionService(IMapper mapper, Func<AdventureWorks2017DbContext> awDbContext, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _awDbContext = awDbContext;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IList<Contract.VProductAndDescription>> GetAllProducts()
        {
            using (var dbContext = _awDbContext())
            {
                var products = await dbContext.Set<Contract.VProductAndDescription>().AsQueryable()                                
                                .ToListAsync();

                return products;
            }
        }
    }
}
