using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Samoloty.Models;
using Samoloty.Security;

namespace Samoloty.Controllers
{
    public class Aircrafts2Controller : Controller
    {
        private AircraftDBCtxt db = new AircraftDBCtxt();

        // GET: Aircrafts2
        //public ActionResult Index()
        //{
        //    return View(db.Aircrafts.ToList());
        //}

        // GET: Aircrafts2/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aircraft aircraft = db.Aircrafts.Find(id);
            if (aircraft == null)
            {
                return HttpNotFound();
            }
            return View(aircraft);
        }

        // GET: Aircrafts2/Create
        [CustomAuthorize(Roles = "admin,superadmin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Aircrafts2/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "admin,superadmin")]
        public ActionResult Create([Bind(Include = "Id,Brand,Model,Price,PaxCapacity,Bought")] Aircraft aircraft)
        {
            if (ModelState.IsValid)
            {
                db.Aircrafts.Add(aircraft);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aircraft);
        }

        // GET: Aircrafts2/Edit/5
        [CustomAuthorize(Roles = "admin,superadmin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aircraft aircraft = db.Aircrafts.Find(id);
            if (aircraft == null)
            {
                return HttpNotFound();
            }
            return View(aircraft);
        }

        // POST: Aircrafts2/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "pilot,admin,superadmin")]
        public ActionResult Edit([Bind(Include = "Id,Brand,Model,Price,PaxCapacity,Bought")] Aircraft aircraft)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aircraft).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aircraft);
        }

        // GET: Aircrafts2/Delete/5
        [CustomAuthorize(Roles = "superadmin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aircraft aircraft = db.Aircrafts.Find(id);
            if (aircraft == null)
            {
                return HttpNotFound();
            }
            return View(aircraft);
        }

        // POST: Aircrafts2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "superadmin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Aircraft aircraft = db.Aircrafts.Find(id);
            db.Aircrafts.Remove(aircraft);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //[HttpPost]
        //public ActionResult Index(SzukajSamolotu Model)
        //{
        //    var aircrafts = from i in db.Aircrafts
        //               select i;
        //    if (Model != null)
        //    {
        //        aircrafts = from i in db.Aircrafts
        //               where i.Model.Equals(Model.ModelZnajdz)
        //               && i.Brand.Equals(Model.BrandZnajdz)
        //               select i;
        //    }

        //    return View(aircrafts.ToList());
        //}

        public ActionResult Index(string sortowanie, SzukajSamolotu Model, int? page)
        {
            ViewBag.SortedBy = sortowanie;
            ViewBag.SortByModel = sortowanie == null ? "Model_Malejaco" : "";
            ViewBag.SortByPrice = sortowanie == "Price_Malejaco" ? "Price_Rosnaco" : "Price_Malejaco";

            var aircrafts = from i in db.Aircrafts
                            select i;
            if (ModelState.IsValid)
            {
                if (Model.BrandZnajdz != null && Model.ModelZnajdz != null)
                {
                    aircrafts = from i in db.Aircrafts
                                where i.Model.Equals(Model.ModelZnajdz)
                           && i.Brand.Equals(Model.BrandZnajdz)
                           select i;
                }
                else if (Model.BrandZnajdz != null)
                {
                    aircrafts = from i in db.Aircrafts
                                where i.Brand.Equals(Model.BrandZnajdz)
                           select i;
                }
                else if (Model.ModelZnajdz != null)
                {
                    aircrafts = from i in db.Aircrafts
                                where i.Model.Equals(Model.ModelZnajdz)
                           select i;
                }
                ViewBag.BrandZnajdz = Model.BrandZnajdz;
                ViewBag.ModelZnajdz = Model.ModelZnajdz;
            }
            switch (sortowanie)
            {
                case "Model_Malejaco":
                    aircrafts = aircrafts.OrderByDescending(s => s.Model);
                    break;
                case "Price_Malejaco":
                    aircrafts = aircrafts.OrderByDescending(s => s.Price);
                    break;
                case "Price_Rosnaco":
                    aircrafts = aircrafts.OrderBy(s => s.Price);
                    break;
                default:
                    aircrafts = aircrafts.OrderBy(s => s.Model);
                    break;
            }


            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(aircrafts.ToPagedList(pageNumber, pageSize));
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
