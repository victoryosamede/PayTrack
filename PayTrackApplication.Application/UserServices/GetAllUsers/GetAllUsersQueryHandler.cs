using MediatR;
using PayTrackApplication.Application.Services.AuthenticationServices;
using PayTrackApplication.Application.Services.PayTrackServices;
using PayTrackApplication.Domain.Models.UsersFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayTrackApplication.Application.UserServices.GetAllUsers
{
    internal class GetAllUsersQueryHandler: IRequestHandler<GetAllUsersQuery, ActionResponse> 
    {
        private readonly IUserRepository _UserRepo;
        private readonly IAuthenticationManager _authenticationManager;
        public GetAllUsersQueryHandler(IUserRepository userRepo, IAuthenticationManager authenticationManager)
        {
            _UserRepo = userRepo;
            _authenticationManager = authenticationManager;
        }
        public async Task<ActionResponse> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            return new ActionResponse
            {
                PayLoad = await _UserRepo.GetAll()
            };
        }
    }
}
