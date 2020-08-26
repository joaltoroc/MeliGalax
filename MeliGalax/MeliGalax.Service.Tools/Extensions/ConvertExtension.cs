namespace MeliGalax.Service.Tools.Extensions
{
    using System;

    public static class ConvertExtension
    {
        /// <summary>
        /// Convertir grados en radianes.
        /// </summary>
        /// <param name="degrees">Grados.</param>
        /// <returns>Valor en Radianes.</returns>
        public static double ToRadians(this int degrees)
        {
            double radians = (Math.PI / 180) * degrees;
            return radians;
        }
    }
}