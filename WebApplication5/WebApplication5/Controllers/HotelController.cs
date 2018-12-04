using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Net;
using System.Data;

namespace WebApplication5.Controllers
{
    public class HotelController : Controller
    {
        string connectionString = @"Data Source = LAPTOP-4DCIQUFG; Initial Catalog = HotelDatabase; Integrated Security=True";

        private Model1 db = new Model1();
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult showBookingHistory()
        {
            DataTable dtblproduct = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter SQLda = new SqlDataAdapter("SELECT * FROM [reservation_table]", sqlCon);
                SQLda.Fill(dtblproduct);
            }
            return View(dtblproduct);
        }

        [ActionName("Login")]
        [HttpPost]
        public ActionResult Login(RegisterUser user)
        {
            Session["firstName"] = user.UserName;
            var obj = DBManager.ValidateLogin(user.UserName, user.Password);
            if (obj != null)
            {
                Session["login"] = user.UserName;
                return Redirect("~/Hotel/ContactInformation");

            }
            else {
                ViewBag.Msg = "You have entered an incorrect Username or Password";
                ViewBag.Login = user.UserName;
                ViewBag.Login1 = Session["login"];
            }
            Session["login"] = null;
            return View("Login");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterUser user)
        {
            Session["EmailAddress"] = user.EmailAddress;
            Session["firstName"] = user.UserName;
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "Insert into [RegisterUser] values(@UserName,@Password,@EmailAddress)";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@UserName", user.UserName);
                sqlCmd.Parameters.AddWithValue("@Password", user.Password);
                sqlCmd.Parameters.AddWithValue("@EmailAddress", user.EmailAddress);

                sqlCmd.ExecuteNonQuery();

                Session["userName"] = user.UserName;
            }
            return RedirectToAction("ContactInformation");

        }

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
            Session["firstName"] = user.First_Name;
            Session["lastName"] = user.Last_Name;
            Session["Street_Number"] = user.Street_Number;
            Session["City"] = user.City;
            Session["Province_State"] = user.Province_State;
            Session["Country"] = user.Country;
            Session["Postal_Code"] = user.Postal_Code;
            Session["Phone_Number"] = user.Phone_Number;
            Session["EmailAddress"] = user.E_mail_Address;

            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                if (ModelState.IsValid)
                {
                    return RedirectToAction("Form", "Transaction");

                }
                else
                {
                    return Redirect("~/Hotel/NewView");
                }
                
            }
           

        }
        // GET: Hotel/Edit/5
        public ActionResult Show()
        {
            if (Session["login"] == null)
            {
                return Redirect("~/Hotel/Login");
            }
            return View("Register");
        }
        [HttpGet]
        public ActionResult NewView()
        {

            return View("NewView");
        }

        public ActionResult Logout()
        {
            Session["login"] = null;
            Session["userName"] = null;
            Session["EmailAddress"] = null;
            Session["firstName"] = null;
            Session.Abandon();
            return Redirect("~/Hotel/Login");

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
