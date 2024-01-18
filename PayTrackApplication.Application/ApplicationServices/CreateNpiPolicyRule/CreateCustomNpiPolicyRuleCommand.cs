using PayTrackApplication.Application.CQRS;
using PayTrackApplication.Application.UserServices.CreateUser;
using PayTrackApplication.Domain.Models.NpiPolicyFolder;
using PayTrackApplication.Domain.Models.UsersFolder;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PayTrackApplication.Domain.Constants.Enums;

namespace PayTrackApplication.Application.ApplicationServices.CreateNpiPolicyRule
{
    public class CreateCustomNpiPolicyRuleCommand:Request
    {
        [MaxLength(60), MinLength(3)]
        public required string Description { get; set; }

        public PolicyRuleType PolicyRuleType { get;  } = PolicyRuleType.Custom;

        public override ActionResponse Validate()
        {
            this.Description = Description.ToLower();
            ///Do the complete validation later
            return base.Validate();
        }

        public static implicit operator NpiPolicyRule(CreateCustomNpiPolicyRuleCommand command)
        {
            return new NpiPolicyRule
            {
                Description = command.Description,
                PolicyRuleType = command.PolicyRuleType,
       
            };
        }
    }
}
