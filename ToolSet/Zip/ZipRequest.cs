using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolSet.Zip
{
    public class ZipRequest : RequestBase
    {
        public string TargetLocation { get; set; }
        public string FolderLocation { get; set; }
        public string Pattern { get; set; }
        public ZipRequest(string[] args) : base(args)
        {
        }

        public override void Execute(Dispatcher service)
        {
            if (!Directory.Exists(FolderLocation))
                throw new DirectoryNotFoundException(FolderLocation);
            Console.Write("Compressing '" + FolderLocation + "' to '" + TargetLocation + "'...");
            if (File.Exists(TargetLocation))
                File.Delete(TargetLocation);
            ZipFile.CreateFromDirectory(FolderLocation, TargetLocation, CompressionLevel.Optimal, false);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Success");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public override void ProcessArgs()
        {
            if (Arguments.Length < 3)
            {
                throw new NotEnoughArgumentsException();
            }
            TargetLocation = Arguments[2];
            FolderLocation = Arguments[1];
        }

        public override string[] GetHelp()
        {
            return new string[] {

                "[folder location] [target zip file path]",
                "Compresses folder to location"
            };
        }
    }
}
