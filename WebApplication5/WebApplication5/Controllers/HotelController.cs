using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Net;

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
        [ActionName("Login")]
        [HttpPost]
        public ActionResult Login(RegisterUser user)
        {

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
        [ValidateAntiForgeryToken]
        public ActionResult ContactInformation(User user)
        {

            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                if (ModelState.IsValid)
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
                else
                {
                    return Redirect("~/Hotel/NewView");
                }
                
            }
            return RedirectToAction("ContactInformation");

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
