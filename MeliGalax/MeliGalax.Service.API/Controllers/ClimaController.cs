using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MeliGalax.Service.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClimaController : ControllerBase
    {
        /// <summary>
        /// The domain resultados
        /// </summary>
        private readonly Domain.DomainResultados.IDomainResultados DomainResultados;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClimaController"/> class.
        /// </summary>
        /// <param name="domainResultados">The domain resultados.</param>
        public ClimaController(Domain.DomainResultados.IDomainResultados domainResultados)
        {
            DomainResultados = domainResultados;
        }

        /// <summary>
        /// Obtiene el estado del clima en el día ingresado.
        /// </summary>
        /// <param name="dia">Día a consultar.</param>
        /// <returns>Información del clima.</returns>
        [HttpGet("{dia}")]
        public async Task<IActionResult> ObtenerClima(int dia)
        {
            var resultado = await DomainResultados.ConsultarResultado(dia);

            return StatusCode(resultado.Codigo, resultado);
        }
    }
}