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
        string connectionString = @"Data Source = LAPTOP-4DCIQUFG; Initial Catalog = HotelDatabase; Integrated Security=True";

        // GET: RoomReservation
        [HttpGet]
        public ActionResult ContactInformation()
        {
            User user = new User();
            var Countries = GetAllCountries();
            user.Countries = GetSelectListItems(Countries);

            return View(user);
        }

        [HttpPost]
        public ActionResult ContactInformation(User user)
        {

            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "Insert into [User] values(@First_Name,@Last_Name,@Street_Number,@City,@Province_State,@Country,@Postal_Code,@Phone_Number,@E_mail_Address)";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@First_Name", user.First_Name);
                sqlCmd.Parameters.AddWithValue("@Last_Name", user.Last_Name);
                sqlCmd.Parameters.AddWithValue("@Street_Number", user.Street_Number);
                sqlCmd.Parameters.AddWithValue("@City", user.City);
                sqlCmd.Parameters.AddWithValue("@Province_State", user.Province_State);
                sqlCmd.Parameters.AddWithValue("@Country", user.Country);
                sqlCmd.Parameters.AddWithValue("@Postal_Code", user.Postal_Code);
                sqlCmd.Parameters.AddWithValue("@Phone_Number", user.Phone_Number);
                sqlCmd.Parameters.AddWithValue("@E_mail_Address", user.E_mail_Address);

                sqlCmd.ExecuteNonQuery();
            }

            return RedirectToAction("ContactInformation");
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
        private IEnumerable<string> GetAllCountries()
        {
            return new List<string>
            {
                "Canada",
                "US",
            };
        }

        private IEnumerable<SelectListItem> GetSelectListItems(IEnumerable<string> elements)
        {
            // Create an empty list to hold result of the operation
            var selectList = new List<SelectListItem>();

            // For each string in the 'elements' variable, create a new SelectListItem object
            // that has both its Value and Text properties set to a particular value.
            // This will result in MVC rendering each item as:
            //     <option value="State Name">State Name</option>
            foreach (var element in elements)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element,
                    Text = element
                });
            }

            return selectList;
        }
    }
}