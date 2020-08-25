namespace MeliGalax.Service.Tools.Extensions
{
    using System;

    public static class ConvertExtension
    {
        public static double ToRadians(this int degrees)
        {
            double radians = (Math.PI / 180) * degrees;
            return (radians);
        }
    }
}