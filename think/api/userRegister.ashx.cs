using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Script.Serialization;
using System.Data.SqlClient;

namespace think.api
{
    /// <summary>
    /// Summary description for userRegister
    /// </summary>
    public class userRegister : IHttpHandler
    {
        private class RequestBody {
            public string fullname { get; set; }
            public string email { get; set; }
            public string mobile { get; set; }
            public string password { get; set; }
        }

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.HttpMethod == "POST") {
                string query,status,message,json;
                Dictionary<string, string> resData = new Dictionary<string, string>();
                JsonHelper jsonConverter = new JsonHelper();
                RequestBody data = jsonConverter.parseWithStream<RequestBody>(context.Request.InputStream);
                InternalSqlCrud crud = new InternalSqlCrud();
                query = "SELECT * FROM users WHERE email='" + data.email + "'";
                SqlDataReader dataReader = crud.executeReader(query);

                if (!dataReader.HasRows)
                {
                    query = "INSERT INTO users(userType,fullname,email,mobile,password) ";
                    query += "VALUES('user','" + data.fullname + "','" + data.email + "','" + data.mobile + "','" + data.password + "')";
                    bool result = crud.executeCommand(query);
                    dataReader = crud.executeReader("SELECT id FROM users WHERE email='" + data.email + "'");
                    message = result ? "Register Success" : "Internal server error";
                    status = result ? "200" : "500";

                    HttpCookie cookie1 = new HttpCookie("userId", dataReader["id"].ToString());
                    HttpCookie cookie2 = new HttpCookie("userType", "user");
                    cookie1.Expires = DateTime.Now.AddMonths(1);
                    cookie2.Expires = DateTime.Now.AddMonths(1);
                    context.Response.Cookies.Add(cookie1);
                    context.Response.Cookies.Add(cookie2);
                }
                else {
                    status = "409";
                    message = "User already exist";
                    
                }
                resData.Add("status", status);
                resData.Add("message", message);
                json = jsonConverter.stringWithResponse(resData);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = Convert.ToInt16(status);
                context.Response.Write(json);
                context.Response.End();
            }
        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
    }
}