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
    public class GuildsController : Controller
    {
        private GamingContext db = new GamingContext();
        
        // GET: Guilds
        public ActionResult Index()
        {
            var guilds = db.Guilds.Include(g => g.Place);
            return View(guilds.ToList());
        }

        // GET: Guilds/Details/5
        public ActionResult Details(int? id, string levels)
        {
            if (!String.IsNullOrEmpty(levels))
            {
                int x;
                if (!int.TryParse(levels, out x))
                {
                    Response.Write("<script>alert('Can't parse Levels.');</script>");
                }
                else
                {
                    db.levelUpGuildMembers(id.ToString(), x);
                    Response.Write("<script>alert('Added levels to guild members.');</script>");
                };
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guild guild = db.Guilds.Find(id);
            if (guild == null)
            {
                return HttpNotFound();
            }
            return View(guild);
        }

        // GET: Guilds/Create
        public ActionResult Create()
        {
            ViewBag.GuildHouse = new SelectList(db.Places, "Place_ID", "Name");
            return View();
        }

        // POST: Guilds/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Guild_ID,GuildLevel,Name,GuildHouse")] Guild guild)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Guilds.Add(guild);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                Response.Write("<script>alert('Creating unsuccessful. Try to change values to unique.');</script>");
            }

            ViewBag.GuildHouse = new SelectList(db.Places, "Place_ID", "Name", guild.GuildHouse);
            return View(guild);
        }

        // GET: Guilds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guild guild = db.Guilds.Find(id);
            if (guild == null)
            {
                return HttpNotFound();
            }
            ViewBag.GuildHouse = new SelectList(db.Places, "Place_ID", "Name", guild.GuildHouse);
            return View(guild);
        }

        // POST: Guilds/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Guild_ID,GuildLevel,Name,GuildHouse")] Guild guild)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(guild).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                Response.Write("<script>alert('Creating unsuccessful. Try to change values to unique.');</script>");
            }
            ViewBag.GuildHouse = new SelectList(db.Places, "Place_ID", "Name", guild.GuildHouse);
            return View(guild);
        }

        // GET: Guilds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guild guild = db.Guilds.Find(id);
            if (guild == null)
            {
                return HttpNotFound();
            }
            return View(guild);
        }

        // POST: Guilds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Guild guild = db.Guilds.Find(id);
            db.Guilds.Remove(guild);
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
