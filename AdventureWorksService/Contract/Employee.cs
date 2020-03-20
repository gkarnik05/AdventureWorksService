using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace AdventureWorksService.WebApi.Contract
{    
    public class Employee
    {
        public int BusinessEntityId { get; set; }
        public string NationalIdNumber { get; set; }
        public string LoginId { get; set; }
        public string JobTitle { get; set; }
        public DateTime BirthDate { get; set; }
        public char MaritalStatus { get; set; }
        public char Gender { get; set; }
    }
}
