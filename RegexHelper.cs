using System.Text.RegularExpressions;

namespace Rocket
{
    public class RegexHelper
    {
        public static string ContentByStartAndEnd(string content, string start, string end)
        {
            return new Regex(start + "(.*?)" + end).Match(content).Groups[1].ToString();
        }

        public static string ParentLevelOfURL(string url)
        {
            return new Regex("^.*\\/").Match(url).Value;
        }
    }
}
