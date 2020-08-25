namespace MeliGalax.Service
{
    using Microsoft.Extensions.DependencyInjection;

    public static class StartupDomain
    {
        public static void AddDomain(this IServiceCollection services)
        {
            services.AddScoped<Domain.DomainDatos.IDomainDatos, Domain.DomainDatos.DomainDatos>();
            services.AddScoped<Domain.DomainResultados.IDomainResultados, Domain.DomainResultados.DomainResultados>();
        }
    }
}
