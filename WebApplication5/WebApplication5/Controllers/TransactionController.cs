using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;
using Newtonsoft.Json.Linq;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class TransactionController : Controller
    {
        string connectionString = @"Data Source = LAPTOP-4DCIQUFG; Initial Catalog = HotelDatabase; Integrated Security=True";
        
        public ActionResult Index()
        {
            int Reservation_ID = 0;
            int No_of_rooms = 0;
            int UserId = 0;
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "select max(UserId) from [User];";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                

                using (SqlCommand command = new SqlCommand(query, sqlCon))
                {
                    
                    //Command 1
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // reader.Read iteration etc
                        
                        if (reader.Read() == true)
                        {
                            UserId = reader.GetInt32(0);
                        }
                    }

                } // command is disposed.

                

                string query2 = "select top(1)Reservation_ID,No_of_rooms from[reservation_table] order by Reservation_ID desc";
                
                using (SqlCommand command = new SqlCommand(query2, sqlCon))
                {

                    //Command 1
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read() == true)
                        {
                            Reservation_ID = reader.GetInt32(0);
                            No_of_rooms = reader.GetInt32(1);
                        }
                        // reader.Read iteration etc
                    }

                } // command is disposed.
 

            }
            return Content(Reservation_ID.ToString());
            //return Content("How are you?");
            //return View();
        }

        public ActionResult Form(String tid)
        {
            int Reservation_ID = 0;
            int No_of_rooms = 0;

            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
            string query = "Insert into [User] values(@First_Name,@Last_Name,@Street_Number,@City,@Province_State,@Country,@Postal_Code,@Phone_Number,@E_mail_Address)";
            SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
            sqlCmd.Parameters.AddWithValue("@First_Name", Session["firstName"]);
            sqlCmd.Parameters.AddWithValue("@Last_Name", Session["lastName"]);
            sqlCmd.Parameters.AddWithValue("@Street_Number", Session["Street_Number"]);
            sqlCmd.Parameters.AddWithValue("@City", Session["City"]);
            sqlCmd.Parameters.AddWithValue("@Province_State", Session["Province_State"]);
            sqlCmd.Parameters.AddWithValue("@Country", Session["Country"]);
            sqlCmd.Parameters.AddWithValue("@Postal_Code", Session["Postal_Code"]);
            sqlCmd.Parameters.AddWithValue("@Phone_Number", Session["Phone_Number"]);
            sqlCmd.Parameters.AddWithValue("@E_mail_Address", Session["EmailAddress"]);

            sqlCmd.ExecuteNonQuery();


        

                string query2 = "select top(1)Reservation_ID,No_of_rooms from[reservation_table] order by Reservation_ID desc";

                using (SqlCommand command = new SqlCommand(query2, sqlCon))
                {

                    //Command 1
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read() == true)
                        {
                            Reservation_ID = reader.GetInt32(0);
                            No_of_rooms = reader.GetInt32(1);
                        }
                        // reader.Read iteration etc
                    }

                } // command is disposed.


            }
            ViewBag.roomNumber = No_of_rooms;
            ViewBag.totalPrice = No_of_rooms * 100;
            return View();
        }

        [HttpPost]
        public JsonResult AddTrxn(string NameOnCard, string ExpDate, int CardCategory, string CardNumber)
        {
            //return Content(NameOnCard);
            int Reservation_ID = 0;
            int No_of_rooms = 0;
            int UserId = 0;
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "select max(UserId) from [User];";

                using (SqlCommand command = new SqlCommand(query, sqlCon))
                {

                    //Command 1
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // reader.Read iteration etc

                        if (reader.Read() == true)
                        {
                            UserId = reader.GetInt32(0);
                        }
                    }

                } // command is disposed.



                string query2 = "select top(1)Reservation_ID,No_of_rooms from[reservation_table] order by Reservation_ID desc";

                using (SqlCommand command = new SqlCommand(query2, sqlCon))
                {

                    //Command 1
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read() == true)
                        {
                            Reservation_ID = reader.GetInt32(0);
                            No_of_rooms = reader.GetInt32(1);
                        }
                        // reader.Read iteration etc
                    }

                } // command is disposed.


            }
            HotelTrxnService.Transaction trxn = new HotelTrxnService.Transaction();
            trxn.nameOnCard = NameOnCard;
            trxn.expDate = ExpDate;
            trxn.cardCategory = CardCategory;
            trxn.cardNumber = CardNumber;
            HotelTrxnService.MySQLAccess dao = new HotelTrxnService.MySQLAccess();
            HotelTrxnService.TrxnWebServiceClient client = new HotelTrxnService.TrxnWebServiceClient();
            client.setDao(dao);
            String str = DateTime.Now.ToString("u");
            String tid = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").Substring(8, 16);
            trxn.id = tid;
            trxn.unitPrice = 100;
            trxn.quantity = No_of_rooms;
            trxn.userId = UserId;
            trxn.reservationId = Reservation_ID;
            bool res = client.createTransaction(trxn);
            
            return Json(new { success = res, tid = tid });
        }

        public ActionResult GetLastestId()
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "select max(UserId) from [User];";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                SqlDataReader reader = sqlCmd.ExecuteReader();
                int UserId = 0;
                if (reader.Read() == true)
                {
                    UserId = reader.GetInt32(0);
                }
                return Content(UserId.ToString());

            }
        }

        public ActionResult ShowTrxn(string tid)
        {
            HotelTrxnService.TrxnWebServiceClient client = new HotelTrxnService.TrxnWebServiceClient();

            if (client == null)
            {
                return Content("java service unavaliable");
            }
            else
            {
               
                string res = client.getTransaction(tid);
               
                if (res.Length == 0 || res == "null")
                {
                    return Content("Get not find transaction_id:"+tid);

                }


              
                ViewBag.trxn = JsonConvert.DeserializeObject<Trxn>(res);
                
                return View();
            }
        }

        public JsonResult GetRes(int rid)
        {
            using (Model3 res = new Model3())
            {
                var details = res.reservation_table.Find(rid);
                int Reservation_ID = details.Reservation_ID;
                string checkindate = details.Check_in_date.ToString("yyyy/MM/dd");
                string checkoutdate = details.Check_out_date.ToString("yyyy/MM/dd");
                int No_of_rooms = details.No_of_rooms;
                int No_of_guests = details.No_of_guests;
                return Json(new {Reservation_ID=Reservation_ID, Check_in_date=checkindate, Check_out_date=checkoutdate, No_of_rooms=No_of_rooms, No_of_guests=No_of_guests}, JsonRequestBehavior.AllowGet);
                //return new JsonResult() { Data = details, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                
            }
        }

        public JsonResult GetUser(int uid)
        {
            using (Model3 user = new Model3())
            {
                var details = user.User.Find(uid);
                return new JsonResult() { Data = details, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
                

            
        }

        public ActionResult cancelTrxn() { 
             
            return View();
        }

        public JsonResult CancelTrxnAjax(string tid)
        {
            HotelTrxnService.MySQLAccess dao = new HotelTrxnService.MySQLAccess();
            HotelTrxnService.TrxnWebServiceClient client = new HotelTrxnService.TrxnWebServiceClient();
            client.setDao(dao);
            string trxn = client.getTransaction(tid);
            string msg = "";
            bool res = false;
            if (trxn == "null" || trxn.Length == 0)
            {
                msg = "transaction id: "+ tid + " doesn't exist";
            }
            else
            {
                res = client.removeTransaction(tid);
            }

            return Json(new { res = res, msg = msg });
        }


    }


}
