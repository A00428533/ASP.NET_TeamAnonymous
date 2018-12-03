using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;

namespace WebApplication5.Controllers
{
    public class TransactionController : Controller
    {
        string connectionString = @"Data Source = DEYUKONG-NB0\SQLEXPRESS; Initial Catalog = HotelDatabase; Integrated Security=True";
        // GET: Transaction
        
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
            ViewBag.roomNumber = 2;
            ViewBag.totalPrice = 2 * 100;
            return View();
        }

        [HttpPost]
        public JsonResult AddTrxn(String NameOnCard, String ExpDate)
        {
            //return Content(NameOnCard);
            HotelTrxnService.Transaction trxn = new HotelTrxnService.Transaction();
            trxn.nameOnCard = NameOnCard;
            trxn.expDate = ExpDate;
            return Json(trxn);
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

        public ActionResult ShowTrxn()
        {
            HotelTrxnService.TrxnWebServiceClient client = new HotelTrxnService.TrxnWebServiceClient();

            if (client == null)
            {
                return Content("java service unavaliable");
            }
            else
            {
               /* int Reservation_ID = 0;
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


                }*/

                /*HotelTrxnService.MySQLAccess dao = new HotelTrxnService.MySQLAccess();
                client.setDao(dao);
                HotelTrxnService.Transaction trxn = new HotelTrxnService.Transaction();
                String str = DateTime.Now.ToString("u");
                String tid = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").Substring(8, 16);
                trxn.id = tid;
                trxn.nameOnCard = "zeweiyan";
                trxn.cardCategory = 1;
                trxn.cardNumber = "5155555533252353";
                trxn.unitPrice = 100;
                trxn.quantity = No_of_rooms;
                trxn.expDate = "03/2031";
                trxn.userId = UserId;
                trxn.reservationId = Reservation_ID;*/
                //Boolean res = client.createTransaction(trxn);
                string res = client.getTransaction("DAB5D19C03828278");

                /*Response.Write(res);
                Response.Write(tid);
                Response.Write(str);*/
                //client.test();


                //Response.Write(client.getTransaction("12345"));

                //ViewBag.Message = res;
                ViewBag.trxn = JsonConvert.DeserializeObject(res);
                return View();
            }
        }
    }


}