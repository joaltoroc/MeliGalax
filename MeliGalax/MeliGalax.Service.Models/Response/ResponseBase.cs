using System.Net;

namespace MeliGalax.Service.Models.Response
{
    public class ResponseBase<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseBase{T}"/> class.
        /// </summary>
        /// <param name="codigo">Código de respuesta.</param>
        /// <param name="mensaje">Mensaje de respuesta.</param>
        /// <param name="dato">Información del dato.</param>
        public ResponseBase(HttpStatusCode codigo = HttpStatusCode.OK, string mensaje = null, T dato = default)
        {
            Codigo = (int)codigo;
            Mensaje = mensaje;
            Dato = dato;
        }

        /// <summary>
        /// Código de respuesta.
        /// </summary>
        public int Codigo { get; set; }

        /// <summary>
        /// Mensaje de respuesta.
        /// </summary>
        public string Mensaje { get; set; }

        /// <summary>
        /// Información del dato.
        /// </summary>
        public T Dato { get; set; }
    }
}