using System.Net;

namespace INDWalks.API.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly ILogger<ExceptionHandlerMiddleware> logger;
        private readonly RequestDelegate next;
        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger, RequestDelegate next)
        {
            this.logger = logger;
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            } 
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid();

                //Log the Exception
                logger.LogError(ex, $"{errorId} : {ex.Message}");

                //Return a custom error response
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType= "application/json";

                var error = new
                {
                    Id = errorId,
                    Message = "Something Went Wrong!!!",
                };

                await httpContext.Response.WriteAsJsonAsync(error);
            }
        }
    }
}
