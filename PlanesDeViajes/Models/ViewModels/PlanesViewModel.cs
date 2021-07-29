using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanesDeViajes.Models.ViewModels
{
    public class PlanesViewModel
    {
        public int IdPlan { get; set; }
        public string Nombre { get; set; }
        public int Cupo { get; set; }


         public PlanesViewModel(Planes planes)
            {
                this.IdPlan = planes.IdPlan;
                this.Nombre = planes.Nombre;
              
                this.Cupo = (int)planes.Cupo;

            }
        
    }
}