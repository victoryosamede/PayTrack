using PayTrackApplication.Application.CQRS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayTrackApplication.Application.UserServices.LoginUser
{
    public class LoginUserCommand: Request
    {
        [MaxLength(60), MinLength(3)]
        public required string UserName { get; set; }
        [MaxLength(60), MinLength(8)]
        public required string Password { get; set; }
        [EmailAddress]
        public required string Email { get; set; }

        public override ActionResponse Validate()
        {
            this.Email = Email.ToLower();
            ///Do the complete validation later
            return base.Validate();
        }
    }
}
