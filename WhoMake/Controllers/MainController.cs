﻿using System;
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
using WhoMake.Models;
using PagedList.Mvc;
using PagedList;
using System.Collections;
using System.Globalization;

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

        public ActionResult ServicesList(string id, string id_serv)
        {
            if (id_serv != "") ViewBag.id_serv = id_serv;
            List<DataBase.services> services = null;
            if (id != "")
            {
                
                services = context.services.Where(x => x.services_id_category == Convert.ToInt32(id)).ToList();

                return PartialView(services);
            }
            else
            {
                return PartialView();
            }
         }

        public ActionResult CategoriesList(string id)
        {
            ViewBag.id = id ?? "";
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

        public ActionResult Performers(string category_id, string name, int? page)
        {
                if (category_id == null) category_id = "";
                if (name == null) name = "";
            //Thread.Sleep(5000);
            List<performers> performer = null;

                if (category_id != "" && name != "")
                performer = (from perf in context.performers
                             join cat_perf in context.category_performers on perf.performers_id equals cat_perf.category_performers_id_person
                             where (Convert.ToInt32(category_id) == cat_perf.category_performers_id_category && perf.performers_name.ToLower().Trim().Contains(name.ToLower().Trim()))
                             select perf).ToList();

                else if (category_id != "" && name == "")
                performer = (from perf in context.performers
                             join cat_perf in context.category_performers on perf.performers_id equals cat_perf.category_performers_id_person
                             where Convert.ToInt32(category_id) == cat_perf.category_performers_id_category
                             select perf).ToList();

                else if (category_id == "" && name != "")
                performer = (from perf in context.performers
                             join cat_perf in context.category_performers on perf.performers_id equals cat_perf.category_performers_id_person
                             where perf.performers_name.ToLower().Trim().Contains(name.ToLower().Trim())
                             select perf).ToList();
            // context.performers.Join(context.category_performers, // второй набор
            //p => p.performers_id, // свойство-селектор объекта из первого набора
            //c => c.category_performers_id_person, // свойство-селектор объекта из второго набора
            //(p, c) => new {p,c}).W);



            else if (category_id == "" && name == "")
                performer = context.performers.ToList();

                ViewBag.category = category_id;
                ViewBag.name = name;

                int pageSize = 10;
                int pageNumber = (page ?? 1);
                return View(performer.ToPagedList(pageNumber, pageSize));

        }
        
        public ActionResult Task(string id, string backUrl)
        {
            if (id == null || backUrl == null)
                return HttpNotFound();
            else
            {
                ViewBag.URL = backUrl;
                
                tasks task = context.tasks.Where(x => x.tasks_id == Convert.ToInt32(id)).First();

                users user = context.users.Where(x => x.users_id == task.tasks_id_person).First();


                ViewBag.Task = task;
                ViewBag.User = user;
                var result = context.tasks.SingleOrDefault(b => b.tasks_id == task.tasks_id);
                    if (result != null)
                    {
                    result.tasks_views += 1;
                        context.SubmitChanges();
                    }
                

                return View();
            }
        }


        public ActionResult Tasks(string category_id, string service_id, string name, int? page)
        {

            if (category_id == null) category_id = "";
            if (service_id == null) service_id = "";
            if (name == null) name = "";
            //Thread.Sleep(5000);
            List<tasks> task = null;
            if(category_id != "" && service_id != "" && name != "")
                task = context.tasks.Where(x => x.tasks_category == Convert.ToInt32(category_id) 
                                             && x.tasks_service == Convert.ToInt32(service_id) 
                                             && x.tasks_name.ToLower().Trim().Contains(name.ToLower().Trim())).ToList();

            else if(category_id != "" && service_id != "" && name == "")
                task = context.tasks.Where(x => x.tasks_category == Convert.ToInt32(category_id)
                             && x.tasks_service == Convert.ToInt32(service_id)).ToList();

            else if(category_id != "" && service_id == "" && name == "")
                task = context.tasks.Where(x => x.tasks_category == Convert.ToInt32(category_id)).ToList();

            else if(category_id == "" && service_id == "" && name == "")
                task = context.tasks.ToList();

            else if (category_id == "" && service_id != "" && name == "")
                task = context.tasks.Where(x => x.tasks_service == Convert.ToInt32(service_id)).ToList();

            else if (category_id == "" && service_id == "" && name != "")
                task = context.tasks.Where(x => x.tasks_name.ToLower().Trim().Contains(name.ToLower().Trim())).ToList();

            else if (category_id == "" && service_id != "" && name != "")
                task = context.tasks.Where(x => x.tasks_service == Convert.ToInt32(service_id)
                                             && x.tasks_name.ToLower().Trim().Contains(name.ToLower().Trim())).ToList();

            else if (category_id != "" && service_id == "" && name != "")
                task = context.tasks.Where(x => x.tasks_category == Convert.ToInt32(category_id)
                                             && x.tasks_name.ToLower().Trim().Contains(name.ToLower().Trim())).ToList();

            ViewBag.category = category_id;
            ViewBag.service= service_id;
            ViewBag.name = name;

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(task.ToPagedList(pageNumber, pageSize));

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
            string user = "", regdate = "", money = "", name = "", secname = "", id = "";
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
   ,[users_secname]
   ,[users_id]FROM [whomake_database_on].[dbo].[users]");

                    using (SqlDataReader ReadData = cmdSELECT.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (ReadData.Read())
                        {
                            string[] tempString = new string[7];
                            tempString[0] = ReadData.GetValue(0).ToString().Trim();
                            tempString[1] = ReadData.GetValue(1).ToString().Trim();
                            tempString[2] = ReadData.GetValue(2).ToString().Trim();
                            tempString[3] = ReadData.GetValue(3).ToString().Trim();
                            tempString[4] = ReadData.GetValue(4).ToString().Trim();
                            tempString[5] = ReadData.GetValue(5).ToString().Trim();
                            tempString[6] = ReadData.GetValue(6).ToString().Trim();
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
                            id = itemLst[6];
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
                    Session["users_id"] = id;
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
            string  regdate = "", money = "", name = "", secname = "", id = "";
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
   ,[users_secname]
   ,[users_id] FROM [whomake_database_on].[dbo].[users] WHERE [users_email] = '{0}'", username);

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
                            id = ReadData.GetValue(6).ToString().Trim();
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
                    Session["users_id"] = id;

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
            else
                UserName = "Anonymous";
        

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

        [ValidateInput(false)]
        public void CreateNewTask(string category_id, 
                                  string service_id, 
                                  string title_id, 
                                  string description, 
                                  string price_id, 
                                  string date_exeq_begin,
                                  string time_exeq_begin,
                                  string date_exeq_end,
                                  string time_exeq_end,
                                  string date_begin,
                                  string date_end,
                                  string location_id,
                                  string adres_id,
                                  string phone_id)
        {
            var category = context.category.Where(x => x.category_id == Convert.ToInt32(category_id)).ToList();

            //if (date_exeq_begin == null)
            //    date_exeq_begin = "00.00.0000";
            //if (time_exeq_begin == null)
            //    time_exeq_begin = "00.00.0000";
            //if (date_exeq_end == null)
            //    date_exeq_end = "00.00.0000";
            //if (time_exeq_end == null)
            //    time_exeq_end = "00.00.0000";
            //if (date_begin == null)
            //    date_begin = "00.00.0000";
            //if (date_end == null)
            //    date_end = "00.00.0000";


            DateTime date_exeq_beginD;
            if (date_exeq_begin == null)
                date_exeq_beginD = new DateTime();
            else
            {
                date_exeq_beginD = DateTime.Parse(date_exeq_begin);
            }

            TimeSpan time_exeq_beginD;
            if (time_exeq_begin == null)
                time_exeq_beginD = new TimeSpan();
            else
            {
                time_exeq_beginD = TimeSpan.Parse(time_exeq_begin + ":00");
            }
            DateTime date_exeq_endD;
            if (date_exeq_end == null)
                date_exeq_endD = new DateTime();
            else
            {
                date_exeq_endD = DateTime.Parse(date_exeq_end);
            }
            TimeSpan time_exeq_endD;
            if (time_exeq_end == null)
                time_exeq_endD = new TimeSpan();
            else
            {
                time_exeq_endD = TimeSpan.Parse(time_exeq_end+":00");
            }
            DateTime date_beginD;
            if (date_begin == null)
                date_beginD = new DateTime();
            else
            {
                date_beginD = DateTime.Parse(date_begin);
            }
            DateTime date_endD;
            if (date_end == null)
                date_endD = new DateTime();
            else
            {
                date_endD = DateTime.Parse(date_end);
            }

            string name = Session["users_name"].ToString();
            string secname = Session["users_secname"].ToString();
            string id = Session["users_id"].ToString();
            var newTask = new tasks {
                tasks_id_person = Convert.ToInt32(id),
                tasks_category = Convert.ToInt32(category_id),
                tasks_service = Convert.ToInt32(service_id),
                tasks_name = title_id,
                tasks_description = description,
                tasks_price = Convert.ToInt32(price_id),
                tasks_phone = phone_id,
                tasks_date_begin = date_beginD,
                tasks_date_end = date_endD,
                tasks_date_exeq_begin = date_exeq_beginD,
                tasks_time_exeq_begin = time_exeq_beginD,
                tasks_date_exeq_end = date_exeq_endD,
                tasks_time_exeq_end = time_exeq_endD,
                tasks_adres = adres_id,
                tasks_name_person = name,
                tasks_secname_person = secname,
                tasks_creation = DateTime.Now,
                tasks_status = "Открыто",
                tasks_views = 0,
                tasks_money = category[0].category_price
        }; 
            context.tasks.InsertOnSubmit(newTask);
            context.tasks.Context.SubmitChanges();

            Thread.Sleep(5000);
            HttpContext.Response.Write("true");
        }


    }


 


}