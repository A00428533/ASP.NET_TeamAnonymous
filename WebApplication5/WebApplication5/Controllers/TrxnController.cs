using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication5.Controllers
{
    public class TrxnController : Controller
    {
        // GET: Trxn
        public ActionResult Index()
        {
            return Content("How are you?");
            return View();
        }
    }
}