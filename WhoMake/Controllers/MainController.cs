using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using DataBase;
using System.Data.Linq;
using System.Linq;
using System.Configuration;

namespace WhoMake.Controllers
{
    public class MainController : Controller
    {
  
        DataClassesDataContext context = new DataClassesDataContext(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

        static public string Session_Id;
        static public string UserName;
        static public bool CookieAuth;
        string DbConnect = @"Data Source=localhost;
                            Initial Catalog=whomake_database_on;
                            Integrated Security=False;User ID=sa;Password=90963555aSd;";

        protected override void Dispose(bool disposing)
        {
           // base.Dispose();
            context.Dispose();
        }

        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
         
            return View();
        }

        public ActionResult ServicesList(string id)
        {
                 var services = context.services.Where(x =>x.services_id_category == Convert.ToInt32(id)).ToList();

            return PartialView(services);
        }

        public ActionResult CategoriesList()
        {
            var categories = context.category.ToList();
            return PartialView(categories);
        }

        public ActionResult Head()
        {
           

            return View("Head");
        }

        public ActionResult Footer()
        {

            return PartialView();
        }

        public ActionResult Performers()
        {

            return View();
        }

        public ActionResult Tasks()
        {

            return View();
        }

        public ActionResult Registration()
        {

            return View();
        }

        public ActionResult CreateTask()
        {

            return View();
        }

        public EmptyResult CheckCookieAuth()
        {
            string[] splitStrings = new string[2];
            bool SuccessCookieAuth = false;
            string user = "", regdate = "", money = "", name = "", secname = "";
            if (HttpContext.Request.Cookies["JustASimpleCookieOmnOmnOmn"] != null)
            {
                splitStrings = HttpContext.Request.Cookies["JustASimpleCookieOmnOmnOmn"].Value.ToString().Split(new string[] { "|USSRisComeBack!|" }, StringSplitOptions.None);

                List<string[]> lst = new List<string[]>();

                using (SqlConnection conn = new SqlConnection(DbConnect))
                {

                    conn.Open();
                    SqlCommand cmdSELECT = conn.CreateCommand();

                    cmdSELECT.CommandText = String.Format(@"SELECT 
    [users_email]
   ,[users_pass]
   ,[users_regdate]
   ,[users_money]
   ,[users_name]
   ,[users_secname] FROM [whomake_database_on].[dbo].[users]");

                    using (SqlDataReader ReadData = cmdSELECT.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (ReadData.Read())
                        {
                            string[] tempString = new string[6];
                            tempString[0] = ReadData.GetValue(0).ToString().Trim();
                            tempString[1] = ReadData.GetValue(1).ToString().Trim();
                            tempString[2] = ReadData.GetValue(2).ToString().Trim();
                            tempString[3] = ReadData.GetValue(3).ToString().Trim();
                            tempString[4] = ReadData.GetValue(4).ToString().Trim();
                            tempString[5] = ReadData.GetValue(5).ToString().Trim();
                            lst.Add(tempString);
                        }
                    }

                }
                foreach (var itemLst in lst)
                {
                    

                    if (VerifyHashedPassword(splitStrings[0], itemLst[0]))
                    {
                        //if (VerifyHashedString(itemLst[1], splitStrings[1]))
                       // {
                            SuccessCookieAuth = true;
                            user = itemLst[0];
                            regdate = itemLst[2];
                            money = itemLst[3];
                            name = itemLst[4];
                            secname = itemLst[5];
                        // }
                    }
                }

                if (SuccessCookieAuth)
                {
                    CookieAuth = true;
                    Session["CookieOmnOmn"] = user;
                    Session["users_regdate"] = regdate;
                    Session["users_money"] = money;
                    Session["users_name"] = name;
                    Session["users_secname"] = secname;
    }
            }
            
            return new EmptyResult();
        }

        public ActionResult Logout()
        {
            Session["UserName"] = null;
            Session["CookieOmnOmn"] = null;

            if (Request.Cookies["JustASimpleCookieOmnOmnOmn"] != null)
            {
                var cookie = new HttpCookie("JustASimpleCookieOmnOmnOmn")
                {
                    Expires = DateTime.Now.AddDays(-1d)
                };
                Response.Cookies.Add(cookie);
            }

            return PartialView();
        }

        public ActionResult LoginAnswer(string username, string password, string RememberMe)
        {
            
            string tempEmail = "";
            string tempPassword = "";
            string  regdate = "", money = "", name = "", secname = "";
            ViewBag.AnswerReg = "";
            bool NotExistUser = false;
            bool CheckPassword = false;
            using (SqlConnection conn = new SqlConnection(DbConnect))
            {

                conn.Open();
                SqlCommand cmdSELECT = conn.CreateCommand();

                cmdSELECT.CommandText = String.Format(@"SELECT  
    [users_email]
   ,[users_pass]
   ,[users_regdate]
   ,[users_money]
   ,[users_name]
   ,[users_secname] FROM [whomake_database_on].[dbo].[users] WHERE [users_email] = '{0}'", username);

                using (SqlDataReader ReadData = cmdSELECT.ExecuteReader(CommandBehavior.CloseConnection))
                { 
                    if (ReadData.HasRows)
                    {
                        while (ReadData.Read())
                        {
                            tempEmail = ReadData.GetValue(0).ToString().Trim();
                            tempPassword = ReadData.GetValue(1).ToString().Trim();
                            regdate = ReadData.GetValue(2).ToString().Trim();
                            money = ReadData.GetValue(3).ToString().Trim();
                            name = ReadData.GetValue(4).ToString().Trim();
                            secname = ReadData.GetValue(5).ToString().Trim();
                        }
                    }
                    else
                    {
                        NotExistUser = true;
                    }
                }

            }

            if (VerifyHashedPassword(tempPassword, password))
                CheckPassword = true;
            else CheckPassword = false;

            if (!NotExistUser)
            {
                if (CheckPassword)
                {
                    ViewBag.AnswerType = "success";
                    ViewBag.AnswerLogin = "";

                    Session["UserName"] = username;
                    Session["users_regdate"] = regdate;
                    Session["users_money"] = money;
                    Session["users_name"] = name;
                    Session["users_secname"] = secname;

                }
                else
                {
                    ViewBag.AnswerType = "error";
                    ViewBag.AnswerLogin = "Неверный логин или пароль, или пользователь не существует!";
                }
            }
            else
            {
                ViewBag.AnswerType = "error";
                ViewBag.AnswerLogin = "Неверный логин или пароль, или пользователь не существует!";
            }

            if(RememberMe == "on")
            {
                HttpContext.Response.Cookies["JustASimpleCookieOmnOmnOmn"].Value = HashPassword(username) + "|USSRisComeBack!|" + HashPassword(password);
                HttpContext.Response.Cookies["JustASimpleCookieOmnOmnOmn"].Expires = DateTime.Now.AddDays(30);
            }
            return PartialView();
        }

            public ActionResult Login()
        {
           
            return View();
        }

        public ActionResult RegisAnswer(string Name, string SecName, string Email, string password)
        {
            Thread.Sleep(10000);
             ViewBag.AnswerReg = "";
            bool flag = false;
            using (SqlConnection conn = new SqlConnection(DbConnect))
            {

                conn.Open();
                SqlCommand cmdSELECT = conn.CreateCommand();
                SqlCommand cmdINSERT = conn.CreateCommand();

                cmdSELECT.CommandText = String.Format(@"SELECT * FROM [whomake_database_on].[dbo].[users] WHERE users_email = '{0}'", Email);

                using (SqlDataReader ReadData = cmdSELECT.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    //  while (ReadData.Read())
                    //{
                    //  ReadData.GetValue(4).ToString().Trim();
                    //}

                    if (ReadData.HasRows)
                    {
                        ViewBag.AnswerReg = "Такой пользователь уже существует!";
                        ViewBag.AnswerType = "error";
                        
                    }
                    else
                    {
                        flag = true;
                    }
                }

                if (flag)
                {
                    conn.Open();
                    cmdINSERT.CommandText = String.Format(@"INSERT INTO[dbo].[users]
           ([users_name]
           ,[users_secname]
           ,[users_pass]
           ,[users_email]
           ,[users_money]
           ,[users_accept]
           ,[users_regdate]
           ,[users_ip]
           ,[users_ban])
     VALUES
           ('{0}'
           ,'{1}'
           ,'{2}'
           ,'{3}'
           ,'{4}'
           ,'{5}'
           ,'{6}'
           ,'{7}'
           ,'{8}')", Name, SecName, HashPassword(password), Email, 0, 0, DateTime.Now, Request.UserHostAddress, 0);
                    cmdINSERT.ExecuteNonQuery();

                    cmdINSERT.CommandText = String.Format(@"INSERT INTO [dbo].[tech_user]
           ([Tech_ip]
           ,[tech_dnsname]
           ,[tech_lang]
           ,[tech_browser])
     VALUES
           ('{0}'
           ,'{1}'
           ,'{2}'
           ,'{3}')", Request.UserHostAddress, Request.UserHostName, Request.UserLanguages, Request.Browser);

                    cmdINSERT.ExecuteNonQuery();

                    ViewBag.AnswerReg = "Вы успешно зарегистрировались на сайте!" +
                        Environment.NewLine + "Вы будете автоматически перенаправлены на страницу авторизации через 5 секунд!";
                    ViewBag.AnswerType = "success";
                }

            }
            return PartialView();
        }

        public static string HashPassword(string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }

        public static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            byte[] buffer4;
            if (hashedPassword == null)
            {
                return false;
            }
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            byte[] src = Convert.FromBase64String(hashedPassword);
            if ((src.Length != 0x31) || (src[0] != 0))
            {
                return false;
            }
            byte[] dst = new byte[0x10];
            Buffer.BlockCopy(src, 1, dst, 0, 0x10);
            byte[] buffer3 = new byte[0x20];
            Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);


            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
            {
                buffer4 = bytes.GetBytes(0x20);
            }

            return ByteArraysEqual(buffer3, buffer4);
        }

        public static bool VerifyHashedString(string hashedEl1, string hashedEl2)
        {
            if (hashedEl1 == null)
            {
                return false;
            }
            if (hashedEl2 == null)
            {
                return false;
            }

            byte[] src = Convert.FromBase64String(hashedEl1);
            if ((src.Length != 0x31) || (src[0] != 0))
            {
                return false;
            }
            byte[] dst = new byte[0x10];
            Buffer.BlockCopy(src, 1, dst, 0, 0x10);
            byte[] buffer3 = new byte[0x20];
            Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);

            byte[] src2 = Convert.FromBase64String(hashedEl2);
            if ((src2.Length != 0x31) || (src2[0] != 0))
            {
                return false;
            }
            byte[] dst2 = new byte[0x10];
            Buffer.BlockCopy(src2, 1, dst2, 0, 0x10);
            byte[] buffer32 = new byte[0x20];
            Buffer.BlockCopy(src2, 0x11, buffer32, 0, 0x20);


            return ByteArraysEqual(buffer3, buffer32);
        }

        public static bool ByteArraysEqual(byte[] b1, byte[] b2)
        {
            if (b1 == b2) return true;
            if (b1 == null || b2 == null) return false;
            if (b1.Length != b2.Length) return false;
            for (int i = 0; i < b1.Length; i++)
            {
                if (b1[i] != b2[i]) return false;
            }
            return true;
        }


        public EmptyResult CreateSession()
        {
            if (Session["CookieOmnOmn"] != null)
                Session["UserName"] = Session["CookieOmnOmn"];


            if (Session_Id == null)
                Session_Id = Session.SessionID;
            if (Session["UserName"] != null)
                UserName = Session["UserName"].ToString();
            else UserName = "Anonymous";


            using (SqlConnection conn = new SqlConnection(DbConnect))
            {
                bool flag = false;
                conn.Open();
                SqlCommand cmdSELECT = conn.CreateCommand();
                SqlCommand cmdINSERT = conn.CreateCommand();
                SqlCommand cmdUPDATE = conn.CreateCommand();
                SqlCommand cmdDELETE = conn.CreateCommand();

                cmdSELECT.CommandText = String.Format(@"SELECT TOP (1000) [id_session]
      ,[putdate]
      ,[user]
      ,[ip]
  FROM [whomake_database_on].[dbo].[session] WHERE id_session = '{0}'", Session_Id);

                using (SqlDataReader ReadData = cmdSELECT.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    //  while (ReadData.Read())
                    //{
                    //  ReadData.GetValue(4).ToString().Trim();
                    //}

                    if (!ReadData.HasRows)
                    {
                        flag = true;
                    }
                }

                if (flag)
                {
                    conn.Open();

                    cmdDELETE.CommandText = String.Format(@"DELETE FROM [dbo].[session]
      WHERE ip = '{0}'", Request.UserHostAddress);
                    cmdDELETE.ExecuteNonQuery();

                    cmdINSERT.CommandText = String.Format(@"INSERT INTO [dbo].[session]
           ([id_session]
           ,[putdate]
           ,[user]
           ,[ip])
     VALUES
           ('{0}'
           ,'{1}'
           ,'{2}'
           ,'{3}')", Session_Id, DateTime.Now, UserName, Request.UserHostAddress);

                    cmdINSERT.ExecuteNonQuery();
                }

                else
                {
                    conn.Open();
                    cmdUPDATE.CommandText = String.Format(@"UPDATE [dbo].[session]
   SET [id_session] = '{0}'
      ,[putdate] = '{1}'
      ,[user] = '{2}'
      ,[ip] = '{3}'
 WHERE id_session='{4}'", Session_Id ,DateTime.Now, UserName, Request.UserHostAddress, Session_Id);
                    cmdUPDATE.ExecuteNonQuery();
                }
                }


                return new EmptyResult();
        }

        public void CreateNewTask()
        {
          
            //Thread.Sleep(20000);
            HttpContext.Response.Write("true");
        }


    }


 


}