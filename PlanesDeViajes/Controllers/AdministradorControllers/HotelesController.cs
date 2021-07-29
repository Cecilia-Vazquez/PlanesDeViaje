using Newtonsoft.Json;
using PlanesDeViajes.APIs;
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
    public class HotelesController : Controller
    {
        // GET: Hoteles

        string BaseUrl = "https://academy-dotnet-hotel1.azurewebsites.net/";
        public async Task<ActionResult> Index()
        {
            using (var client = new HttpClient())
            {
                List<Locacion> listado2 = new List<Locacion>();
                client.BaseAddress = new Uri("https://academy-dotnet-hotel1.azurewebsites.net/");
                var reques = client.GetAsync("api/locations").Result;
                if (reques.IsSuccessStatusCode)
                {
                    var resultString = reques.Content.ReadAsStringAsync().Result;
                    var listado = JsonConvert.DeserializeObject<List<Locacion>>(resultString);

                    foreach (var item in listado)
                    {
                        listado2.Add(item);

                    }



                }

                return View(listado2);
            }

         
        }


        }
    }