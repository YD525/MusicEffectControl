using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsonCore
{
    public class JsonHelper
    {
        public static T JsonParse<T>(string Content)
        {
            return JsonConvert.DeserializeObject<T>(Content);
        }

        public static string CreatJson(object Any)
        {
            return JsonConvert.SerializeObject(Any);
        }

        public static JObject GetObj(string Content)
        {
            return JObject.Parse(Content);
        }
    }
}
