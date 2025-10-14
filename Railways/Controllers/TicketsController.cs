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
    public class TicketsController : Controller
    {
        private RailwayTicketOfficeDBEntities1 db = new RailwayTicketOfficeDBEntities1();

        // GET: /Tickets/
        public ActionResult Index()
        {
            int pass = int.Parse(Request.Cookies["passengerID"].Value);
            var tickets = db.Tickets.Include(t => t.Carriages).Include(t => t.Passengers).Include(t => t.Stations).Include(t => t.Stations1).Include(t => t.Trains).Where(t => t.PassengerID == pass);
            return View(tickets.ToList());
        }

        // GET: /Tickets/Details/5
        public ActionResult Details(int carriageID, int seat)
        {
            
            Tickets tickets = (Tickets)Session["ticket"];            
            if (tickets == null)
            {
                return HttpNotFound();
            }
            tickets.CarriageID = carriageID;
            tickets.Seat = seat;

            int dist1 = (from r in db.Routes
                         where (r.StationID == tickets.StartTrip && r.TrainID == tickets.TrainID)
                         select r.Distance).First<int>();

            int dist2 = (from r in db.Routes
                         where (r.StationID == tickets.EndTrip && r.TrainID == tickets.TrainID)
                         select r.Distance).First<int>();

            int dist = dist2 - dist1;

            string tea = Request.Form["tea"];
            string bedlinen = Request.Form["bedlinen"];
            decimal price = 0;

            if (tea == "on")
                price += 3;

            if (bedlinen == "on")
                price += 12;

            var pass = db.Passengers.Find(tickets.PassengerID);
            var pricePerKm = db.Carriages.Find(carriageID).CarriageTypes.PricePerKm;

            if (db.Carriages.Find(carriageID).CarriageTypes.TypeName == "Плацкарта")
            {
                switch (pass.PassengerStatus)
                {
                    case "Студент":
                        {
                            price += (decimal)dist * pricePerKm * 0.5m;
                            price *= 1.2m;
                            price *= 1.015m;
                            price += 5;
                            break;
                        }

                    case "Дитина":
                        {
                            price += (decimal)dist * pricePerKm * 0.75m;
                            price *= 1.2m;
                            price *= 1.015m;
                            price += 5;
                            break;
                        }

                    case "Пільговик":
                        {
                            price += (decimal)dist * pricePerKm * 0.25m;
                            price *= 1.015m;
                            break;
                        }

                    default:
                        {
                            price += (decimal)dist * pricePerKm;
                            price *= 1.2m;
                            price *= 1.015m;
                            price += 5;
                            break;
                        }


                } 
            }
            else
            {
                price += (decimal)dist * pricePerKm;
                price *= 1.2m;
                price *= 1.015m;
                price += 5;
            }

            tickets.Price = price;
            Session["ticket"] = tickets;
            return View("Details",tickets);
        }

        [HttpPost]
        public ActionResult Details()
        {
            Tickets t = (Tickets)Session["ticket"];
            db.Tickets.Add(t);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: /Tickets/Create
        public ActionResult Create()
        {
            //ViewBag.CarriageID = new SelectList(db.Carriages, "CarriageID", "CarriageID");
            //ViewBag.PassengerID = new SelectList(db.Passengers, "PassengerID", "FirstName");
            //ViewBag.StartTrip = new SelectList(db.Stations, "StationID", "Name");
            //ViewBag.EndTrip = new SelectList(db.Stations, "StationID", "Name");
            //ViewBag.TrainID = new SelectList(db.Trains, "TrainID", "Number");
            
            return View();
        }

        // POST: /Tickets/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="TicketID,PassengerID,StartTrip,EndTrip,DateTrip,TrainID,CarriageID,Seat,Price")] Tickets tickets)
        {
            if (ModelState.IsValid)
            {
                db.Tickets.Add(tickets);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CarriageID = new SelectList(db.Carriages, "CarriageID", "CarriageID", tickets.CarriageID);
            ViewBag.PassengerID = new SelectList(db.Passengers, "PassengerID", "FirstName", tickets.PassengerID);
            ViewBag.StartTrip = new SelectList(db.Stations, "StationID", "Name", tickets.StartTrip);
            ViewBag.EndTrip = new SelectList(db.Stations, "StationID", "Name", tickets.EndTrip);
            ViewBag.TrainID = new SelectList(db.Trains, "TrainID", "Number", tickets.TrainID);
            return View(tickets);
        }

        // GET: /Tickets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tickets tickets = db.Tickets.Find(id);
            if (tickets == null)
            {
                return HttpNotFound();
            }
            ViewBag.CarriageID = new SelectList(db.Carriages, "CarriageID", "CarriageID", tickets.CarriageID);
            ViewBag.PassengerID = new SelectList(db.Passengers, "PassengerID", "FirstName", tickets.PassengerID);
            ViewBag.StartTrip = new SelectList(db.Stations, "StationID", "Name", tickets.StartTrip);
            ViewBag.EndTrip = new SelectList(db.Stations, "StationID", "Name", tickets.EndTrip);
            ViewBag.TrainID = new SelectList(db.Trains, "TrainID", "Number", tickets.TrainID);
            return View(tickets);
        }

        // POST: /Tickets/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="TicketID,PassengerID,StartTrip,EndTrip,DateTrip,TrainID,CarriageID,Seat,Price")] Tickets tickets)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tickets).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CarriageID = new SelectList(db.Carriages, "CarriageID", "CarriageID", tickets.CarriageID);
            ViewBag.PassengerID = new SelectList(db.Passengers, "PassengerID", "FirstName", tickets.PassengerID);
            ViewBag.StartTrip = new SelectList(db.Stations, "StationID", "Name", tickets.StartTrip);
            ViewBag.EndTrip = new SelectList(db.Stations, "StationID", "Name", tickets.EndTrip);
            ViewBag.TrainID = new SelectList(db.Trains, "TrainID", "Number", tickets.TrainID);
            return View(tickets);
        }

        // GET: /Tickets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tickets tickets = db.Tickets.Find(id);
            if (tickets == null)
            {
                return HttpNotFound();
            }
            return View(tickets);
        }

        // POST: /Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tickets tickets = db.Tickets.Find(id);
            db.Tickets.Remove(tickets);
            db.SaveChanges();
            return RedirectToAction("Index");
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
