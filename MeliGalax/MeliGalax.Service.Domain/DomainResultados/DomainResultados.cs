using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;

namespace MeliGalax.Service.Domain.DomainResultados
{
    public class DomainResultados : IDomainResultados
    {

        private readonly ILogger<DomainResultados> Logger;

        private readonly Data.Base.IBaseRepository<Models.DAO.Resultado> RepositoryResultado;

        public DomainResultados(ILogger<DomainResultados> logger,
                                Data.Base.IBaseRepository<Models.DAO.Resultado> repositoryResultado)
        {
            Logger = logger;
            RepositoryResultado = repositoryResultado;
        }

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