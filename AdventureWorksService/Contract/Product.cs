using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventureWorksService.WebApi.Contract
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public bool IsMake { get; set; }
        public bool IsFinishedGoods { get; set; }
        public string Color { get; set; }
        public int StockLevel { get; set; }
        public int ReorderPoint { get; set; }
        public decimal StandardCost { get; set; }
        public decimal ListPrice { get; set; }
        public string Size { get; set; }
    }
}
