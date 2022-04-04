using Microsoft.OpenApi.Models;

namespace Api.Extensions
{
    public static class ApiServiceExtensions
    {
        public static IServiceCollection AddApi(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });
            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .WithExposedHeaders("WWW-Authenticate", "Pagination")
                        .WithOrigins("https://agreeable-sea-096c34603.1.azurestaticapps.net");
                        //.SetIsOriginAllowed(origin => true);

                });
            });

            return services;
        }
    }
}
