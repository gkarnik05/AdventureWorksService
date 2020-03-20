using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using AdventureWorksService.WebApi.Models;

namespace AdventureWorksService.WebApi.Mappers
{
    public class AutoMapperProfile : AutoMapper.Profile
    {
        public AutoMapperProfile()
        {
            //CreateMap<Employee, Contract.Employee>();
            //CreateMap<Product, Contract.Product>()
            //    .ForMember(dest => dest.Id, opt => opt.MapFrom(s => s.ProductID))
            //    .ForMember(dest => dest.Number, opt => opt.MapFrom(s => s.ProductNumber))
            //    .ForMember(dest => dest.IsMake, opt => opt.MapFrom(s => s.MakeFlag))
            //    .ForMember(dest => dest.IsFinishedGoods, opt => opt.MapFrom(s => s.FinishedGoodsFlag))
            //    .ForMember(dest => dest.StockLevel, opt => opt.MapFrom(s => s.SafetyStockLevel));                
        }
    }
}
