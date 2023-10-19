using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayTrackApplication.Domain.Models.EmployeeFolder
{
    public class EmployeePolicyRenewalValidation: BaseModel
    {
        public int NpiPolicyId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime LastRenewed { get; set; }
    }
}
