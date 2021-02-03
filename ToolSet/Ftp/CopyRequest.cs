using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolSet.Nuget;

namespace ToolSet.Ftp
{
    public class CopyRequest : RequestBase
    {
        public string FromPath { get; set; }
        public string ToPath { get; set; }
        public CopyRequest(string[] args) : base(args)
        {
        }

        public override void Execute(Dispatcher service)
        {
            IFileHandler fromHandler =service.GetHandler(FromPath, true);
            IFileHandler toHandler = service.GetHandler(ToPath);

            byte[] file = fromHandler.GetFile();
            string fileName = fromHandler.GetFileName();

            Console.Write("Copying " + fileName);
            if (!toHandler.SaveFile(fileName, file, out string message))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\tFailed : " + message);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("\tSuccess");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            Console.WriteLine();

        }

        public override void ProcessArgs()
        {
            if (Arguments.Length < 3)
            {
                throw new NotEnoughArgumentsException();
            }
            FromPath = Arguments[1];
            ToPath = Arguments[2];
        }

        public override string[] GetHelp()
        {
            return new string[] {
                "[file path (file system or ftpString)] [target directory path (file system or ftpString)]",
                "ftp:[ftp user]/[ftp password]@[server name]:[ftpport]::[A:active,P:passive]::[target folder in ftp server]",
                "Copies file from file path to target directory"
            };
        }
    }
}
