using BLL.Interfaces.Repositories;
using BLL.Interfaces.Services;
using BLL.Services;
using BLL.Interfaces;
namespace API.Middleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<AuthenticationMiddleware> _logger;

        private static readonly HashSet<string> ExcludedPaths = new(StringComparer.OrdinalIgnoreCase)
        {
            "/v3/api-docs", "/login", "/register"
        };
        private static readonly string[] ExcludedPrefixes = { "/external", "/movie", "/show", "/swagger", "/environments" };

        private bool IsExcludedPath(string path)
        {
            if (ExcludedPaths.Contains(path))
                return true;

            foreach (var prefix in ExcludedPrefixes)
            {
                if (path.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }

        public AuthenticationMiddleware(RequestDelegate next, IServiceScopeFactory scopeFactory, ILogger<AuthenticationMiddleware> logger)
        {
            _next = next;
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var path = context.Request.Path.Value?.TrimEnd('/').ToLower() ?? "";
            if (IsExcludedPath(path))
            {
                await _next(context);
                return;
            }

            try
            {
                // Create a scope to resolve the UserService
                using (var scope = _scopeFactory.CreateScope())
                {
                    var userService = scope.ServiceProvider.GetRequiredService<IUserService>();

                    var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                    if (string.IsNullOrEmpty(token) || token == "null")
                    {
                        RejectRequest(context);
                        return;
                    }

                    var user = userService.ValidateToken(token);

                    if (IsProtectedResource(context) && user == null)
                    {
                        RejectRequest(context);
                        return;
                    }

                    context.Items["User"] = user;

                    if (IsProtectedResource(context))
                    {
                        await _next(context);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log any exceptions that occur during authentication
                _logger.LogError(ex, "An error occurred while processing the authentication middleware.");
                RejectRequest(context);
            }
        }

        private void RejectRequest(HttpContext context)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";
            context.Response.WriteAsync("{\"error\": \"Unauthorized access. Invalid or expired token.\"}");
        }

        private bool IsProtectedResource(HttpContext context)
        {
            var path = context.Request.Path.Value.ToLower();
            return !(path == "/login" || path == "/register" || context.Request.Method == "OPTIONS");
        }

    }
}
