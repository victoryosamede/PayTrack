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
    public class EmployeePolicyRenewalValidationRepository : PayTrackService<EmployeePolicyRenewalValidation>, IEmployeePolicyRenewalValidationRepository
    {
        public EmployeePolicyRenewalValidationRepository(PayTrackApplicationDbContext context) : base(context)
        {
        }
    }
}
