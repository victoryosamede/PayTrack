using Microsoft.EntityFrameworkCore;
using PayTrackApplication.Application.Services.PayTrackServices;
using PayTrackApplication.Domain.Models.EmployeeFolder;
using PayTrackApplication.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayTrackApplication.Infrastructure.Services
{
    public class EmployeeRepository : PayTrackService<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(PayTrackApplicationDbContext context) : base(context)
        {
        }
    }
}
