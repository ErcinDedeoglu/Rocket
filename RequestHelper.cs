using System.Web;

namespace Rocket
{
    public class RequestHelper
    {
        public static string QueryStringBuilder()
        {
            string str = (string)null;
            foreach (string allKey in HttpContext.Current.Request.QueryString.AllKeys)
            {
                if (!string.IsNullOrWhiteSpace(allKey))
                    str = str + allKey + "=" + HttpContext.Current.Request.QueryString[allKey] + "&";
            }
            if (str != null)
                str = "?" + str.Remove(str.Length - 1);
            return str;
        }
    }
}