using System;
using System.Collections.Generic;

namespace Rocket
{
    public class StringHelper
    {
        public static List<String> SplitString(string source, string byString)
        {
            return new List<string>(source.Split(new string[] { byString }, StringSplitOptions.None));
        }
    }
}