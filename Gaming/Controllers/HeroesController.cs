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
    public class HeroesController : Controller
    {
        private GamingContext db = new GamingContext();

        // GET: Heroes
        public ActionResult Index(string sortOrder, string searchString)
        {
            /*  var heroes = db.Heroes.Include(h => h.Account).Include(h => h.Equipment1).Include(h => h.Guild1);
                return View(heroes.ToList());*/
            ViewBag.NicknameSortParm = String.IsNullOrEmpty(sortOrder) ? "nickname_desc" : "";
            ViewBag.GuildSortParm = sortOrder == "Guild" ? "guild_desc" : "Guild";

            var heroes = db.Heroes.Include(h => h.Account).Include(h => h.Equipment1).Include(h => h.Guild1);

            // Search case
            if (!String.IsNullOrEmpty(searchString))
            {
                heroes = heroes.Where(s => s.Nickname.Contains(searchString)
                                    || s.Guild1.Name.Contains(searchString)
                                    || s.Equipment1.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "nickname_desc":
                    heroes = heroes.OrderByDescending(s => s.Nickname);
                    break;
                case "Guild":
                    heroes = heroes.OrderBy(s => s.Guild);
                    break;
                case "guild_desc":
                    heroes = heroes.OrderByDescending(s => s.Guild);
                    break;
                default:
                    heroes = heroes.OrderBy(s => s.Nickname);
                    break;
            }

            return View(heroes.ToList());
        }

        // GET: Heroes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hero hero = db.Heroes.Find(id);
            if (hero == null)
            {
                return HttpNotFound();
            }
            return View(hero);
        }

        // GET: Heroes/Create
        public ActionResult Create()
        {
            ViewBag.Owner_ID = new SelectList(db.Accounts, "Account_ID", "Username");
            ViewBag.Equipment = new SelectList(db.Equipments, "Item_ID", "Name");
            ViewBag.Guild = new SelectList(db.Guilds, "Guild_ID", "Name");
            return View();
        }

        // POST: Heroes/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Hero_ID,Owner_ID,Nickname,Level,Class,Equipment,Guild")] Hero hero)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Heroes.Add(hero);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                Response.Write("<script>alert('Creating unsuccessful. Try to change values to unique.');</script>");
            }

            ViewBag.Owner_ID = new SelectList(db.Accounts, "Account_ID", "Username", hero.Owner_ID);
            ViewBag.Equipment = new SelectList(db.Equipments, "Item_ID", "Name", hero.Equipment);
            ViewBag.Guild = new SelectList(db.Guilds, "Guild_ID", "Name", hero.Guild);
            return View(hero);
        }

        // GET: Heroes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hero hero = db.Heroes.Find(id);
            if (hero == null)
            {
                return HttpNotFound();
            }
            ViewBag.Owner_ID = new SelectList(db.Accounts, "Account_ID", "Username", hero.Owner_ID);
            ViewBag.Equipment = new SelectList(db.Equipments, "Item_ID", "Name", hero.Equipment);
            ViewBag.Guild = new SelectList(db.Guilds, "Guild_ID", "Name", hero.Guild);
            return View(hero);
        }

        // POST: Heroes/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Hero_ID,Owner_ID,Nickname,Level,Class,Equipment,Guild")] Hero hero)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(hero).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                Response.Write("<script>alert('Creating unsuccessful. Try to change values to unique.');</script>");
            }
            ViewBag.Owner_ID = new SelectList(db.Accounts, "Account_ID", "Username", hero.Owner_ID);
            ViewBag.Equipment = new SelectList(db.Equipments, "Item_ID", "Name", hero.Equipment);
            ViewBag.Guild = new SelectList(db.Guilds, "Guild_ID", "Name", hero.Guild);
            return View(hero);
        }

        // GET: Heroes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hero hero = db.Heroes.Find(id);
            if (hero == null)
            {
                return HttpNotFound();
            }
            return View(hero);
        }

        // POST: Heroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Hero hero = db.Heroes.Find(id);
            db.Heroes.Remove(hero);
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
