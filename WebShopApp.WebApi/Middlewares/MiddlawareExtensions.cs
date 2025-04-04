using WebShopApp.WebApi.Middlewares;

namespace WebShopApp.WebApi.Middlewares
{
    public static class MiddlawareExtensions
    {
        public static IApplicationBuilder UseMaintenanceMode(this IApplicationBuilder app)
        {
            return app.UseMiddleware<MaintenanceMiddleware>();
        }

        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}