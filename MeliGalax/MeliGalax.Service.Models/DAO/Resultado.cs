namespace MeliGalax.Service.Models.DAO
{
    /// <summary>
    /// Resultado
    /// </summary>
    public class Resultado
    {
        /// <summary>
        /// Identificador.
        /// </summary>
        public System.Guid Id { get; set; }

        /// <summary>
        /// Día.
        /// </summary>
        public int Dia { get; set; }

        /// <summary>
        /// Estado del Clima.
        /// </summary>
        public string EstadoClima { get; set; }

        /// <summary>
        /// Intensidad.
        /// </summary>
        public string Intensidad { get; set; }

        /// <summary>
        /// Perímetro.
        /// </summary>
        public double Perimetro { get; set; }

        /// <summary>
        /// Obtener clima.
        /// </summary>
        public string Clima => $"{EstadoClima} {Intensidad}".Trim();
    }
}