using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventureWorksService.WebApi.Config
{
    public class AzureAdConfig
    {
        public string Instance { get; set; }
        public string Domain { get; set; }
        public string TenantId { get; set; }
        public string ClientId { get; set; }
        public string Audience { get; set; }
        public string ApplicationName { get; set; }
        public string ClientSecret { get; set; }
    }
}
