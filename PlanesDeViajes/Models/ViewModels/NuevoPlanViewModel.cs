using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PlanesDeViajes.Models.ViewModels
{
    public class NuevoPlanViewModel
    {

        public int IdPlan { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
        public int Cupo { get; set; }

 
        
  

      
    }
}