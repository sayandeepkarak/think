﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Script.Serialization;

namespace think
{
    public class JsonHelper
    {
        private JavaScriptSerializer serializer;

        public JsonHelper() {
            this.serializer = new JavaScriptSerializer();
        }

        public T parseWithStream<T>(Stream stream) {
            StreamReader reader = new StreamReader(stream);
            string jsonString = reader.ReadToEnd();
            T data = serializer.Deserialize<T>(jsonString);
            return data;
        }

        public string stringWithResponse(string status,string data) {
            Dictionary<string,string> networkResponse = new Dictionary<string, string>();
            networkResponse.Add("status",status);
            networkResponse.Add("message",data);
            string json = serializer.Serialize(networkResponse);
            return json;
        }
    }
}