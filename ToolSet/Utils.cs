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
        public static string IdToPhrase(string id)
        {
            if (id.Contains("__"))
                id = id.GetAfterLast("__");

            id = id.Replace("_", " ");
            Regex r = new Regex("[A-Z]");
            MatchCollection col = r.Matches(id);
            int i = 0;

            foreach (Match d in col)
            {
                if (d.Index != 0)
                {
                    id = id.Insert(d.Index + (i++), " ");
                }
            }

            string[] words = id.Split(' ');
            string[] joiners = new string[] { "At", "To", "Of", "The", "And", "From", "In" };
            string res = "";

            for (int ii = 0; ii < words.Length; ii++)
            {
                if (ii != 0)
                    words[ii] = words[ii].ToLower();
                string sep = (ii == (words.Length - 1) || words[ii].Length == 1) ? "" : " ";
                res += words[ii] + sep;
            }

            return res;
        }

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
