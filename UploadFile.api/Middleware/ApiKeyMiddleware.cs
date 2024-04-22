namespace UploadFile.api.Middleware
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _config;

        public ApiKeyMiddleware(RequestDelegate next, IConfiguration config)
        {
            _next = next;
            _config = config;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.Path.StartsWithSegments("/swagger"))
            {
                if (!context.Request.Headers.TryGetValue("x-api-key", out var appKey))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Falta llave de aplicacion.");
                    return;
                }

                var validAppKey = _config["AppSettings:ApiKey"]; // Obtener la clave de configuración
                if (appKey != validAppKey)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Llave de aplicacion invalida.");
                    return;
                }
            }
            
            await _next(context);
        }
    }
}

