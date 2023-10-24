using MediatR;
using Microsoft.AspNetCore.Mvc;
using PayTrackApplication.Application;
using PayTrackApplication.Application.DTO;
using PayTrackApplication.Application.Services.AuthenticationServices;
using PayTrackApplication.Application.Services.PayTrackServices;
using PayTrackApplication.Application.UserServices.ChangePassword;
using PayTrackApplication.Application.UserServices.CreateUser;
using PayTrackApplication.Application.UserServices.DeleteUser;
using PayTrackApplication.Application.UserServices.ForgotPassword;
using PayTrackApplication.Application.UserServices.GetAllUsers;
using PayTrackApplication.Application.UserServices.GetUserById;
using PayTrackApplication.Application.UserServices.LoginUser;
using PayTrackApplication.Application.UserServices.ResetPassword;
using PayTrackApplication.Application.UserServices.VerifyResetToken;
using PayTrackApplication.Domain.Models.UsersFolder;
using System.Data;

namespace PayTrackApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController:CustomControllerBase
    {
        public AuthController(ISender sender) : base(sender)
        {
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser (CreateUserCommand command)
        => await SendAsync(command);

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser (LoginUserCommand command) => await SendAsync(command);
        
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteUser(DeleteUserCommand command) => await SendAsync(command);

        [HttpPut ("change password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordCommand command) => await SendAsync(command);

        [HttpPut ("forgot password")]
        public async Task<IActionResult> ForgotPassword (ForgotPasswordCommand command) => await SendAsync(command);

        [HttpPut]
        public async Task<IActionResult> VerifyResetPasswordToken (VerifyResetTokenCommand command) => await SendAsync(command);

        [HttpPut]
        public async Task<IActionResult> ResetPassword (ResetPasswordCommand command) => await SendAsync(command);

        [HttpGet]
        public async Task<IActionResult> GetAUser (GetUserByIdQuery query) => await SendAsync(query);

        [HttpGet]
        public async Task<IActionResult> GetAllUsers (GetAllUsersQuery query) => await SendAsync(query);
    }
}
