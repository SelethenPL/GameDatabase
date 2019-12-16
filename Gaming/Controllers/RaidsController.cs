using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Gaming.Models;

namespace Gaming.Controllers
{
    public class RaidsController : Controller
    {
        private GamingContext db = new GamingContext();

        // GET: Raids
        public ActionResult Index()
        {
            var raids = db.Raids.Include(r => r.Boss1).Include(r => r.Event);
            return View(raids.ToList());
        }

        // GET: Raids/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Raid raid = db.Raids.Find(id);
            if (raid == null)
            {
                return HttpNotFound();
            }
            return View(raid);
        }

        // GET: Raids/Create
        public ActionResult Create()
        {
            ViewBag.Boss = new SelectList(db.Bosses, "Boss_ID", "Name");
            ViewBag.Event_ID = new SelectList(db.Events, "Event_ID", "Name");
            return View();
        }

        // POST: Raids/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Raid_ID,Name,Level_required,Boss,Event_ID")] Raid raid)
        {
            if (ModelState.IsValid)
            {
                db.Raids.Add(raid);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Boss = new SelectList(db.Bosses, "Boss_ID", "Name", raid.Boss);
            ViewBag.Event_ID = new SelectList(db.Events, "Event_ID", "Name", raid.Event_ID);
            return View(raid);
        }

        // GET: Raids/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Raid raid = db.Raids.Find(id);
            if (raid == null)
            {
                return HttpNotFound();
            }
            ViewBag.Boss = new SelectList(db.Bosses, "Boss_ID", "Name", raid.Boss);
            ViewBag.Event_ID = new SelectList(db.Events, "Event_ID", "Name", raid.Event_ID);
            return View(raid);
        }

        // POST: Raids/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Raid_ID,Name,Level_required,Boss,Event_ID")] Raid raid)
        {
            if (ModelState.IsValid)
            {
                db.Entry(raid).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Boss = new SelectList(db.Bosses, "Boss_ID", "Name", raid.Boss);
            ViewBag.Event_ID = new SelectList(db.Events, "Event_ID", "Name", raid.Event_ID);
            return View(raid);
        }

        // GET: Raids/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Raid raid = db.Raids.Find(id);
            if (raid == null)
            {
                return HttpNotFound();
            }
            return View(raid);
        }

        // POST: Raids/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Raid raid = db.Raids.Find(id);
            db.Raids.Remove(raid);
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
