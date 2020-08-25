using MeliGalax.Service.Tools.Extensions;
using System;

namespace MeliGalax.Service.Models.DTO
{
    public class Planeta
    {
        public Planeta(string nombre, int distancia, int grados, int horario)
        {
            Nombre = nombre;
            Distancia = distancia;
            Grados = grados;
            Horario = horario;
        }

        public string Nombre { get; set; }

        public int Distancia { get; set; }

        public int Grados { get; set; }

        public int Horario { get; set; }

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