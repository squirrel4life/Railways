using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Railways.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        RailwayTicketOfficeDBEntities1 db = new RailwayTicketOfficeDBEntities1();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Тут Ви дізнаєтесь про нас трохи більше.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Хочете зв'язатися з нами? Тоді Вам сюди.";

            return View();
        }

       
    }
}