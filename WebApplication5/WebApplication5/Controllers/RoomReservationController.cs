using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models;
using System.Data.SqlClient;

namespace RoomReservation.Controllers
{

    public class RoomReservationController : Controller
    {
        public ActionResult front() {
            return View();
        }
        
        [HttpGet]
        public ActionResult reserve()
        {
            reservation_table res = new reservation_table();
            return View();
        }

        [HttpPost]
        public ActionResult reserve(reservation_table res)
        {
            if(!ModelState.IsValid)
            {
                return View(res);
            }
            using (Model3 dbModel1 = new Model3())
            {
                dbModel1.reservation_table.Add(res);
                dbModel1.SaveChanges();
            }
            ModelState.Clear();
            ViewBag.SuccessMessage = "Data Inserted";
            //return View("Reserve");
            return RedirectToAction("ContactInformation");
        }
       
    }
}