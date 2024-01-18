using MediatR;
using PayTrackApplication.Application.CQRS;
using PayTrackApplication.Application.Services.AuthenticationServices;
using PayTrackApplication.Application.Services.PayTrackServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayTrackApplication.Application.UserServices.DeleteUser
{
    internal class DeleteUserCommandHandler: IRequestHandler<DeleteUserCommand, ActionResponse>
    {
        internal readonly IUserRepository _UserRepo;
        public DeleteUserCommandHandler(IUserRepository userRepo)
        {
            _UserRepo = userRepo;
        }

        public async Task<ActionResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _UserRepo.GetById(request.Id);
            if (user == null) return new ActionResponse("User is not found.", ResponseType.NotFound);

            return await _UserRepo.DeleteEntity(user);
        }
    }

}
