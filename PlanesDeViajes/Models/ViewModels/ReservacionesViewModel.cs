using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanesDeViajes.Models.ViewModels
{
    public class ReservacionesViewModel
    {
        public int IdReservacion { get; set; }
        public DetallePlanViewModel Detalles { get; set; }
        public string Cliente { get; set; }
        public string Correo { get; set; }
       

        public ReservacionesViewModel(Reservaciones res)
        {
            this.IdReservacion= res.IdReservacion;
            this.Detalles = new DetallePlanViewModel(res.Plan_Detalle);
            this.Cliente= res.Cliente;
            this.Correo = res.Correo;


   


        }
    }
}