using PayTrackApplication.Domain.Models.NpiPolicyFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayTrackApplication.Application.Services.PayTrackServices
{
    public interface INpiPolicyRepository: IPayTrackService<NpiPolicy>
    {
    }
}
