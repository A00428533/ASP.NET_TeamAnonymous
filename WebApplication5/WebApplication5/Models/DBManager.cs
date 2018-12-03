using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace WebApplication5.Models
{
    public static class DBManager
    {

        public static Boolean Validate(String login, String Password)
        {
            if (login == "admin" && Password == "admin")
                return true;
            else
                return false;

        }

        public static RegisterUser ValidateLogin(String Login, String Password)
        {
            string connectionString = @"Data Source = DEYUKONG-NB0\SQLEXPRESS; Initial Catalog = HotelDatabase; Integrated Security=True";
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                try
                {

                    sqlCon.Open();
                    string query = String.Format("Select UserId, UserName, Password from [RegisterUser] where UserName='{0}' and Password='{1}'", Login, Password);




                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.Read() == true)
                    {
                        RegisterUser user = new RegisterUser();
                        user.UserId = reader.GetInt32(0);
                        user.UserName = reader.GetString(1);
                        user.Password = reader.GetString(2);

                        return user;
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

    }
}