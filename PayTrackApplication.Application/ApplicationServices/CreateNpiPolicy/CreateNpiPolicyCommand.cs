using PayTrackApplication.Application.CQRS;
using PayTrackApplication.Application.UserServices.CreateUser;
using PayTrackApplication.Domain.Models.CompanyFolder;
using PayTrackApplication.Domain.Models.NpiPolicyFolder;
using PayTrackApplication.Domain.Models.UsersFolder;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayTrackApplication.Application.ApplicationServices.CreateNpiPolicy
{
    public class CreateNpiPolicyCommand:Request
    {
        public required Company  Company {  get; set; }
        [MaxLength(60), MinLength(3)]
        public required string Description { get; set; }

        public List<NpiPolicyRule> Rules { get; set; } = new ();
        public decimal Amount { get; set; }

        public override ActionResponse Validate()
        {
            this.Description = Description.ToLower();
            ///Do the complete validation later
            return base.Validate();
        }

        public static implicit operator NpiPolicy(CreateNpiPolicyCommand command)
        {
            return new NpiPolicy
            {
                Company = command.Company,
                Description = command.Description,
                Rules = command.Rules,
                Amount = command.Amount,
            };
        }
    }
}
