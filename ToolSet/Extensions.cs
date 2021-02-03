using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ToolSet
{
    public static class Extensions
    {

        public static string GetMessageRecursive(this Exception ex, bool ignorInvocationException = true)
        {

            if (ex is TargetInvocationException && ignorInvocationException)
            {
                if (ex.InnerException != null)
                    return ex.InnerException.GetMessageRecursive();
                return "";
            }
            else
            {
                string message = " (" + ex.GetType().Name + ")  " + ex.Message;
                if (ex.InnerException != null)
                    message += " >> " + ex.InnerException.GetMessageRecursive(ignorInvocationException);
                return message;
            }


        }

        public static string[] GetStackTrace(this Exception ex, bool recurse = false, bool ignorInvocationException = true)
        {

            if (ex is TargetInvocationException && ignorInvocationException)
            {
                if (ex.InnerException != null)
                    return ex.InnerException.GetStackTrace();
                return new string[] { };
            }
            else if (recurse)
            {
                List<string> message = ex.StackTrace.Split('\r', '\n').Where(d => d.Length > 0).ToList();
                if (ex.InnerException != null)
                {
                    var inner = ex.InnerException.GetStackTrace(recurse, ignorInvocationException);
                    message.Add("");
                    message.Add("INNER STACK");
                    message.Add("");
                    message.AddRange(inner);
                }

                return message.ToArray();
            }
            else
            {
                return ex.StackTrace.Split('\r', '\n').Where(d => d.Length > 0).ToArray();
            }


        }

        public static string GetBefore(this string subject, string str)
        {
            int ind = subject.IndexOf(str);
            if (ind > 0)
                return subject.Substring(0, ind);
            else
                return subject;
        }

        public static string GetBeforeLast(this string subject, string str)
        {
            int ind = subject.LastIndexOf(str);
            if (ind != 0)
                return subject.Substring(0, ind);
            else
                return subject;
        }

        public static string GetBeforeFirst(this string subject, string str)
        {
            int ind = subject.IndexOf(str);
            if (ind > 0)
                return subject.Substring(0, ind);
            else
                return subject;
        }

        public static string GetAfterLast(this string subject, string str)
        {
            int ind = subject.LastIndexOf(str);
            if (ind != 0)
                return subject.Substring(ind + str.Length);
            else
                return subject;
        }

        public static string GetAfterFirst(this string subject, string str, int length = 0)
        {
            int ind = subject.IndexOf(str);
            if (ind != 0)
            {
                if (length == 0)
                    return subject.Substring(ind + str.Length);
                else
                    return subject.Substring(ind + str.Length, length);
            }

            else
                return subject;
        }

        public static string ToFolderPath(this string st)
        {
            st.Replace('/', '\\');
            if (st[st.Length - 1] != '\\')
                st += '\\';
            return st;
        }

        public static string GetTagContent(this string subject, string tag)
        {
            Regex pattern = new Regex($"<{tag}>(.*?)</{tag}>");
            var s = pattern.Match(subject);
            if (!s.Success)
                return null;
            return s.Groups[1].Value;
        }

        public static bool GetPatternContents(this string subject, string pattern, out string[] data)
        {
            Regex reg = new Regex(pattern);
            var s = reg.Match(subject);
            if (!s.Success)
            {
                data = new string[0];
                return false;
            }
            var lst = new List<string>();
            for (var i = 0; i < s.Groups.Count; i++)
            {
                if (i > 0)
                {
                    lst.Add(s.Groups[i].Value);
                }
            }
            data = lst.ToArray();
            return true;
        }


    }
}
