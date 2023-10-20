using MediatR;
using PayTrackApplication.Domain.Models.UsersFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayTrackApplication.Application.UserServices.GetAllUsers
{
    public class GetAllUsersQuery:IRequest<ActionResponse>
    {
    }
}
