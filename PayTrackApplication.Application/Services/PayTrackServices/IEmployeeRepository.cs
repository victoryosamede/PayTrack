using PayTrackApplication.Domain.Models.EmployeeFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayTrackApplication.Application.Services.PayTrackServices
{
    public interface IEmployeeRepository: IPayTrackService<Employee>
    {
    }
}
