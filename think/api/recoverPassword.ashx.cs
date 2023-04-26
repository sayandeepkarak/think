using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace think.api
{
    /// <summary>
    /// Summary description for recoverPassword
    /// </summary>
    public class recoverPassword : IHttpHandler
    {
        private class RequestBody
        {
            public string email { get; set; }
        }

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.HttpMethod == "POST") {
                Dictionary<string, string> resData = new Dictionary<string, string>();

                JsonHelper converter = new JsonHelper();
                RequestBody input = converter.parseWithStream<RequestBody>(context.Request.InputStream);
                InternalSqlCrud crud = new InternalSqlCrud();

                SqlDataReader data = crud.executeReader("SELECT fullname,password FROM users WHERE email='" + input.email + "'");
                if (data.HasRows) {
                    data.Read();

                    string serviceMail = "think.sayandeepkarak.official@gmail.com";
                    string appCode = "espsquqqxwuklvzd";

                    MailSender mail = new MailSender(serviceMail,appCode);
                    string message = "Hello " + data["fullname"] + "\nYour think member account's password is " + data["password"];
                    mail.loadAndDispatch(input.email, "Recover Password", message, (bool res) =>
                    {
                        resData.Add("status", res ? "200" : "500");
                        resData.Add("message", res ? data["fullname"].ToString() : "Internal server error");
                    });

                } else {
                    resData.Add("status", "404");
                    resData.Add("message", "Email id not found");
                }
                string json = converter.stringWithResponse(resData);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = int.Parse(resData["status"]);
                context.Response.StatusDescription = resData["message"];
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