namespace MeliGalax.Service.Domain.DomainDatos
{
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class DomainDatos : IDomainDatos
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<DomainDatos> Logger;

        /// <summary>
        /// The repository resultado
        /// </summary>
        private readonly Data.Base.IBaseRepository<Models.DAO.Resultado> RepositoryResultado;

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainDatos"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="repositoryResultado">The repository resultado.</param>
        public DomainDatos(ILogger<DomainDatos> logger,
                           Data.Base.IBaseRepository<Models.DAO.Resultado> repositoryResultado)
        {
            Logger = logger;
            RepositoryResultado = repositoryResultado;
        }

        /// <summary>
        /// Ejecución de la instancia para el cálculo de los años.
        /// </summary>
        public async Task Handle()
        {
            try
            {
                // Limpiar tabla de resultados
                await RepositoryResultado.DeleteAsync(predicate: w => w.Dia != 0);

                // Inicializar valores planetas
                var planetaFerengi = new Models.DTO.Planeta("Ferengi", 500, 1, 1);
                var planetaBetasoide = new Models.DTO.Planeta("Betasoide", 2000, 3, 1);
                var planetaVulcano = new Models.DTO.Planeta("Vulcano", 1000, 5, -1);
                var planetaBase = new Models.DTO.Planeta("Sol", 0, 0, 1);

                List<Models.DAO.Resultado> resultados = new List<Models.DAO.Resultado>();

                // Inicio de días hasta 10 años basados en 360 grados.
                for (int dia = 0; dia < 3600; dia++)
                {
                    var sistemaPlaneta = new Models.DTO.Sistema(dia, planetaFerengi, planetaBetasoide, planetaVulcano);

                    // Validación. Si contiene el planeta y el área es cero, se encuentra líneales
                    if (sistemaPlaneta.ContienePlaneta(planetaVulcano) && sistemaPlaneta.ObtenerArea() == 0)
                    {
                        // Validación. Si contiene el sol
                        if (sistemaPlaneta.ContienePlaneta(planetaBase))
                        {
                            resultados.Add(new Models.DAO.Resultado
                            {
                                Dia = dia,
                                Id = Guid.NewGuid(),
                                EstadoClima = "Sequía",
                                Perimetro = 0
                            });
                            continue;
                        }

                        resultados.Add(new Models.DAO.Resultado
                        {
                            Dia = dia,
                            Id = Guid.NewGuid(),
                            EstadoClima = "Óptimo",
                            Perimetro = 0
                        });
                        continue;
                    }
                    // Vaidación. Si contiene solo el sol.
                    else if (sistemaPlaneta.ContienePlaneta(planetaBase))
                    {
                        resultados.Add(new Models.DAO.Resultado
                        {
                            Dia = dia,
                            Id = Guid.NewGuid(),
                            EstadoClima = "Lluvia",
                            Perimetro = sistemaPlaneta.Perimetro()
                        });
                        continue;
                    }

                    resultados.Add(new Models.DAO.Resultado
                    {
                        Dia = dia,
                        Id = Guid.NewGuid(),
                        EstadoClima = "Soleado",
                        Perimetro = 0
                    });
                }

                // Validar los días de lluvia intensa.
                resultados = resultados.Select(s => { s.Intensidad = resultados.Max(m => m.Perimetro) == s.Perimetro ? "Intensa" : string.Empty; return s; }).ToList();

                // Almacenar la información.
                await RepositoryResultado.AddAsync(resultados);

                // Calcular la cantidad de días según el clima.
                var resumen = resultados.GroupBy(g => g.EstadoClima).Select(s => new { Clima = s.Key, Cantidad = s.Count() }).ToList();
                resumen.Add(new { Clima = "Lluvia Intensa", Cantidad = resultados.Count(c => !string.IsNullOrEmpty(c.Intensidad)) });

                Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(resumen));
            }
            catch (Exception exc)
            {
                Logger.LogError(exc.Message, exc.InnerException, exc.StackTrace);
            }
        }
    }
}