using PayTrackApplication.Domain.Models.CompanyFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayTrackApplication.Domain.Models.NpiPolicyFolder
{
    public class NpiPolicy: BaseModel
    {
        public required Company Company { get; set; }
        public string? Description { get; set; }
        public List<NpiPolicyRule> Rules { get; set; } = new();
        public decimal Amount { get; set; }
    }
}
