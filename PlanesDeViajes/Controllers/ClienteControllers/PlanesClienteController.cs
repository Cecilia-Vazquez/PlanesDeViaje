using Newtonsoft.Json;
using PlanesDeViajes.APIs;
using PlanesDeViajes.Models;
using PlanesDeViajes.Models.ViewModels;
using PlanesDeViajes.Notificaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
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
        public ActionResult VerHotelesPlan(int id)
        {
            ViewBag.id = id;
            ViewBag.idplan = id;
            using (var plan = new PlanDeViajeEntities())
            {
                int idPlan = id;
                var hotel = plan.Plan_Detalle
                              .Where(p => p.Planes.IdPlan == idPlan)
                              .ToList();
                ViewBag.items = hotel;
            }
            return View();
        }

        public void Habitaciones(int idhotel, int idmes, int ida)//Metodo para consumir API traer las habitaciones para reservar
        {
            using (var client = new HttpClient())
            {
                List<Habitacion> listado2 = new List<Habitacion>();
                client.BaseAddress = new Uri("https://academy-dotnet-hotel1.azurewebsites.net/");
                var reques = client.GetAsync("api/room/l/state/" + idhotel + "/" + idmes + "/" + ida + "/").Result;
                if (reques.IsSuccessStatusCode)
                {
                    var resultString = reques.Content.ReadAsStringAsync().Result;
                    var listado = JsonConvert.DeserializeObject<List<Habitacion>>(resultString);

                    foreach (var item in listado)
                    {
                        listado2.Add(item);

                    }



                }


            }
        }

        public ActionResult VerDisponibilidad(int id, int idplan)
        {
            ViewBag.id = id;
            ViewBag.idplan = idplan;
            return View();
        }
        [HttpPost]
        public ActionResult VerDisponibilidad(int idh, int idmes, int idao, int idp)
        {
            ViewBag.IdHotel = idh;
            ViewBag.IdMes = idmes;
            ViewBag.IdAn = idao;
            ViewBag.IdPlan = idp;

            return RedirectToAction("VerHabitaciones", "PlanesCliente", new { id = idh, idm = idmes, ida = idao, idplan = idp });



        }
        public ActionResult VerHabitaciones(int id, int idm, int ida, int idplan)
        {
            ViewBag.idplan = idplan;
            using (var client = new HttpClient())
            {
                List<Habitacion> listado2 = new List<Habitacion>();
                client.BaseAddress = new Uri("https://academy-dotnet-hotel1.azurewebsites.net/");
                var reques = client.GetAsync("api/room/l/state/" + id + "/" + idm + "/" + ida + "/").Result;
                if (reques.IsSuccessStatusCode)
                {
                    var resultString = reques.Content.ReadAsStringAsync().Result;
                    var listado = JsonConvert.DeserializeObject<List<Habitacion>>(resultString);

                    foreach (var item in listado)
                    {
                        listado2.Add(item);

                    }
                    return View(listado2);
                }
            }
            return View();
        }

        public ActionResult Reserva(int idRoom, int idplan)
        {
            ViewBag.id = idRoom;
            ViewBag.idplan = idplan;

            return View();
        }


        [HttpPost]
        public ActionResult Reserva(NuevoReservacionesViewModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    using (PlanDeViajeEntities dbcontext = new PlanDeViajeEntities())
                    {

                        Reservaciones reservacion = new Reservaciones();
                        reservacion.IdReservacion = model.IdReservacion;
                        reservacion.IdDetalle = model.IdDetalle;
                        reservacion.Cliente = model.Cliente;
                        reservacion.Correo = model.Correo;

                        dbcontext.Reservaciones.Add(reservacion);
                        dbcontext.SaveChanges();
                        var mensaje = new MailMessage();
                        mensaje.Subject = "Reservacion Plan De viaje";
                        mensaje.Body = "Hola!\n" + model.Cliente +
                            " Gracias Por Reservar \n" +
                            "Su Reservación fue exitosa!";

                        mensaje.To.Add(model.Correo);
                        mensaje.IsBodyHtml = true;
                        var smtp = new SmtpClient();
                        smtp.Send(mensaje);

                    }

                   

                   
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }

                return Redirect("/PlanesCliente");
            }

            return View(model);

        }
        public ActionResult Notificacion()
        {
            return View();
        }

            [HttpPost]
        public ActionResult Notificacion(Email model)
        {
            var mensaje = new MailMessage();
            mensaje.Subject = model.Asunto;
            mensaje.Body = model.Mensaje;
            mensaje.To.Add(model.Destino);

            mensaje.IsBodyHtml = true;
            var smtp = new SmtpClient();
            smtp.Send(mensaje);
            return Redirect("/PlanesCliente");
            
        }



        
    }
}

