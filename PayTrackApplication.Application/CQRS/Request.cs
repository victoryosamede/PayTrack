using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayTrackApplication.Application.CQRS
{
    public class Request:IRequest<ActionResponse>
    {
        public virtual ActionResponse Validate()
        {
            return new ActionResponse();
        }
        public virtual int Id { get; set; }
    }
}
