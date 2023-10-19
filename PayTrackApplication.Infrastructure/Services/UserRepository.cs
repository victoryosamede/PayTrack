using Microsoft.EntityFrameworkCore;
using PayTrackApplication.Application.Services.PayTrackServices;
using PayTrackApplication.Domain.Models.UsersFolder;
using PayTrackApplication.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayTrackApplication.Infrastructure.Services
{
    public class UserRepository : PayTrackService<User>, IUserRepository
    {
        public UserRepository(PayTrackApplicationDbContext context) : base(context)
        {
        }
    }
}
