using PlanesDeViajes.Models;
using PlanesDeViajes.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlanesDeViajes.Controllers
{
    public class PlanesController : Controller
    {
        // GET: Planes
        public ActionResult Index()
        {
            List<PlanesViewModel> planes;
            using (PlanDeViajeEntities dbContext = new PlanDeViajeEntities())
            {
                planes = dbContext.Planes.AsEnumerable().Select(pv => new PlanesViewModel(pv)).ToList();

            }

            return View(planes);
           
        }


     
        public ActionResult VerDetallesPlan()
        {
            List<DetallePlanViewModel> detalles;
            using (PlanDeViajeEntities dbContext = new PlanDeViajeEntities())
            {
                // detalles = dbContext.Plan_Detalle.Where(d => d.Planes.IdPlan = id).ToList();
                detalles = dbContext.Plan_Detalle.AsEnumerable().Select(d => new DetallePlanViewModel(d)).ToList();

            }

            return View(detalles);
        }

        public ActionResult DetallesPlan(int id)
        {
            ViewBag.id = id;
            return View();

        }
        [HttpPost]
        
            public ActionResult DetallesPlan(NuevoDetallePlanViewModel model)
        {

          
            if (ModelState.IsValid)
            {
                try
                {

                    using (PlanDeViajeEntities dbcontext = new PlanDeViajeEntities())
                    {
                        

                        Plan_Detalle plan = new Plan_Detalle();
                        plan.IdPlan =model.IdPlan ;
                        plan.IdHotel = model.IdHotel;
                        plan.Descripcion = model.Descripcion;


                        dbcontext.Plan_Detalle.Add(plan);
                        dbcontext.SaveChanges();


                    }


                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
                return Redirect("/Planes");
            }

            return View(model);
           
        }
        public ActionResult Agrega()
        {
            return View();

        }
        [HttpPost]
        public ActionResult Agrega(NuevoPlanViewModel model)
        {
             
            if (ModelState.IsValid)
            {
                try
                {
                    using (PlanDeViajeEntities dbcontext = new PlanDeViajeEntities())
                    {

                        Planes plan = new Planes();
                        plan.IdPlan = model.IdPlan;
                        plan.Nombre= model.Nombre;
                        plan.Cupo = model.Cupo;
                      
                        dbcontext.Planes.Add(plan);
                        dbcontext.SaveChanges();


                    }

                    
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
                return Redirect("/Planes");
            }

            return View(model);

        }

        public ActionResult Editar(int id)
        {
            NuevoPlanViewModel model = new NuevoPlanViewModel();
            try
            {
                using (PlanDeViajeEntities dbContext = new PlanDeViajeEntities())
                {
                    var plan = dbContext.Planes.Where(p => p.IdPlan == id).SingleOrDefault();
                    model.IdPlan= plan.IdPlan;
                    model.Nombre = plan.Nombre;
                    model.Cupo = (int)plan.Cupo;
                    

                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);

            }
            return View(model);

        }
        [HttpPost]
        public ActionResult Editar(NuevoPlanViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (PlanDeViajeEntities dbContext = new PlanDeViajeEntities())
                    {

                        var plan = dbContext.Planes.Find(model.IdPlan);
                        plan.Nombre = model.Nombre;
                        plan.Cupo = model.Cupo;
                       

                        dbContext.Entry(plan).State = System.Data.Entity.EntityState.Modified;
                        dbContext.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);

                }

                return Redirect("/Planes");
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

                    var plan = dbContext.Planes.Find(Id);
                    dbContext.Planes.Remove(plan);


                    dbContext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);

            }

            return Redirect("/Planes");

        }
    }

}
