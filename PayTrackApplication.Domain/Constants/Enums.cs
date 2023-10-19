using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayTrackApplication.Domain.Constants
{
    public class Enums
    {
        public enum Roles
        {
            User = 1,
            Admin = 2,
            SuperAdmin = 3,
        }     
        
        public enum PolicyRuleType
        {
            Custom = 200,
            Default = 100,
        }
    }
}
