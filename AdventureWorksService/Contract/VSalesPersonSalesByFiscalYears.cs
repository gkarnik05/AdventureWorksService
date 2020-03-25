using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdventureWorksService.WebApi.Contract
{
    public partial class VSalesPersonSalesByFiscalYears
    {
        public int? SalesPersonId { get; set; }
        public string FullName { get; set; }
        public string JobTitle { get; set; }
        public string SalesTerritory { get; set; } 
        [Column("2002")]        
        public decimal? _2002 { get; set; }
        [Column("2003")]
        public decimal? _2003 { get; set; }
        [Column("2004")]
        public decimal? _2004 { get; set; }
    }
}
