using PayTrackApplication.Domain.Models.CompanyFolder;
using PayTrackApplication.Domain.Models.NpiPolicyFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayTrackApplication.Domain.Models.EmployeeFolder
{
    public class Employee: BaseModel
    {
        public DateTime StartDate { get; set; }
        public string EmailAddress { get; set; } = string.Empty;
        public required string Position { get; set; }
        public List<NpiPolicy> Policies { get; set; } = new();
        public Company? Company { get; set; }
    }
}
