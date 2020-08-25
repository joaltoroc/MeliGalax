namespace MeliGalax.Service.Domain.DomainDatos
{
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class DomainDatos : IDomainDatos
    {
        private readonly ILogger<DomainDatos> Logger;

        private readonly Data.Base.IBaseRepository<Models.DAO.Resultado> RepositoryResultado;

        public DomainDatos(ILogger<DomainDatos> logger,
                           Data.Base.IBaseRepository<Models.DAO.Resultado> repositoryResultado)
        {
            Logger = logger;
            RepositoryResultado = repositoryResultado;
        }

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

                // Inicio de días hasta 10 años
                for (int dia = 0; dia < 3600; dia++)
                {
                    var sistemaPlaneta = new Models.DTO.Sistema(dia, planetaFerengi, planetaBetasoide, planetaVulcano);

                    if (sistemaPlaneta.ContienePlaneta(planetaVulcano) && sistemaPlaneta.ObtenerArea() == 0)
                    {
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

                resultados = resultados.Select(s => { s.Intensidad = resultados.Max(m => m.Perimetro) == s.Perimetro ? "Intensa" : string.Empty; return s; }).ToList();

                await RepositoryResultado.AddAsync(resultados);

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