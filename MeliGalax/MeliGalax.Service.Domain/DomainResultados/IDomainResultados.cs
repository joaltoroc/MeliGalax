using MeliGalax.Service.Models.Response;
using System.Threading.Tasks;

namespace MeliGalax.Service.Domain.DomainResultados
{
    public interface IDomainResultados
    {
        /// <summary>
        /// Consultar los resultados basados en los datos generados.
        /// </summary>
        /// <param name="dia">El día a consultar.</param>
        /// <returns>Información del clima.</returns>
        Task<ResponseBase<ResponseClima>> ConsultarResultado(int dia);
    }
}