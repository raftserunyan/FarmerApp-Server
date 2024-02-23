﻿using FarmerApp.Shared.Exceptions;
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

        public async Task Invoke(HttpContext context, ILogger<ExceptionHandlingMiddleware> logger)
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
                    case NotFoundException e:
                        {
                            response.StatusCode = (int)HttpStatusCode.NotFound;
                            break;
                        }
                    case BadHttpRequestException e:
                        {
                            response.StatusCode = (int)HttpStatusCode.BadRequest;
                            break;
                        }
                    case BadRequestException e:
                        {
                            response.StatusCode = (int)HttpStatusCode.BadRequest;
                            break;
                        }
                    case UnauthorizedAccessException e:
                        {
                            response.StatusCode = (int)HttpStatusCode.Unauthorized;
                            break;
                        }
                    case AccessDeniedException e:
                        {
                            response.StatusCode = (int)HttpStatusCode.Forbidden;
                            break;
                        }
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                logger.LogError(error, error.Message);

                await response.WriteAsync(error.Message);
            }
        }
    }
}
