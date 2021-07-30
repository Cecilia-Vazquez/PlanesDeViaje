using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanesDeViajes.Models.ViewModels
{
    public class NuevoReservacionesViewModel
    {

        public int IdReservacion { get; set; }
        public int IdDetalle { get; set; }
        public string Cliente { get; set; }
        public string Correo { get; set; }

    }
}