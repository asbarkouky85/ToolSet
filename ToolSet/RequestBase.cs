using System;
using System.Collections.Generic;
using System.IO;

namespace ToolSet
{
    public abstract class RequestBase
    {
        protected string[] Arguments { get; set; }
        public string MainDirectory { get; set; }

        public RequestBase(string[] args)
        {
            Arguments = PrepArgs(args);
        }

        public abstract void ProcessArgs();
        public abstract void Execute(Dispatcher service);
        public abstract string[] GetHelp();

        protected string[] PrepArgs(string[] args)
        {
            bool ignore = false;

            var lst = new List<string>();
            for (var i = 0; i < args.Length; i++)
            {
                if (args[i].ToLower() == "--folder" || args[i] == "-d")
                {
                    MainDirectory = args[i + 1];
                    ignore = true;
                }
                else if (!ignore)
                {
                    lst.Add(args[i]);
                }
                else
                {
                    ignore = false;
                }

            }

            MainDirectory = MainDirectory ?? AppDomain.CurrentDomain.BaseDirectory.ToFolderPath();
            if (!Directory.Exists(MainDirectory))
                throw new Exception("Cannot find Directory : " + MainDirectory);
            return lst.ToArray();
        }

        protected string FindArg(char id, string name = null)
        {
            name = name?.ToLower();
            for (var i = 0; i < Arguments.Length; i++)
            {
                if ((name == null && Arguments[i].ToLower() == "--" + name) || Arguments[i] == "-d")
                {
                    if (Arguments.Length > i)
                        return Arguments[i + 1];
                }
            }
            return null;

        }

        protected string ToShortVersion(string v)
        {
            string[] parts = v.Split(new char[] { '.' });
            string[] full = new string[] { "1", "0" };

            for (int i = 0; i < parts.Length; i++)
            {
                if (i >= full.Length)
                    break;
                full[i] = parts[i];
            }

            return string.Join(".", full);
        }



        protected string ToLongVersion(string v)
        {
            string[] parts = v.Split(new char[] { '.' });
            if (parts.Length >= 4)
                return v;

            string[] full = new string[] { "1", "0", "0", "0" };

            for (int i = 0; i < parts.Length; i++)
            {
                if (i >= full.Length)
                    break;
                full[i] = parts[i];
            }
            return string.Join(".", full);
        }
    }
}
