using System.Net;

namespace FarmerApp.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "text";


                switch (error)
                {
                    case BadHttpRequestException e:
                        {
                            response.StatusCode = (int)HttpStatusCode.BadRequest;
                            break;
                        }                    
                    case UnauthorizedAccessException e:
                        {
                            response.StatusCode = (int)HttpStatusCode.Unauthorized;
                            break;
                        }
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                await response.WriteAsync(error.Message);
            }
        }
    }
}
