using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace think.api
{
    /// <summary>
    /// Summary description for login
    /// </summary>
    public class login : IHttpHandler
    {
        private class RequestBody {
            public string email { get; set; }
            public string password { get; set; }
        }

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.HttpMethod == "POST") {
                string query, status, message, json;
                Dictionary<string, string> resData = new Dictionary<string, string>();

                JsonHelper jsonConverter = new JsonHelper();
                RequestBody data = jsonConverter.parseWithStream<RequestBody>(context.Request.InputStream);
                InternalSqlCrud crud = new InternalSqlCrud();
                query = "SELECT * FROM users WHERE email='" + data.email + "' AND password='" + data.password + "'";
                SqlDataReader dataReader = crud.executeReader(query);
                if (dataReader.HasRows)
                {
                    dataReader.Read();
                    string userType = dataReader["userType"].ToString();
                    
                    HttpCookie cookie1 = new HttpCookie("userId", dataReader["id"].ToString());
                    HttpCookie cookie2 = new HttpCookie("userType", userType);
                    cookie1.Expires = DateTime.Now.AddMonths(1);
                    cookie2.Expires = DateTime.Now.AddMonths(1);
                    context.Response.Cookies.Add(cookie1);
                    context.Response.Cookies.Add(cookie2);
                    
                    status = "200";
                    message = "Success";
                    resData.Add("userType", userType == "admin" ? "admin" : "user");
                }
                else {
                    status = "404";
                    message = "Wrong credentials";
                }
                json = jsonConverter.stringWithResponse(resData);
                context.Response.StatusCode = Convert.ToInt16(status);
                context.Response.ContentType = "application/json";
                context.Response.StatusDescription = message;
                context.Response.Write(json);
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