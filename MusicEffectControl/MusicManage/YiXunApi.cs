using JsonCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicEffectControl.MusicManage
{
    public class YiXunApi
    {
        public static string Host = "http://XXX.top/dubBgMusic/";

        public static MusicTag[] QueryTypes()
        {
            var Result = new RequestHelper().HttpPost(
                Host + "getAllTypes",
                ""
                );
            return JsonHelper.JsonParse<MusicTag[]>(Result);
        }

        public static WebMusic[] SearchMusic(string Type)
        {
            var Result = new RequestHelper().HttpPost(
               Host + "selectByType",
               "type1=" + Type
               );
            return JsonHelper.JsonParse<WebMusic[]>(Result);
        }
    }



  

    public class WebMusic
    {
        public string id { get; set; }
        public int len { get; set; }
        public string name { get; set; }
        public string remark { get; set; }
        public string type { get; set; }
        public string url { get; set; }
    }

    public class MusicTag
    {
        public string type { get; set; }
    }

}
