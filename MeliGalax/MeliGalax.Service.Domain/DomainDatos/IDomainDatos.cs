using System.Threading.Tasks;

namespace MeliGalax.Service.Domain.DomainDatos
{
    public interface IDomainDatos
    {
        /// <summary>
        /// Ejecución de la instancia para el cálculo de los años.
        /// </summary>
        Task Handle();
    }
}