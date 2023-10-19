using Microsoft.EntityFrameworkCore;
using PayTrackApplication.Domain.Models.CompanyFolder;
using PayTrackApplication.Domain.Models.EmployeeFolder;
using PayTrackApplication.Domain.Models.NpiPolicyFolder;
using PayTrackApplication.Domain.Models.UsersFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayTrackApplication.Infrastructure.Database
{
    public class PayTrackApplicationDbContext: DbContext
    {
        //string connectionString = @"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=PayTackAppDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";
        public PayTrackApplicationDbContext(DbContextOptions<PayTrackApplicationDbContext> options): base (options)
        {
                
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //    optionsBuilder.UseSqlServer(connectionString);
        //}
       
        public DbSet<Company> Company { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<NpiPolicy> NpiPolicies { get; set; }
        public DbSet<NpiPolicyRule> NpiPolicyRule { get; set; }
        public DbSet<EmployeePolicyRenewalValidation> EmployeePolicyRenewalValidation { get; set; }
        public DbSet<User> Users{ get; set; }
    }
}
