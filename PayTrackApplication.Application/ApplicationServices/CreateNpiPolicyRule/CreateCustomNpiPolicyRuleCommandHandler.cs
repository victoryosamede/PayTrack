using MediatR;
using PayTrackApplication.Application.Services.PayTrackServices;
using PayTrackApplication.Domain.Models.NpiPolicyFolder;
using PayTrackApplication.Domain.Models.UsersFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayTrackApplication.Application.ApplicationServices.CreateNpiPolicyRule
{
    internal class CreateCustomNpiPolicyRuleCommandHandler: IRequestHandler <CreateCustomNpiPolicyRuleCommand, ActionResponse>
    {
        internal readonly INpiPolicyRuleRepository _NpiPolicyRuleRepo;
        
        public CreateCustomNpiPolicyRuleCommandHandler(INpiPolicyRuleRepository npiPolicyRuleRepo)
        {
            _NpiPolicyRuleRepo = npiPolicyRuleRepo;
        }

        public async Task <ActionResponse> Handle ( CreateCustomNpiPolicyRuleCommand request, CancellationToken cancellation)
        {
            var npiPolicyRule = await _NpiPolicyRuleRepo.FindOneByExpression(x => x.Description == request.Description);
            if (npiPolicyRule is not null) return new ActionResponse("Rule Already Exists.");

            NpiPolicyRule newNpiPolicyRule = request;

            var result = await _NpiPolicyRuleRepo.AddEntity(newNpiPolicyRule);
            if (result.ResponseType == ResponseType.Success)
                return result;
            return new ActionResponse
            {
                PayLoad = new
                {
                    newNpiPolicyRule.Id,
                    newNpiPolicyRule.Description,
                    PolicyRuleType = newNpiPolicyRule.PolicyRuleType.ToString()
                }
            };
        }
    }
}
