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
    public class raidsOnTheGoController : Controller
    {
        private Entities db = new Entities();

        // GET: raidsOnTheGo
        public ActionResult Index()
        {
            var raids = db.raidsOnTheGo();
            return View(raids.ToList());
        }
    }
}