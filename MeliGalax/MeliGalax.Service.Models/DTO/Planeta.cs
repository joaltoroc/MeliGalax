using MeliGalax.Service.Tools.Extensions;
using System;

namespace MeliGalax.Service.Models.DTO
{
    public class Planeta
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Planeta"/> class.
        /// </summary>
        /// <param name="nombre">Nombre del planeta.</param>
        /// <param name="distancia">Distancia del planeta respecto al Sol.</param>
        /// <param name="grados">Grados de desplazamiento del planeta.</param>
        /// <param name="horario">Horario. 1 (Horario) -1 (Antihorario).</param>
        public Planeta(string nombre, int distancia, int grados, int horario)
        {
            Nombre = nombre;
            Distancia = distancia;
            Grados = grados;
            Horario = horario;
        }

        /// <summary>
        /// Nombre del planeta.
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Distancia del planeta respecto al Sol.
        /// </summary>
        public int Distancia { get; set; }

        /// <summary>
        /// Grados de desplazamiento del planeta.
        /// </summary>
        public int Grados { get; set; }

        /// <summary>
        /// Horario. 1 (Horario) -1 (Antihorario).
        /// </summary>
        public int Horario { get; set; }

        /// <summary>
        /// Se obtiene el punto en el que se encuentra el planeta basado en los radianes.
        /// </summary>
        public Punto ObtenerPunto(int dia)
        {
            var posicion = (dia * Grados * Horario) % 360;
            var radians = posicion.ToRadians();

            var x = Math.Cos(radians) * Distancia;
            var y = Math.Sin(radians) * Distancia;

            return new Punto(x, y);
        }
    }
}