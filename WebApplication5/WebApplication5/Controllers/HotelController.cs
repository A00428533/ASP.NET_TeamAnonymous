using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models;
using System.Data.SqlClient;

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
        public ActionResult Login(User user)
        {

            var obj = DBManager.ValidateLogin(user.txtLogin, user.txtPassword);
            if (obj != null)
            {
                Session["login"] = user.txtLogin;
                Session["IsAdmin"] = obj.IsAdmin;
                return Redirect("~/Hotel/ContactInformation");

            }
            else {
                ViewBag.Msg = "You have entered an incorrect Username or Password";
                ViewBag.Login = user.txtLogin;
            }

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
            
            return RedirectToAction("Login");

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

        public ActionResult Logout()
        {
            Session["login"] = null;
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
