using Assignment.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment.Controllers
{
    public class FinishController : Controller
    {
        public IActionResult Index()
        {
            List<Game> score = null;

            if(Request.Cookies.ContainsKey("score"))
            {
                score = JsonConvert.DeserializeObject<List<Game>>(Request.Cookies["score"]);
            }

            if(score ==  null)
            {
                score = new List<Game>();
            }

            return View(score);
        }
    }
}
