using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace MusicEffectControl.MusicManage
{
    public class RequestHelper
    {
        public string HttpPost(string posturl, string postData, string contentType = "application/x-www-form-urlencoded")
        {
            Stream stream = null;
            Stream stream2 = null;
            StreamReader streamReader = null;
            HttpWebResponse httpWebResponse = null;
            HttpWebRequest httpWebRequest = null;
            Encoding encoding = Encoding.GetEncoding("utf-8");
            byte[] bytes = encoding.GetBytes(postData);
            try
            {
                if (posturl.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    ServicePointManager.ServerCertificateValidationCallback = ((object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors) => true);
                    httpWebRequest = (WebRequest.Create(posturl) as HttpWebRequest);
                    httpWebRequest.ProtocolVersion = HttpVersion.Version10;
                }
                else
                {
                    httpWebRequest = (WebRequest.Create(posturl) as HttpWebRequest);
                }

                CookieContainer cookieContainer2 = httpWebRequest.CookieContainer = new CookieContainer();
                httpWebRequest.AllowAutoRedirect = true;
                httpWebRequest.Method = "POST";
                httpWebRequest.ContentType = contentType;
                httpWebRequest.ContentLength = bytes.Length;
                stream = httpWebRequest.GetRequestStream();
                if (bytes.Length > 0)
                {
                    stream.Write(bytes, 0, bytes.Length);
                }
                stream.Close();
                httpWebResponse = (httpWebRequest.GetResponse() as HttpWebResponse);
                stream2 = httpWebResponse.GetResponseStream();
                streamReader = new StreamReader(stream2, encoding);
                string result = streamReader.ReadToEnd();
                string empty = string.Empty;
                return result;
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return string.Empty;
            }
        }
    }
}
