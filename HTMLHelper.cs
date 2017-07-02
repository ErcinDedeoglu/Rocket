using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace Rocket
{
    public class HTMLHelper
    {
        public List<HtmlNode> HTMLNodeListByClass(HtmlDocument htmlDocument, string clasName, string descendants = "div")
        {
            List<HtmlNode> hasFloatClass = htmlDocument.DocumentNode
                .Descendants(descendants)
                .Where(div => HasClass(div, clasName)).ToList();

            return hasFloatClass;
        }

        public static Boolean HasClass(HtmlNode element, String className)
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
        private static Boolean CheapClassListContains(String haystack, String needle, StringComparison comparison)
        {
            if (String.Equals(haystack, needle, comparison)) return true;
            Int32 idx = 0;
            while (idx + needle.Length <= haystack.Length)
            {
                idx = haystack.IndexOf(needle, idx, comparison);
                if (idx == -1) return false;

                Int32 end = idx + needle.Length;

                // Needle must be enclosed in whitespace or be at the start/end of string
                Boolean validStart = idx == 0 || Char.IsWhiteSpace(haystack[idx - 1]);
                Boolean validEnd = end == haystack.Length || Char.IsWhiteSpace(haystack[end]);
                if (validStart && validEnd) return true;

                idx++;
            }
            return false;
        }
    }
}