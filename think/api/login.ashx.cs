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
                JsonHelper jsonConverter = new JsonHelper();
                RequestBody data = jsonConverter.parseWithStream<RequestBody>(context.Request.InputStream);
                InternalSqlCrud crud = new InternalSqlCrud();
                query = "SELECT * FROM users WHERE email='" + data.email + "' AND password='" + data.password + "'";
                SqlDataReader dataReader = crud.executeReader(query);
                if (dataReader.HasRows)
                {
                    dataReader.Read();
                    HttpCookie cookie = new HttpCookie("userId",dataReader["id"].ToString());
                    cookie.Expires = DateTime.Now.AddMonths(1);
                    status = "200";
                    message = "Successfully loggedin";
                    context.Response.Cookies.Add(cookie);
                }
                else {
                    status = "404";
                    message = "Wrong credentials";
                }
                json = jsonConverter.stringWithResponse(status,message);
                context.Response.StatusCode = Convert.ToInt16(status);
                context.Response.ContentType = "application/json";
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