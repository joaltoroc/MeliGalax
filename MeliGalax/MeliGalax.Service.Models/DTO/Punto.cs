using System;

namespace MeliGalax.Service.Models.DTO
{
    public class Punto
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Punto"/> class.
        /// </summary>
        /// <param name="x">Posición en el eje X.</param>
        /// <param name="y">Posición en el eje Y.</param>
        public Punto(double x, double y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Posición en el eje X.
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Posición en el eje Y.
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// Obtener la distancia entre dos puntos.
        /// </summary>
        public double ObtenerDistancia(Punto punto)
        {
            return Math.Sqrt(Math.Pow(punto.X - X, 2) + Math.Pow(punto.Y - Y, 2));
        }
    }
}