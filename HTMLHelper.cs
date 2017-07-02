using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace Rocket
{
    public class HTMLHelper
    {
        public static List<HtmlNode> HTMLNodeListByClass(HtmlDocument htmlDocument, string clasName, string descendants = "div")
        {
            List<HtmlNode> hasFloatClass = htmlDocument.DocumentNode
                .Descendants(descendants)
                .Where(div => HasClass(div, clasName)).ToList();

            return hasFloatClass;
        }

        public static Boolean HasClass(HtmlNode element, string className)
        {
            if (element == null) throw new ArgumentNullException(nameof(element));
            if (string.IsNullOrWhiteSpace(className)) throw new ArgumentNullException(nameof(className));
            if (element.NodeType != HtmlNodeType.Element) return false;

            HtmlAttribute classAttrib = element.Attributes["class"];
            if (classAttrib == null) return false;

            bool hasClass = CheapClassListContains(classAttrib.Value, className, StringComparison.Ordinal);
            return hasClass;
        }

        /// <summary>Performs optionally-whitespace-padded string search without new string allocations.</summary>
        /// <remarks>A regex might also work, but constructing a new regex every time this method is called would be expensive.</remarks>
        private static bool CheapClassListContains(string haystack, string needle, StringComparison comparison)
        {
            if (string.Equals(haystack, needle, comparison)) return true;
            int idx = 0;
            while (idx + needle.Length <= haystack.Length)
            {
                idx = haystack.IndexOf(needle, idx, comparison);
                if (idx == -1) return false;

                int end = idx + needle.Length;

                // Needle must be enclosed in whitespace or be at the start/end of string
                bool validStart = idx == 0 || Char.IsWhiteSpace(haystack[idx - 1]);
                bool validEnd = end == haystack.Length || Char.IsWhiteSpace(haystack[end]);
                if (validStart && validEnd) return true;

                idx++;
            }
            return false;
        }
    }
}