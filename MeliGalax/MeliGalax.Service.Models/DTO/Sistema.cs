using System;

namespace MeliGalax.Service.Models.DTO
{
    public class Sistema
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Sistema"/> class.
        /// </summary>
        /// <param name="dia">Día del año del sistema solar.</param>
        /// <param name="planetaFerengi">Planeta Ferengi.</param>
        /// <param name="planetaBetasoide">Planeta Betasoide.</param>
        /// <param name="planetaVulcano">Planeta Vulcano.</param>
        public Sistema(int dia, Planeta planetaFerengi, Planeta planetaBetasoide, Planeta planetaVulcano)
        {
            Dia = dia;
            PlanetaFerengi = planetaFerengi;
            PlanetaBetasoide = planetaBetasoide;
            PlanetaVulcano = planetaVulcano;
        }

        /// <summary>
        /// Día del año del sistema solar.
        /// </summary>
        public int Dia { get; }

        /// <summary>
        /// Planeta Ferengi.
        /// </summary>
        public Planeta PlanetaFerengi { get; }

        /// <summary>
        /// Planeta Betasoide.
        /// </summary>
        public Planeta PlanetaBetasoide { get; }

        /// <summary>
        /// Planeta Vulcano.
        /// </summary>
        public Planeta PlanetaVulcano { get; }

        /// <summary>
        /// Obtiene el área total del triangulo de los planetas.
        /// </summary>
        /// <returns>Retorna el valor del área.</returns>
        public decimal ObtenerArea()
        {
            var puntoPlanetaFerengi = PlanetaFerengi.ObtenerPunto(Dia);
            var puntoPlanetaBetasoide = PlanetaBetasoide.ObtenerPunto(Dia);
            var puntoPlanetaVulcano = PlanetaVulcano.ObtenerPunto(Dia);

            return Math.Abs(Convert.ToDecimal(
                (puntoPlanetaFerengi.X *
                (puntoPlanetaBetasoide.Y - puntoPlanetaVulcano.Y)) +
                (puntoPlanetaBetasoide.X *
                (puntoPlanetaVulcano.Y - puntoPlanetaFerengi.Y)) +
                (puntoPlanetaVulcano.X *
                (puntoPlanetaFerengi.Y - puntoPlanetaBetasoide.Y))
            ));
        }

        /// <summary>
        /// Validación si en el triangulo conformado por los planetas se encuentra otro.
        /// </summary>
        /// <param name="planeta">Información del planeta.</param>
        /// <returns>True existe dentro del triangulo el planeta.</returns>
        public bool ContienePlaneta(Planeta planeta)
        {
            var sistema1 = new Sistema(Dia, planeta, PlanetaBetasoide, PlanetaVulcano);
            var sistema2 = new Sistema(Dia, planeta, PlanetaVulcano, PlanetaFerengi);
            var sistema3 = new Sistema(Dia, planeta, PlanetaFerengi, PlanetaBetasoide);

            return sistema1.ObtenerArea() + sistema2.ObtenerArea() + sistema3.ObtenerArea() == ObtenerArea();
        }

        /// <summary>
        /// Obtener el perímetro del triangulo conformado por los planetas.
        /// </summary>
        /// <returns>Perímetro del triangulo de los planetas.</returns>
        public double Perimetro()
        {
            var puntoPlanetaFerengi = PlanetaFerengi.ObtenerPunto(Dia);
            var puntoPlanetaBetasoide = PlanetaBetasoide.ObtenerPunto(Dia);
            var puntoPlanetaVulcano = PlanetaVulcano.ObtenerPunto(Dia);

            return puntoPlanetaFerengi.ObtenerDistancia(puntoPlanetaBetasoide) + puntoPlanetaBetasoide.ObtenerDistancia(puntoPlanetaVulcano) + puntoPlanetaVulcano.ObtenerDistancia(puntoPlanetaFerengi);
        }
    }
}