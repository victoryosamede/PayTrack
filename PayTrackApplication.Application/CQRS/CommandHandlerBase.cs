using MediatR;
using PayTrackApplication.Application.Services.AuthenticationServices;
using PayTrackApplication.Application.Services.PayTrackServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayTrackApplication.Application.CQRS
{
    internal class CommandHandlerBase<Cmd> : IRequestHandler<Cmd, ActionResponse> where Cmd : CommandBase
    {
        internal readonly IUserRepository _UserRepo;
        internal readonly IAuthenticationManager _authManager;
        public CommandHandlerBase(IUserRepository userRepo, IAuthenticationManager authManager)
        {
            _UserRepo = userRepo;
            _authManager = authManager;
        }

        public virtual async Task<ActionResponse> Handle(Cmd request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(new ActionResponse(new Exception("Not implemented")));
        }
    }
}
