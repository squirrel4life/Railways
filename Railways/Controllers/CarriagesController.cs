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
    public class CarriagesController : Controller
    {
        private RailwayTicketOfficeDBEntities1 db = new RailwayTicketOfficeDBEntities1();

        // GET: /Carriages/
        public ActionResult Index()
        {
            var carriages = db.Carriages.Include(c => c.CarriageTypes).Include(c => c.Trains);
            return View(carriages.ToList());
        }

        // GET: /Carriages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carriages carriages = db.Carriages.Find(id);
            if (carriages == null)
            {
                return HttpNotFound();
            }
            return View(carriages);
        }

        // GET: /Carriages/Create
        public ActionResult Create()
        {
            ViewBag.CarriageTypeID = new SelectList(db.CarriageTypes, "CarriageTypeID", "TypeName");
            ViewBag.TrainID = new SelectList(db.Trains, "TrainID", "Number");
            return View();
        }

        // POST: /Carriages/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="CarriageID,Number,CarriageTypeID,TrainID")] Carriages carriages)
        {
            if (ModelState.IsValid)
            {
                db.Carriages.Add(carriages);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CarriageTypeID = new SelectList(db.CarriageTypes, "CarriageTypeID", "TypeName", carriages.CarriageTypeID);
            ViewBag.TrainID = new SelectList(db.Trains, "TrainID", "Number", carriages.TrainID);
            return View(carriages);
        }

        // GET: /Carriages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carriages carriages = db.Carriages.Find(id);
            if (carriages == null)
            {
                return HttpNotFound();
            }
            ViewBag.CarriageTypeID = new SelectList(db.CarriageTypes, "CarriageTypeID", "TypeName", carriages.CarriageTypeID);
            ViewBag.TrainID = new SelectList(db.Trains, "TrainID", "Number", carriages.TrainID);
            return View(carriages);
        }

        // POST: /Carriages/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="CarriageID,Number,CarriageTypeID,TrainID")] Carriages carriages)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carriages).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CarriageTypeID = new SelectList(db.CarriageTypes, "CarriageTypeID", "TypeName", carriages.CarriageTypeID);
            ViewBag.TrainID = new SelectList(db.Trains, "TrainID", "Number", carriages.TrainID);
            return View(carriages);
        }

        // GET: /Carriages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carriages carriages = db.Carriages.Find(id);
            if (carriages == null)
            {
                return HttpNotFound();
            }
            return View(carriages);
        }

        // POST: /Carriages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Carriages carriages = db.Carriages.Find(id);
            db.Carriages.Remove(carriages);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ShowSeats(int id)
        {
            Carriages carr = db.Carriages.Find(id);

            Tickets ttt = (Tickets)Session["ticket"];

            DateTime dt = ttt.DateTrip;

            string st1 = db.Stations.Find(ttt.StartTrip).Name;
            string st2 = db.Stations.Find(ttt.EndTrip).Name;

            int dist1 = (from route in db.Routes
                         where (route.Stations.Name == st1 && route.TrainID == carr.Trains.TrainID)
                         select route.Distance).FirstOrDefault<int>();

            int dist2 = (from route in db.Routes
                         where (route.Stations.Name == st2 && route.TrainID == carr.Trains.TrainID)
                         select route.Distance).FirstOrDefault<int>();

            List<int> before = (from route in db.Routes
                                where (route.TrainID == carr.Trains.TrainID && route.Distance <= dist1)
                                select route.StationID).ToList<int>();

            List<int> after = (from route in db.Routes
                               where (route.TrainID == carr.Trains.TrainID && route.Distance > dist1)
                               select route.StationID).ToList<int>();

            List<int> between = (from route in db.Routes
                                 where (route.TrainID == carr.Trains.TrainID && route.Distance >= dist1 && route.Distance <= dist2)
                                 select route.StationID).ToList<int>();

            List<Tickets> ticks = (from tick in db.Tickets
                                   where (tick.CarriageID == id && tick.DateTrip == dt)
                                   select tick).ToList<Tickets>();

            List<int> bookedSeats = new List<int>();

            foreach (var t in ticks)
            {
                if (between.Contains(t.StartTrip) || (before.Contains(t.StartTrip) && after.Contains(t.EndTrip)))
                {
                    bookedSeats.Add(t.Seat);
                }
            }

            Session["BookedSeats"] = bookedSeats;

            return PartialView("ShowSeats", carr);
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
