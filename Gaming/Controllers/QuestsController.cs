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
    public class QuestsController : Controller
    {
        private GamingContext db = new GamingContext();

        // GET: Quests
        public ActionResult Index()
        {
            var quests = db.Quests.Include(q => q.Equipment).Include(q => q.Event).Include(q => q.NPC);
            return View(quests.ToList());
        }

        // GET: Quests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quest quest = db.Quests.Find(id);
            if (quest == null)
            {
                return HttpNotFound();
            }
            return View(quest);
        }

        // GET: Quests/Create
        public ActionResult Create()
        {
            ViewBag.Reward_eq = new SelectList(db.Equipments, "Item_ID", "Name");
            ViewBag.Event_ID = new SelectList(db.Events, "Event_ID", "Name");
            ViewBag.NPC_giving = new SelectList(db.NPCs, "NPC_ID", "Name");
            return View();
        }

        // POST: Quests/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Quest_ID,Name,Level_required,Reward_eq,NPC_giving,Event_ID")] Quest quest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Quests.Add(quest);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                Response.Write("<script>alert('Creating unsuccessful. Try to change values to unique.');</script>");
            }

            ViewBag.Reward_eq = new SelectList(db.Equipments, "Item_ID", "Name", quest.Reward_eq);
            ViewBag.Event_ID = new SelectList(db.Events, "Event_ID", "Name", quest.Event_ID);
            ViewBag.NPC_giving = new SelectList(db.NPCs, "NPC_ID", "Name", quest.NPC_giving);
            return View(quest);
        }

        // GET: Quests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quest quest = db.Quests.Find(id);
            if (quest == null)
            {
                return HttpNotFound();
            }
            ViewBag.Reward_eq = new SelectList(db.Equipments, "Item_ID", "Name", quest.Reward_eq);
            ViewBag.Event_ID = new SelectList(db.Events, "Event_ID", "Name", quest.Event_ID);
            ViewBag.NPC_giving = new SelectList(db.NPCs, "NPC_ID", "Name", quest.NPC_giving);
            return View(quest);
        }

        // POST: Quests/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Quest_ID,Name,Level_required,Reward_eq,NPC_giving,Event_ID")] Quest quest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(quest).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                Response.Write("<script>alert('Creating unsuccessful. Try to change values to unique.');</script>");
            }
            ViewBag.Reward_eq = new SelectList(db.Equipments, "Item_ID", "Name", quest.Reward_eq);
            ViewBag.Event_ID = new SelectList(db.Events, "Event_ID", "Name", quest.Event_ID);
            ViewBag.NPC_giving = new SelectList(db.NPCs, "NPC_ID", "Name", quest.NPC_giving);
            return View(quest);
        }

        // GET: Quests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quest quest = db.Quests.Find(id);
            if (quest == null)
            {
                return HttpNotFound();
            }
            return View(quest);
        }

        // POST: Quests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Quest quest = db.Quests.Find(id);
            db.Quests.Remove(quest);
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
