using Microsoft.EntityFrameworkCore;
using PayTrackApplication.Application.Services.PayTrackServices;
using PayTrackApplication.Domain.Models.CompanyFolder;
using PayTrackApplication.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayTrackApplication.Infrastructure.Services
{
    public class CompanyRepository : PayTrackService<Company>, ICompanyRepository
    {
        public CompanyRepository(PayTrackApplicationDbContext context) : base(context)
        {
        }
    }
}
