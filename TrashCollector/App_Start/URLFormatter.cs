using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace TrashCollector.App_Start
{
    public static class URLFormatter
    {
        public static string Format(string input)
        {
            StringBuilder stringBuilder = new StringBuilder("");
            foreach (char c in input.ToCharArray())
                if (c != ' ')
                    stringBuilder.Append(c);
                else
                    stringBuilder.Append('+');
            return stringBuilder.ToString();
        }
    }
}