using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayTrackApplication.Application
{
    public class ActionResponse
    {
        public ActionResponse()
        {
            ResponseType = ResponseType.Success;
        }
        public ActionResponse(string error, ResponseType responseType = ResponseType.BadRequest)
        {
            ResponseType = responseType;
            AddErrors(error);
        }
        public ActionResponse(Exception exception)
        {
            ResponseType = ResponseType.ServerError;
            AddErrors(exception.Message);
            Exception = exception;
        }
        public object? PayLoad { get; set; }
        private List<string> Errors { get; set; } = new();
        public Exception? Exception { get; set; }
        public ResponseType ResponseType { get; private set; }
        public string ReasonPhrase => FormErrors();
        public void AddErrors(string error)
        {
            Errors.Add(error);
        }

        private string FormErrors()
        {
            StringBuilder err = new();
            foreach (var error in Errors)
            {
                err.AppendLine(error);
            }
            return err.ToString();
        }

    }
    public enum ResponseType
    {
        Success = 0,
        BadRequest = 1,
        NotFound = 2,
        UnAuthorize = 3,
        ServerError = 4
    }

}
