namespace AStrong_TestAPI.ConfigurationExtensions
{
    using DataAccess;
    using Microsoft.EntityFrameworkCore;

    public static class DataBaseConfigurationExtension
    {
        public static IServiceCollection AddAppDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(opt =>
            opt.UseSqlServer(configuration.GetConnectionString("AppDatabase")));

            return services;
        }
    }
}
