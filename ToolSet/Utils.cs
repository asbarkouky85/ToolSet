using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ToolSet
{
    public class Utils
    {
        public static string CombineUrl(params string[] parts)
        {
            string ret = "";
            Regex reg = new Regex("^/");
            bool first = true;
            for (var i = 0; i < parts.Length; i++)
            {
                string part = parts[i];
                if (string.IsNullOrEmpty(part))
                    continue;
                part = part.Replace("\\", "/");
                if (!first)
                    part = reg.Replace(part, "");

                first = false;
                if (i < (parts.Length - 1))
                {
                    char last = part[part.Length - 1];
                    part += (last != '/') ? "/" : "";
                }
                ret += part;
            }
            return ret;
        }
    }
}
