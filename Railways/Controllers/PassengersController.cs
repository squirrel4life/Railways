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
    public class PassengersController : Controller
    {
        private RailwayTicketOfficeDBEntities1 db = new RailwayTicketOfficeDBEntities1();

        // GET: /Passengers/
        public ActionResult Index()
        {
            return View(db.Passengers.ToList());
        }

        // GET: /Passengers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Passengers passengers = db.Passengers.Find(id);
            if (passengers == null)
            {
                return HttpNotFound();
            }
            return View(passengers);
        }

        // GET: /Passengers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Passengers/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="PassengerID,FirstName,LastName,Age,PassengerStatus")] Passengers passengers)
        {
            if (ModelState.IsValid)
            {
                db.Passengers.Add(passengers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(passengers);
        }

        // GET: /Passengers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Passengers passengers = db.Passengers.Find(id);
            if (passengers == null)
            {
                return HttpNotFound();
            }
            return View(passengers);
        }

        // POST: /Passengers/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="PassengerID,FirstName,LastName,Age,PassengerStatus")] Passengers passengers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(passengers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(passengers);
        }

        // GET: /Passengers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Passengers passengers = db.Passengers.Find(id);
            if (passengers == null)
            {
                return HttpNotFound();
            }
            return View(passengers);
        }

        // POST: /Passengers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Passengers passengers = db.Passengers.Find(id);
            db.Passengers.Remove(passengers);
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
