using MeliGalax.Service.Models.Response;
using System.Threading.Tasks;

namespace MeliGalax.Service.Domain.DomainResultados
{
    public interface IDomainResultados
    {
        Task<ResponseBase<ResponseClima>> ConsultarResultado(int dia);
    }
}