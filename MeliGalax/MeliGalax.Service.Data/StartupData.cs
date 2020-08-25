namespace MeliGalax.Service
{
    using MeliGalax.Service.Models.Constants;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System;

    public static class StartupData
    {
        public static void AddData(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Data.Context.MeliGaxContext>(options =>
            {
                options.UseSqlServer(configuration.GetValue<string>(KeyVault.MGConnection), sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(3, TimeSpan.FromSeconds(10), null);
                    sqlOptions.CommandTimeout(60);
                });
            }, ServiceLifetime.Transient);

            services.AddScoped(typeof(Data.Base.IBaseRepository<>), typeof(Data.Base.BaseRepository<>));
        }
    }
}