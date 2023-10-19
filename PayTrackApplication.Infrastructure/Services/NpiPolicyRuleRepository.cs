using Microsoft.EntityFrameworkCore;
using PayTrackApplication.Application.Services.PayTrackServices;
using PayTrackApplication.Domain.Models.NpiPolicyFolder;
using PayTrackApplication.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayTrackApplication.Infrastructure.Services
{
    public class NpiPolicyRuleRepository : PayTrackService<NpiPolicyRule>, INpiPolicyRuleRepository
    {
        public NpiPolicyRuleRepository(PayTrackApplicationDbContext context) : base(context)
        {
        }
    }
}
