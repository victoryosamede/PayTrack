using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PayTrackApplication.Domain.Constants.Enums;

namespace PayTrackApplication.Domain.Models.NpiPolicyFolder
{
    public class NpiPolicyRule: BaseModel
    {
       public string? Description { get; set; }
       public PolicyRuleType PolicyRuleType { get; set; }
    }
}
