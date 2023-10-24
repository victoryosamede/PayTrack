using MediatR;
using Microsoft.AspNetCore.Mvc;
using PayTrackApplication.Application;
using PayTrackApplication.Application.CQRS;

namespace PayTrackApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomControllerBase : ControllerBase
    {
        private readonly ISender _sender;

        public CustomControllerBase(ISender sender)
        {
            _sender = sender;
        }

        public async Task<IActionResult> SendAsync<TRequest>(TRequest request) where TRequest : Request
        {
            var result = request.Validate();
            if (result.ResponseType != ResponseType.Success)
                return BadRequest(result.ReasonPhrase);

            result = await _sender.Send(request);
            
            if (result.ResponseType == ResponseType.Success)
                return Ok(result.PayLoad);
            if (result.ResponseType == ResponseType.NotFound)
                return NotFound(result.ReasonPhrase);
            if (result.ResponseType == ResponseType.ServerError)
            {
                //Log exception
                return Problem(result.ReasonPhrase);
            }
            if (result.ResponseType == ResponseType.UnAuthorize)
                return Unauthorized(result.ReasonPhrase);
            return BadRequest(result.ReasonPhrase);
        }

       
    }
}
