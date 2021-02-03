using System;
using System.Collections.Generic;
using ToolSet.Ftp;
using ToolSet.Nuget;
using ToolSet.Sql;
using ToolSet.Versions;
using ToolSet.Zip;

namespace ToolSet
{
    public class Dispatcher
    {
        static Dictionary<char, FunctionTypes> OptionsDictionary;

        static Dispatcher()
        {
            OptionsDictionary = new Dictionary<char, FunctionTypes>();
            OptionsDictionary['p'] = FunctionTypes.Project;
            OptionsDictionary['h'] = FunctionTypes.Help;
            OptionsDictionary['n'] = FunctionTypes.Nuget;
            OptionsDictionary['z'] = FunctionTypes.Zip;
            OptionsDictionary['c'] = FunctionTypes.Copy;
            OptionsDictionary['r'] = FunctionTypes.SqlRestore;
            OptionsDictionary['q'] = FunctionTypes.SqlQuery;
        }

        public static void Dispatch(string[] args)
        {
            if (args.Length < 1)
                throw new NotEnoughArgumentsException();

            string opt = args[0];
            var Option = FunctionTypes.Project;
            if (opt[0] == '-' || opt[0] == '/')
            {
                if (opt.Length < 2 || !OptionsDictionary.ContainsKey(opt[1]))
                    throw new NonExistantOption(opt[1].ToString());

                Option = OptionsDictionary[opt[1]];
            }
            if (Option == FunctionTypes.Help)
            {
                Help(new Dispatcher());
            }
            else
            {
                var req = GetRequest(Option, args);
                req.ProcessArgs();
                req.Execute(new Dispatcher());

            }
        }

        public static void Help(Dispatcher dis)
        {
            foreach (var s in OptionsDictionary)
            {
                if (s.Key == 'h')
                    continue;
                var helpLines = GetRequest(s.Value).GetHelp();
                
                Console.Write("toolset -" + s.Key);
                var first = true;
                foreach(var l in helpLines)
                {

                    
                    dis.GotoColumn(2);
                    Console.WriteLine(l+(first?" [-d or --folder [working directory]]":""));

                    first = false;
                }
                Console.WriteLine();
            }
            Console.Write("toolset -h");
            dis.GotoColumn(2);
            Console.WriteLine("Shows help");
            Console.WriteLine();
        }

        private static RequestBase GetRequest(FunctionTypes Option, string[] args = null)
        {
            args = args?? new string[0];
            switch (Option)
            {
                case FunctionTypes.Project:
                default:
                    return new ProjectVersionRequest(args);
                case FunctionTypes.Web:
                    return new ProjectVersionRequest(args, true);
                case FunctionTypes.Nuget:
                    return new NugetPublishRequest(args);
                case FunctionTypes.Zip:
                    return new ZipRequest(args);
                case FunctionTypes.Copy:
                    return new CopyRequest(args);
                case FunctionTypes.SqlRestore:
                    return new SqlRestoreRequest(args);
                case FunctionTypes.SqlQuery:
                    return new SqlQueryRequest(args);
            }

        }

        public void GotoColumn(int column)
        {
            int current = Console.CursorLeft;
            int dest = column * 8;

            if (dest > current)
            {
                double tabs_dec = ((double)(dest - current) / (double)8);
                int tabs = (int)tabs_dec;
                if (tabs < tabs_dec)
                    tabs += 1;
                string t = "";
                for (var i = 0; i < tabs; i++)
                {
                    t += "\t";
                }
                Console.Write(t);
            }
        }

        public IFileHandler GetHandler(string nugetPath, bool isFile = false)
        {
            bool isFtp = nugetPath.StartsWith("ftp:");
            if (FTPClient.IsFTPString(nugetPath))
                return new FtpFileHandler(nugetPath, isFile);
            else
                return new DefaultFileHandler(nugetPath, isFile);
        }
    }
}
