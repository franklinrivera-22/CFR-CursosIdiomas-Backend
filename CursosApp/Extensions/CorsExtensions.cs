namespace CursosApp.Extensions
{
    public static class CorsExtensions
    {
        public static IServiceCollection AddCorsConfiguration(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(opt =>
            {
                var allowUrls = configuration.GetSection("AllowURLS").Get<string[]>() ?? new[] { "" };

                opt.AddPolicy("CorsPolicy", builder => builder
                    .WithOrigins(allowUrls)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            return services;
        }
    }
}