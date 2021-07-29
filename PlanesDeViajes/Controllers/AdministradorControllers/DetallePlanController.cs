using Newtonsoft.Json;
using PlanesDeViajes.APIs;
using PlanesDeViajes.Models;
using PlanesDeViajes.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PlanesDeViajes.Controllers
{
    public class DetallePlanController : Controller
    {
        string BaseUrl = "https://academy-dotnet-hotel1.azurewebsites.net/";
        // GET: DetallePlan
        public ActionResult Index()
        {
            List<DetallePlanViewModel> detalles;
            using (PlanDeViajeEntities dbContext = new PlanDeViajeEntities())
            {
                // detalles = dbContext.Plan_Detalle.Where(d => d.Planes.IdPlan = id).ToList();
                detalles = dbContext.Plan_Detalle.AsEnumerable().Select(d => new DetallePlanViewModel(d)).ToList();

            }

            return View(detalles);

        }
        public ActionResult Agrega()
        {
            Planes();
            Hoteles();
            return View();
        }

        public void Planes()
        {
           
                List<PlanesViewModel> planes;
                using (PlanDeViajeEntities dbContext = new PlanDeViajeEntities())
                {
                    planes = dbContext.Planes.AsEnumerable().Select(pv => new PlanesViewModel(pv)).ToList();
                    List<SelectListItem> itemsp = planes.ConvertAll(d =>
                     {
                        return new SelectListItem()
                       {
                        Text = d.Nombre.ToString(),
                        Value = d.IdPlan.ToString(),
                        Selected = false

                      };
                     });

                ViewBag.Itemsp = itemsp;
            }

            

        }

        public void Hoteles()
        {
            using (var client = new HttpClient())
            {
                List<SelectListItem> listado2 = new List<SelectListItem>();
                client.BaseAddress = new Uri("https://academy-dotnet-hotel1.azurewebsites.net/");
                var reques = client.GetAsync("api/locations").Result;
                if (reques.IsSuccessStatusCode)
                {
                    var resultString = reques.Content.ReadAsStringAsync().Result;
                    var listado = JsonConvert.DeserializeObject<List<Locacion>>(resultString);

                    List<SelectListItem> items = listado.ConvertAll(d =>
                    {
                        return new SelectListItem()
                        {
                            Text = d.Location.ToString(),
                            Value = d.id.ToString(),
                            Selected = false

                        };
                    });

                    ViewBag.Items = items;

                }


            }
        }
            [HttpPost]
        public ActionResult Agrega(NuevoDetallePlanViewModel model)
        {
     
            if (ModelState.IsValid)
            {
                try
                {
                    using (PlanDeViajeEntities dbcontext = new PlanDeViajeEntities())
                    {
                        Plan_Detalle detalleplan = new Plan_Detalle();
                       
                        detalleplan.IdHotel = model.IdHotel;
                        detalleplan.IdPlan = model.IdPlan;
                        detalleplan.Descripcion = model.Descripcion;
                        dbcontext.Plan_Detalle.Add(detalleplan);
                        dbcontext.SaveChanges();


                    }
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
                return Redirect("/DetallePlan");
            }

            return View(model);

        }

        public ActionResult Editar(int id)
        {
            NuevoDetallePlanViewModel model = new NuevoDetallePlanViewModel();
            try
            {
                using (PlanDeViajeEntities dbContext = new PlanDeViajeEntities())
                {
                    var plan = dbContext.Plan_Detalle.Where(p => p.IdDetalle == id).SingleOrDefault();
                    model.IdDetalle = plan.IdDetalle;
                    model.IdPlan = (int) plan.IdPlan;
                    model.IdHotel = (int)plan.IdHotel;
                    model.Descripcion= plan.Descripcion;


                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);

            }
            return View(model);

        }
        [HttpPost]
        public ActionResult Editar(NuevoDetallePlanViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (PlanDeViajeEntities dbContext = new PlanDeViajeEntities())
                    {

                        var plan = dbContext.Plan_Detalle.Find(model.IdDetalle);
                        plan.IdHotel = model.IdHotel;
                        plan.IdPlan = model.IdPlan;
                        plan.Descripcion = model.Descripcion;


                        dbContext.Entry(plan).State = System.Data.Entity.EntityState.Modified;
                        dbContext.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);

                }

                return Redirect("/DetallePlan");
            }
            return View(model);

        }

        [HttpGet]
        public ActionResult Eliminar(int Id)
        {
            try
            {
                using (PlanDeViajeEntities dbContext = new PlanDeViajeEntities())
                {

                    var plan = dbContext.Plan_Detalle.Find(Id);
                    dbContext.Plan_Detalle.Remove(plan);


                    dbContext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);

            }

            return Redirect("/DetallePlan");

        }


    }
}