﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Samoloty.Models;
using Samoloty.Security;

namespace Samoloty.Controllers
{
    public class FlightsController : Controller
    {
        private AircraftDBCtxt db = new AircraftDBCtxt();

        // GET: Flights
        [AllowAnonymous]
        public ActionResult Index()
        {
            var flights = db.Flights.Include(f => f.Aircraft);
            return View(flights.ToList());
        }

        // GET: Flights/Details/5
        [CustomAuthorize(Roles = "pilot")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight flight = db.Flights.Find(id);
            if (flight == null)
            {
                return HttpNotFound();
            }
            return View(flight);
        }

        // GET: Flights/Create
        [CustomAuthorize(Roles = "admin,superadmin")]
        public ActionResult Create()
        {
            ViewBag.AircraftID = new SelectList(db.Aircrafts, "Id", "Brand");
            return View();
        }

        // POST: Flights/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "admin,superadmin")]
        public ActionResult Create([Bind(Include = "Id,Date,dep,dest,AircraftID")] Flight flight)
        {
            if (ModelState.IsValid)
            {
                db.Flights.Add(flight);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AircraftID = new SelectList(db.Aircrafts, "Id", "Brand", flight.AircraftID);
            return View(flight);
        }

        // GET: Flights/Edit/5
        [CustomAuthorize(Roles = "pilot,admin,superadmin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight flight = db.Flights.Find(id);
            if (flight == null)
            {
                return HttpNotFound();
            }
            ViewBag.AircraftID = new SelectList(db.Aircrafts, "Id", "Brand", flight.AircraftID);
            return View(flight);
        }

        // POST: Flights/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "pilot,admin,superadmin")]
        public ActionResult Edit([Bind(Include = "Id,Date,dep,dest,AircraftID")] Flight flight)
        {
            if (ModelState.IsValid)
            {
                db.Entry(flight).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AircraftID = new SelectList(db.Aircrafts, "Id", "Brand", flight.AircraftID);
            return View(flight);
        }

        // GET: Flights/Delete/5
        [CustomAuthorize(Roles = "superadmin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight flight = db.Flights.Find(id);
            if (flight == null)
            {
                return HttpNotFound();
            }
            return View(flight);
        }

        // POST: Flights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "superadmin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Flight flight = db.Flights.Find(id);
            db.Flights.Remove(flight);
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
