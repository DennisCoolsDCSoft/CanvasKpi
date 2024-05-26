using System;
using System.Net;

namespace CanvasIdentity.Exceptions
{
    public class IdentityHttpException :Exception
    {
        public IdentityHttpException(HttpStatusCode statusCode, string message) : base(message: message)
        {
            StatusCode = statusCode;
        }
        public HttpStatusCode StatusCode { get; }
    }
}
