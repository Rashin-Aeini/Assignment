using Assignment.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Assignment.Controllers
{
    [Route("/GuessingGame")]
    public class GameController : Controller
    {
        public IActionResult Index()
        {
            if(string.IsNullOrEmpty(HttpContext.Session.GetString("number")))
            {
                HttpContext.Session.SetString("number", (new Random()).Next(1, 100).ToString());
            }

            ViewBag.Number = HttpContext.Session.GetString("number");

            CookieOptions options = new CookieOptions();

            if (!HttpContext.Request.Cookies.ContainsKey("score"))
            {
                options.Expires = DateTime.Now.AddHours(1);
                HttpContext.Response.Cookies.Append("score", "", options);
            }

            Game model = new Game();

            if (!HttpContext.Request.Cookies.ContainsKey("counter"))
            {
                model.Counter = 0;
            }
            else
            {
                model.Counter = int.Parse(Request.Cookies["counter"]);
            }

            options.Expires = DateTime.Now.AddMinutes(10);
            HttpContext.Response.Cookies.Append("counter", model.Counter.ToString(), options);

            return View(model);
        }

        [HttpPost]
        public IActionResult Index(Game entry)
        {
            
            ViewBag.Number = HttpContext.Session.GetString("number");

            if (!ModelState.IsValid)
            {
                return View(entry);
            }

            if (!HttpContext.Request.Cookies.ContainsKey("counter"))
            {
                entry.Counter = 0;
            }
            else
            {
                entry.Counter = int.Parse(Request.Cookies["counter"]);
            }

            entry.Counter++;

            if(string.IsNullOrEmpty(HttpContext.Session.GetString("number")))
            {
                return RedirectToAction("Index");
            }

            CookieOptions options = new CookieOptions();

            if (entry.Guess == int.Parse(HttpContext.Session.GetString("number")))
            {
                string value = string.Empty;
                if(Request.Cookies.ContainsKey("score"))
                {
                    value = Request.Cookies["score"];
                }

                List<Game> scores = JsonConvert.DeserializeObject<List<Game>>(value);

                List<Game> temp = new List<Game>();

                if(scores == null)
                {
                    temp.Add(entry);
                }
                else
                {
                    foreach (Game item in scores)
                    {
                        if (item.Counter < entry.Counter)
                        {
                            temp.Add(item);
                        }
                        else
                        {
                            temp.Add(entry);
                            temp.Add(item);
                        }
                    }
                }

                options.Expires = DateTime.Now.AddHours(1);
                HttpContext.Response.Cookies.Append("score", JsonConvert.SerializeObject(temp), options);

                Response.Cookies.Delete("counter");

                return RedirectToAction("Index", "Finish");
            }

            options.Expires = DateTime.Now.AddMinutes(10);
            HttpContext.Response.Cookies.Append("counter", entry.Counter.ToString(), options);

            if(entry.Guess < int.Parse(HttpContext.Session.GetString("number")))
            {
                ViewBag.Guess = "your guess is lower than my number";
            }
            else
            {
                ViewBag.Guess = "your guess is greather than my number";
            }

            return View(entry);
        }
    }
}
