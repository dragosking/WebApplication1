using Microsoft.AspNetCore.Mvc;
using WebApplication1.Model;

using System.Web;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.ViewModel;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{

    [Route("api/[controller]")]
    public class IndexController: Controller
    {

       // HttpContext context = HttpContext.Current;

       
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost("latest")]
        public string SaveLatest([FromBody] LatestViewModel pl)
        {
            string test = pl.place;

            HttpContext.Session.SetString("SessionUser", JsonConvert.SerializeObject(SortTheList(test)));
            return test;
        }

        private string[] SortTheList(string input)
        {
            string[] test2 = new string[3];

            List<string> loadedList = LoadLatest();

            if (!loadedList.Any())
            {
                test2[0] = input;
                test2[1] = "";
                test2[2] = "";
            }
            else
            {
                if (loadedList.Contains(input))
                {
                    loadedList.Remove(input);
                }

                test2[0] = input;
                test2[1] = loadedList.First();
                test2[2] = loadedList.ElementAt(1);
            }

            return test2;
        }


        [HttpGet("[action]")]
        public List<string> LoadLatest()
        {

            var receive=HttpContext.Session.GetString("SessionUser");
            List<string> va = new List<string>();


            if (receive != null) {
                va = JsonConvert.DeserializeObject<List<string>>(receive);
            }

            return va;

        }


    }
}
