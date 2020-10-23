using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DndServerBase.Models;

namespace DndServerBase.Controllers
{
    public class NpcController : Controller
    {
        // GET: Npc
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListNpcs()
        {
            return View();
        }
    }
}