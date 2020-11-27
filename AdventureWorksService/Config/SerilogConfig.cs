using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventureWorksService.WebApi.Config
{
    public class SerilogConfig
    {
        public string Environment { get; set; }
        public string ServiceUrl { get; set; }
        public string WorkspaceId { get; set; }
        public string PrimaryKey { get; set; }
    }
}
