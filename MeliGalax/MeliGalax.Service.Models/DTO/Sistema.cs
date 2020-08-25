using System;

namespace MeliGalax.Service.Models.DTO
{
    public class Sistema
    {
        public Sistema(int dia, Planeta planetaFerengi, Planeta planetaBetasoide, Planeta planetaVulcano)
        {
            Dia = dia;
            PlanetaFerengi = planetaFerengi;
            PlanetaBetasoide = planetaBetasoide;
            PlanetaVulcano = planetaVulcano;
        }

        public int Dia { get; }
        public Planeta PlanetaFerengi { get; }
        public Planeta PlanetaBetasoide { get; }
        public Planeta PlanetaVulcano { get; }

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

        public bool ContienePlaneta(Planeta planeta)
        {
            var sistema1 = new Sistema(Dia, planeta, PlanetaBetasoide, PlanetaVulcano);
            var sistema2 = new Sistema(Dia, planeta, PlanetaVulcano, PlanetaFerengi);
            var sistema3 = new Sistema(Dia, planeta, PlanetaFerengi, PlanetaBetasoide);

            return sistema1.ObtenerArea() + sistema2.ObtenerArea() + sistema3.ObtenerArea() == ObtenerArea();
        }

        public double Perimetro()
        {
            var puntoPlanetaFerengi = PlanetaFerengi.ObtenerPunto(Dia);
            var puntoPlanetaBetasoide = PlanetaBetasoide.ObtenerPunto(Dia);
            var puntoPlanetaVulcano = PlanetaVulcano.ObtenerPunto(Dia);

            return puntoPlanetaFerengi.ObtenerDistancia(puntoPlanetaBetasoide) + puntoPlanetaBetasoide.ObtenerDistancia(puntoPlanetaVulcano) + puntoPlanetaVulcano.ObtenerDistancia(puntoPlanetaFerengi);
        }
    }
}