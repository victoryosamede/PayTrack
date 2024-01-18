using PayTrackApplication.Application.CQRS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayTrackApplication.Application.UserServices.ChangePassword
{
    public class ChangePasswordCommand: Request
    {
        [Required]
        [DataType(DataType.Password)]
        public required string CurrentPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }
    }
}
