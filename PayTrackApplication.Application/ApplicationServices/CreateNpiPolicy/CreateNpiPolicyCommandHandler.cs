using MediatR;
using PayTrackApplication.Application.Services.PayTrackServices;
using PayTrackApplication.Domain.Models.NpiPolicyFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayTrackApplication.Application.ApplicationServices.CreateNpiPolicy
{
    internal class CreateNpiPolicyCommandHandler: IRequestHandler<CreateNpiPolicyCommand, ActionResponse>
    {
        internal readonly INpiPolicyRepository _NpiPolicyRepo;

        public CreateNpiPolicyCommandHandler(INpiPolicyRepository npiPolicyRepo)
        {
            _NpiPolicyRepo = npiPolicyRepo;
        }

        public async Task <ActionResponse> Handle (CreateNpiPolicyCommand request, CancellationToken cancellationToken)
        {
            var npiPolicy = await _NpiPolicyRepo.FindOneByExpression(x => x.Description == request.Description);
            if (npiPolicy is not null) return new ActionResponse("Policy Already Exists");

            NpiPolicy newNpiPolicy = request;
            var result = await _NpiPolicyRepo.AddEntity(newNpiPolicy);
            if (result.ResponseType == ResponseType.Success)
                return result;
            return new ActionResponse
            {
                PayLoad = new
                {
                    newNpiPolicy.Id,
                    newNpiPolicy.Description,
                    newNpiPolicy.Rules,
                    newNpiPolicy.Amount
                }
            };

        }
    }
    
}
