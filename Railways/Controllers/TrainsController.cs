using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Railways;

namespace Railways.Controllers
{
    public class TrainsController : Controller
    {
        private RailwayTicketOfficeDBEntities1 db = new RailwayTicketOfficeDBEntities1();

        // GET: /Trains/
        public ActionResult Index()
        {
            return View(db.Trains.ToList());
        }

        // GET: /Trains/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trains trains = db.Trains.Find(id);
            if (trains == null)
            {
                return HttpNotFound();
            }
            return View(trains);
        }

        // GET: /Trains/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Trains/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="TrainID,Number,Route")] Trains trains)
        {
            if (ModelState.IsValid)
            {
                db.Trains.Add(trains);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(trains);
        }

        // GET: /Trains/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trains trains = db.Trains.Find(id);
            if (trains == null)
            {
                return HttpNotFound();
            }
            return View(trains);
        }

        // POST: /Trains/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="TrainID,Number,Route")] Trains trains)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trains).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(trains);
        }

        // GET: /Trains/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trains trains = db.Trains.Find(id);
            if (trains == null)
            {
                return HttpNotFound();
            }
            return View(trains);
        }

        // POST: /Trains/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Trains trains = db.Trains.Find(id);
            db.Trains.Remove(trains);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
        public ActionResult ShowCarriages(int id)
        {
            if (Session.IsNewSession)
                return RedirectToAction("Input");
            Trains train = db.Trains.Find(id);
            Tickets ttt = (Tickets)Session["ticket"];
            ttt.TrainID = id;
            Session["ticket"] = ttt;
            return View("ShowCarriages", train);
        }
        [Authorize]
        public ActionResult Input()
        {
            return View("Input");
        }

        [HttpPost, ActionName("Input")]
        [ValidateAntiForgeryToken]
        public ActionResult FindTrains(SearchPath ticket)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Error = false;
                Tickets ttt = Session.IsNewSession ? new Tickets() { PassengerID = int.Parse(Request.Cookies["passengerID"].Value) } : (Tickets)Session["ticket"];
                ttt.DateTrip = ticket.DateOfTrip;
                ttt.StartTrip = db.Stations.Where(x => x.Name == ticket.Start).Select(x => x.StationID).FirstOrDefault<int>();
                ttt.EndTrip = db.Stations.Where(x => x.Name == ticket.End).Select(x => x.StationID).FirstOrDefault<int>();
                Session["ticket"] = ttt;

                string tea = Request.Form["tea"];
                string bedlinen = Request.Form["bedlinen"];

                var sql1 = from route in db.Routes
                           where route.Stations.Name == ticket.Start
                           select route;

                var sql2 = from route in db.Routes
                           where route.Stations.Name == ticket.End
                           select route;

                List<Routes> query1 = sql1.ToList<Routes>();
                List<Routes> query2 = sql2.ToList<Routes>();
                List<Trains> trains = new List<Trains>();

                foreach (var item1 in query1)
                {
                    foreach (var item2 in query2)
                    {
                        if (item1.TrainID == item2.TrainID)
                        {
                            int sql3 = (from route in db.Routes
                                        where (route.TrainID == item1.TrainID && route.Stations.Name == ticket.Start)
                                        select route.Distance).FirstOrDefault<int>();

                            int sql4 = (from route in db.Routes
                                        where (route.TrainID == item1.TrainID && route.Stations.Name == ticket.End)
                                        select route.Distance).FirstOrDefault<int>();

                            if (sql3 < sql4)
                                trains.Add(db.Trains.Find(item1.TrainID));
                        }
                    }
                }

                if (trains.Count == 0)
                {
                    ViewBag.Error = true;
                    return View("Input");
                }

                return View("Search", trains);
            }
            ViewBag.Error = true;
            return View("Input");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
