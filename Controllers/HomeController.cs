using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Food_Enforcement.Models;
using static Food_Enforcement.Models.EF_Models;
//using static Food_Enforcement.APIHandlerManager.APIHandler;
using Food_Enforcement.DataAccess;
using System.Net.Http;
using Newtonsoft.Json;

namespace Food_Enforcement.Controllers
{
    
    public class HomeController : Controller
    {

        public ApplicationDBContext dbContext;
        //Base URL for the IEXTrading API. Method specific URLs are appended to this base URL.

        static string BASE_URL = "https://api.fda.gov/food/";
       
        HttpClient httpClient;

        public HomeController(ApplicationDBContext context)
        {
            dbContext = context;

            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new
                System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public RootObject GetRootObject()
        {
            string FOOD_API_PATH = BASE_URL + "/enforcement.json?search=report_date:[20040101+TO+20131231]&limit=50";
            string foodData = "";

            RootObject rootObject = null;

            httpClient.BaseAddress = new Uri(FOOD_API_PATH);

            // It can take a few requests to get back a prompt response, if the API has not received
            //  calls in the recent past and the server has put the service on hibernation
            try
            {
                HttpResponseMessage response = httpClient.GetAsync(FOOD_API_PATH).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    foodData = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                }

                if (!foodData.Equals(""))
                {
                    // JsonConvert is part of the NewtonSoft.Json Nuget package
                    rootObject = JsonConvert.DeserializeObject<RootObject>(foodData);

                    dbContext.Meta.Add(rootObject.meta);
                    dbContext.Results.Add(rootObject.meta.results);

                    foreach (Result result in rootObject.results)
                    {
                        dbContext.Result.Add(result);
                    }


                    dbContext.SaveChanges();
                    ViewBag.dbSuccessComp = 1;
                }
            }

            catch (Exception e)
            {
                // This is a useful place to insert a breakpoint and observe the error message
                Console.WriteLine(e.Message);
            }

            return rootObject;
        }
        public IActionResult Index()
        {
            ViewBag.dbSuccessComp = 0;
            RootObject rootObject = GetRootObject();

            return View(rootObject);
        }

        public IActionResult MainView()
        {
                       
            //Food_Enforcement.APIHandlerManager.APIHandler webHandler = new Food_Enforcement.APIHandlerManager.APIHandler();
            RootObject rootObject = GetRootObject();

            return View(rootObject);

        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
