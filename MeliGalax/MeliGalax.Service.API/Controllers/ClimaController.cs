using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MeliGalax.Service.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClimaController : ControllerBase
    {
        private readonly Domain.DomainResultados.IDomainResultados DomainResultados;

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