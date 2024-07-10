using ImagineBookStore.Model.Input;
using Microsoft.AspNetCore.Http;

namespace ImagineBookStore.Core.Middlewares;

/// <summary>
/// Middleware for extracting user session information from the current HTTP context.
/// </summary>
public class UserSessionMiddleware
{
    private readonly RequestDelegate _next;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserSessionMiddleware"/> class.
    /// </summary>
    /// <param name="next">The next middleware in the pipeline.</param>
    public UserSessionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    /// Invokes the middleware to extract user session information from the current HTTP context.
    /// </summary>
    /// <param name="context">The HTTP context for the current request.</param>
    /// <param name="session">The user session instance to store extracted information.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task InvokeAsync(HttpContext context, UserSession session)
    {
        if (context.User.Identities.Any(x => x.IsAuthenticated))
        {
            int.TryParse(context.User.Claims.SingleOrDefault(c => c.Type == "sid")?.Value, out int UserId);

            session.UserId = UserId;
            session.Uid = context.User.Claims.SingleOrDefault(c => c.Type == "uid")?.Value;
        }

        // Call the next delegate/middleware in the pipeline
        await _next.Invoke(context);
    }
}