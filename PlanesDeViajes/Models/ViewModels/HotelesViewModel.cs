using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanesDeViajes.Models.ViewModels
{
    public class HotelesViewModel
    {
        public int IdHotel { get; set; }

        public string Locacion { get; set; }



        public HotelesViewModel(Hoteles hotel)
        {
            this.IdHotel = hotel.IdHotel;

            this.Locacion = hotel.Locacion;
          

        }
    }
}