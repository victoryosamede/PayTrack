using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PayTrackApplication.Domain.Constants.Enums;

namespace PayTrackApplication.Application.DTO
{
    public class UserDto
    {
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required Roles Role { get; set; }
        public bool IsDeleted { get; set; }
    }
}
