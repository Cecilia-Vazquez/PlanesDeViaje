using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PlanesDeViajes.Models.ViewModels
{
    public class NuevoDetallePlanViewModel
    {

        public int IdDetalle { get; set; }
        [Required]
        
        public int IdHotel { get; set; }

        public int IdPlan { get; set; }

        public string Descripcion{ get; set; }



    }
}