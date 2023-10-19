using Azure.Identity;
using Microsoft.AspNetCore.Mvc;
using PayTrackApplication.Domain.Models.CompanyFolder;
using PayTrackApplication.Domain.Models.NpiPolicyFolder;

namespace PayTrackApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PayTrackApplicationController: ControllerBase
    {
        public static void CreateNpiPolicy()
        {
            Company companyOne = new Company()
            {
                Name = "A",
            };
            NpiPolicyRule npiPolicyRuleOne = new NpiPolicyRule()
            {
                Description = "Years of Service",
                PolicyRuleType = Domain.Constants.Enums.PolicyRuleType.Default
            };
            NpiPolicyRule npiPolicyRuleTwo = new NpiPolicyRule()
            {
                Description = "Level",
                PolicyRuleType = Domain.Constants.Enums.PolicyRuleType.Default
            };
            List<NpiPolicyRule> npiPolicyRules = new List<NpiPolicyRule>();
            npiPolicyRules.Add(npiPolicyRuleOne);
            npiPolicyRules.Add(npiPolicyRuleTwo);

            NpiPolicy npiPolicyOne = new NpiPolicy()
            {
                Company = companyOne,
                Description = "Car Reward",
                Rules = npiPolicyRules,
                Amount = 10000000

            };
            NpiPolicy npiPolicyTwo = new NpiPolicy()
            {
                Company = companyOne,
                Description = "House Reward",
                Rules = npiPolicyRules,
                Amount = 20000000
            };
             List<NpiPolicy> npiPolicies = new List<NpiPolicy>();
            npiPolicies.Add(npiPolicyOne);
            npiPolicies.Add(npiPolicyTwo);

        }

        
      
       
        
    }
}
