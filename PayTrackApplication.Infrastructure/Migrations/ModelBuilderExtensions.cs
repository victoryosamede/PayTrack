using Microsoft.EntityFrameworkCore;
using PayTrackApplication.Domain.Constants;
using PayTrackApplication.Domain.Models.NpiPolicyFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayTrackApplication.Infrastructure.Migrations
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NpiPolicyRule>().HasData(
                new NpiPolicyRule
                {
                    Id = 1,
                    Description = "Years of Service",
                    PolicyRuleType = Enums.PolicyRuleType.Default
                },
                new NpiPolicyRule
                {
                    Id = 2,
                    Description = "Level",
                    PolicyRuleType = Enums.PolicyRuleType.Default
                },
                new NpiPolicyRule
                {
                    Id = 3,
                    Description = "Renewal Time",
                    PolicyRuleType = Enums.PolicyRuleType.Default
                }
                );
        }
    }
}
