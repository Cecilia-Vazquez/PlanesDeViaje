using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanesDeViajes.Models.ViewModels
{
    public class DetallePlanViewModel
    {
        public int IdDetalle { get; set; }

        public string Descripcion { get; set; }
     
        public PlanesViewModel Planes { get; set; }

        public HotelesViewModel Hoteles { get; set; }


        public DetallePlanViewModel(Plan_Detalle detalle)
        {
            this.IdDetalle = detalle.IdDetalle;
            this.Descripcion = detalle.Descripcion;

            this.Planes = new PlanesViewModel(detalle.Planes);
            this.Hoteles = new HotelesViewModel(detalle.Hoteles);

        }
    }
}