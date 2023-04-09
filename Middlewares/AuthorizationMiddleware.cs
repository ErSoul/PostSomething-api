using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;

namespace PostSomething_api.Middlewares
{
    public class AuthorizationMiddleware : IAuthorizationMiddlewareResultHandler
    {
        private readonly AuthorizationMiddlewareResultHandler defaultHandler = new();
        public async Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
        {
            if (!authorizeResult.Succeeded)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsJsonAsync(
                    new
                    { 
                        message = "",
                        status = "Unauthorized",
                        httpCode = 401
                    });
                return;
            }

            await defaultHandler.HandleAsync(next, context, policy, authorizeResult);
        }
    }
}
