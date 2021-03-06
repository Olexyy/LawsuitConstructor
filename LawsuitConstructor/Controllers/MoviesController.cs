﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.Models;

namespace MVC.Controllers
{
    public class MoviesController : Controller
    {
        private MovieDBContext db = new MovieDBContext();

        public static int ID = 0;
        //
        // GET: /Movies/

        public ActionResult Index()
        {
            try
            {
                return View(db.Movies.ToList());
            }
            catch (Exception e)
            {
                return View(e.ToString());
            }
        }

        //
        // GET: /Movies/Details/5

        public ActionResult Details(int id = 0)
        {
            MovieDB moviedb = db.Movies.Find(id);
            if (moviedb == null)
            {
                return HttpNotFound();
            }
            return View(moviedb);
        }

        //
        // GET: /Movies/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Movies/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MovieDB moviedb)
        {
            if (ModelState.IsValid)
            {
                db.Movies.Add(moviedb);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(moviedb);
        }

        //
        // GET: /Movies/Edit/5

        public ActionResult Edit(int id = 0)
        {
            MovieDB moviedb = db.Movies.Find(id);
            if (moviedb == null)
            {
                return HttpNotFound();
            }
            return View(moviedb);
        }

        //
        // POST: /Movies/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MovieDB moviedb)
        {
            if (ModelState.IsValid)
            {
                db.Entry(moviedb).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(moviedb);
        }

        //
        // GET: /Movies/Delete/5

        public ActionResult Delete(int id = 0)
        {
            MovieDB moviedb = db.Movies.Find(id);
            if (moviedb == null)
            {
                return HttpNotFound();
            }
            return View(moviedb);
        }

        // GET: /Movies/History/5

        public ActionResult History(int id = 0)
        {
            MoviesController.ID = id;
            return RedirectToAction("Show","Info");
        }

        //
        // POST: /Movies/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MovieDB moviedb = db.Movies.Find(id);
            db.Movies.Remove(moviedb);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}