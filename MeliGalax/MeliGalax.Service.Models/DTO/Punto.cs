using System;

namespace MeliGalax.Service.Models.DTO
{
    public class Punto
    {
        public Punto(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X { get; set; }

        public double Y { get; set; }

        public double ObtenerDistancia(Punto punto)
        {
            return Math.Sqrt(Math.Pow(punto.X - X, 2) + Math.Pow(punto.Y - Y, 2));
        }
    }
}