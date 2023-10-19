using PayTrackApplication.Application.CQRS;
using PayTrackApplication.Domain.Models.UsersFolder;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PayTrackApplication.Domain.Constants.Enums;

namespace PayTrackApplication.Application.UserServices.CreateUser
{
    public class CreateUserCommand: CommandBase
    {
        [MaxLength(60), MinLength(3)]
        public required string UserName { get; set; }
        [MaxLength(60), MinLength(8)]
        public required string Password { get; set; }
        [EmailAddress]
        public required string Email { get; set; }
        public required Roles Role { get; set; }

        public override ActionResponse Validate()
        {
            this.Email = Email.ToLower();
            ///Do the complete validation later
            return base.Validate();
        }

        public static implicit operator User(CreateUserCommand command)
        {
            return new User
            {
                Email = command.Email,
                Roles = command.Role,
                UserName = command.UserName,
            };
        }
    }
}
