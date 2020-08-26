using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;

namespace MeliGalax.Service.Domain.DomainResultados
{
    public class DomainResultados : IDomainResultados
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<DomainResultados> Logger;

        /// <summary>
        /// The repository resultado
        /// </summary>
        private readonly Data.Base.IBaseRepository<Models.DAO.Resultado> RepositoryResultado;

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainResultados"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="repositoryResultado">The repository resultado.</param>
        public DomainResultados(ILogger<DomainResultados> logger,
                                Data.Base.IBaseRepository<Models.DAO.Resultado> repositoryResultado)
        {
            Logger = logger;
            RepositoryResultado = repositoryResultado;
        }

        /// <summary>
        /// Consultar los resultados basados en los datos generados.
        /// </summary>
        /// <param name="dia">El día a consultar.</param>
        /// <returns>
        /// Información del clima.
        /// </returns>
        public async Task<Models.Response.ResponseBase<Models.Response.ResponseClima>> ConsultarResultado(int dia)
        {
            try
            {
                var datos = await RepositoryResultado.GetAsync(w => w.Dia == dia);

                return new Models.Response.ResponseBase<Models.Response.ResponseClima>(dato: new Models.Response.ResponseClima
                {
                    Clima = datos.Clima,
                    Dia = datos.Dia
                });
            }
            catch (System.Exception exc)
            {
                Logger.LogError(exc.Message, exc.InnerException, exc.StackTrace);

                return new Models.Response.ResponseBase<Models.Response.ResponseClima>(HttpStatusCode.InternalServerError, exc.Message);
            }
        }
    }
}