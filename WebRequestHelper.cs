using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;

namespace Rocket
{
    public class WebRequestHelper
    {
        public static string ContentFromURL(string url, string referrer, string postData = null, string method = "POST")
        {
            string str = (string)null;
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            byte[] buffer = new byte[0];
            if (postData != null)
                buffer = Encoding.ASCII.GetBytes(postData);
            httpWebRequest.Method = method;
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            httpWebRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            httpWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.103 Safari/537.36";
            httpWebRequest.ContentLength = (long)buffer.Length;
            httpWebRequest.Referer = referrer;
            if (postData != null)
            {
                using (Stream requestStream = httpWebRequest.GetRequestStream())
                    requestStream.Write(buffer, 0, buffer.Length);
            }
            Stream responseStream = httpWebRequest.GetResponse().GetResponseStream();
            if (responseStream != null)
                str = new StreamReader(responseStream).ReadToEnd();
            return str;
        }
        

    }
}