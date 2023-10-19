using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayTrackApplication.Domain.Models.CompanyFolder
{
    public class Company: BaseModel
    {
        public required string Name { get; set; }
    }
}
