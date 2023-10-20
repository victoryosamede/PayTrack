using MediatR;
using PayTrackApplication.Application.Services.AuthenticationServices;
using PayTrackApplication.Application.Services.PayTrackServices;
using PayTrackApplication.Application.UserServices.GetAllUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayTrackApplication.Application.UserServices.GetUserById
{
    internal class GetUserByIdQueryHandler: IRequestHandler<GetUserByIdQuery, ActionResponse>
    {
        private readonly IUserRepository _UserRepo;
        private readonly IAuthenticationManager _authenticationManager;
        public GetUserByIdQueryHandler(IUserRepository userRepo, IAuthenticationManager authenticationManager)
        {
            _UserRepo = userRepo;

            _authenticationManager = authenticationManager;
        }
        public async Task<ActionResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return new ActionResponse
            {
                PayLoad = await _UserRepo.GetById(request.Id)
            };
        }
    }
}
