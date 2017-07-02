using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Rocket
{
    public class RegexHelper
    {
        public static List<Tuple<int, string>> RegexOptionList()
        {
            var regexOptList = new List<Tuple<int, string>>
            {
                Tuple.Create(1, "Compiled"),
                Tuple.Create(2, "CultureInvariant"),
                Tuple.Create(3, "ECMAScript"),
                Tuple.Create(4, "ExplicitCapture"),
                Tuple.Create(5, "IgnoreCase"),
                Tuple.Create(6, "IgnorePatternWhitespace"),
                Tuple.Create(7, "Multiline"),
                Tuple.Create(8, "RightToLeft"),
                Tuple.Create(9, "Singleline"),
                Tuple.Create(10, "None")
            };

            return regexOptList;
        }

        public static string ContentByStartAndEnd(string content, string start, string end)
        {
            return new Regex(start + "(.*?)" + end).Match(content).Groups[1].ToString();
        }

        public static string ParentLevelOfURL(string url)
        {
            return new Regex("^.*\\/").Match(url).Value;
        }

        public static string SubString(string source, string start, string end, RegexOptions regexOptions)
        {
            Regex r = new Regex(Regex.Escape(start) + "(.*?)" + Regex.Escape(end), regexOptions);
            MatchCollection matches = r.Matches(source);
            string subs = string.Empty;
            foreach (Match match in matches)
            {
                subs += match.Groups[1].Value;
            }
            return subs;
        }

        public static List<string> SubStringList(string source, string start, string end, RegexOptions regexOptions)
        {
            List<string> subStringList = new List<string>();
            Regex r = new Regex(Regex.Escape(start) + "(.*?)" + Regex.Escape(end), regexOptions);
            MatchCollection matches = r.Matches(source);
            foreach (Match match in matches)
            {
                string theStr = match.Groups[1].Value;
                if (!string.IsNullOrEmpty(theStr))
                {
                    subStringList.Add(match.Groups[1].Value);
                }
            }
            return subStringList;
        }

        public static string ClearUnwanted(string source, string unwantedChars)
        {
            return new string(source.Where(unwantedChars.Contains).ToArray());
        }

        public static string RemoveNewLines(string source)
        {
            return Regex.Replace(source, @"\t|\n|\r", "");
        }

        public static string RemoveWhiteSpaceBetweenTags(string source)
        {
            return Regex.Replace(source, @"(?:(?<=^\<mattext\>)[^\<]*)|(?:[^\>]*(?=\</mattext\>$))", string.Empty, RegexOptions.Multiline);
        }

        public static string RemoveWhiteSpaceInsideTags(string source)
        {
            return Regex.Replace(source, @"\s*(<[^>]+>)\s*", "$1", RegexOptions.Multiline);
        }

        public static string RemoveTags(string source)
        {
            return Regex.Replace(source, "<.*?>", string.Empty);
        }

        public static string RemoveUnusedTags(string source)
        {
            return Regex.Replace(source, @"<(\w+)\b(?:\s+[\w\-.:]+(?:\s*=\s*(?:""[^""]*""|'[^']*'|[\w\-.:]+))?)*\s*/?>\s*</\1\s*>", string.Empty, RegexOptions.Multiline);
        }

        public static string RemoveAllTagsExcept(string source, string exceptList)
        {
            //Except must be: x|y|z
            string acceptableTags = exceptList;
            string whiteListPattern = "</?(?(?=" + acceptableTags + ")notag|[a-zA-Z0-9]+)(?:\\s[a-zA-Z0-9\\-]+=?(?:([\"']?).*?\\1?)?)*\\s*/?>";
            return Regex.Replace(source, whiteListPattern, "", RegexOptions.Compiled);
        }
    }
}