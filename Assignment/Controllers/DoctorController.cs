using Assignment.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment.Controllers
{
    [Route("/FeverCheck")]
    public class DoctorController : Controller
    {
        // FeverCheck
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Temperature entry)
        {
            if(ModelState.IsValid == false)
            {
                return View(entry);
            }

            return Content(Temperature.Check(entry.Value));
        }
    }
}
