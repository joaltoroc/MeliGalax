using System.Threading.Tasks;

namespace MeliGalax.Service.Domain.DomainDatos
{
    public interface IDomainDatos
    {
        Task Handle();
    }
}