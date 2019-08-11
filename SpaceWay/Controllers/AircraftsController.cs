﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SpaceWay.Context;
using SpaceWay.Models;

namespace SpaceWay.Controllers
{
    public class AircraftsController : Controller
    {
        private SpaceWayDbContext db = new SpaceWayDbContext();

        // GET: Aircrafts
        public ActionResult Index(int? levelToFilter)
        {
    
            List<Aircraft> airs = new List<Aircraft>();
            if (levelToFilter == null || levelToFilter == -1)   //if just opened the page
            {
                return View(db.Aircrafts.ToList());
            }
            if (levelToFilter == 0)                        //if bad input (ε for example)
            {
                ModelState.AddModelError(string.Empty, "Please enter a valid input");
                return View(new List<Aircraft>());
            }

            airs = db.Aircrafts.ToList().Where(a => a.Level == levelToFilter).ToList();
            if (!airs.Any())                               //no match         
            {
                return View(new List<Aircraft>());
            }
            return View(airs);                              //filter
        }

        // POST: Aircrafts
        [HttpPost]
        public ActionResult Search(string lvl)
        {
            int n;
            bool isNumeric = int.TryParse(lvl, out n);
            if (string.IsNullOrEmpty(lvl))
            {
                return RedirectToAction("Index", new { @levelToFilter = -1 });
            }

            if (!isNumeric)
            { 
                return RedirectToAction("Index", new { @levelToFilter = 0 });
            }

            int level = Convert.ToInt16(lvl);
            return RedirectToAction("Index", new { @levelToFilter = level });
        }


        // GET: Aircrafts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }
            Aircraft aircraft = db.Aircrafts.Find(id);
            if (aircraft == null)
            {
                return View("Error");
            }
            return View(aircraft);
        }

        // GET: Aircrafts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Aircrafts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AircraftID,Level,Seats")] Aircraft aircraft)
        {
            if (ModelState.IsValid)
            {
                db.Aircrafts.Add(aircraft);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aircraft);
        }

        // GET: Aircrafts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }
            Aircraft aircraft = db.Aircrafts.Find(id);
            if (aircraft == null)
            {
                return View("Error");
            }
            return View(aircraft);
        }

        // POST: Aircrafts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AircraftID,Level,Seats")] Aircraft aircraft)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aircraft).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aircraft);
        }

        // GET: Aircrafts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }
            Aircraft aircraft = db.Aircrafts.Find(id);
            if (aircraft == null)
            {
                return View("Error");
            }
            return View(aircraft);
        }

        // POST: Aircrafts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Aircraft aircraft = db.Aircrafts.Find(id);
            db.Aircrafts.Remove(aircraft);
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


