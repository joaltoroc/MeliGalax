using System.Net;

namespace MeliGalax.Service.Models.Response
{
    public class ResponseBase<T>
    {
        public ResponseBase(HttpStatusCode codigo = HttpStatusCode.OK, string mensaje = null, T dato = default)
        {
            Codigo = (int)codigo;
            Mensaje = mensaje;
            Dato = dato;
        }

        public int Codigo { get; set; }

        public string Mensaje { get; set; }

        public T Dato { get; set; }
    }
}