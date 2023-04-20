namespace PortalGrupoAlyne
{
    public class CustomErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);

            if (context.Response.StatusCode == 401)
            {
                context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            }
        }
    }

    public static class CustomErrorHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomErrorHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomErrorHandlerMiddleware>();
            return app;
        }
    }

}
