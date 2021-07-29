using PlanesDeViajes.Models;
using PlanesDeViajes.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlanesDeViajes.Controllers.ClienteControllers
{
    public class PlanesClienteController : Controller
    {
        // GET: PlanesCliente
        public ActionResult Index()
        {
            List<PlanesViewModel> planes;
            using (PlanDeViajeEntities dbContext = new PlanDeViajeEntities())
            {
                planes = dbContext.Planes.AsEnumerable().Select(pv => new PlanesViewModel(pv)).ToList();

            }

            return View(planes);
            
        }
    }
}