using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanesDeViajes.Notificaciones
{
    public class Email
    {
        public string Destino { get; set; }
        public string Asunto { get; set; }
        public string Mensaje { get; set; }
    }
}