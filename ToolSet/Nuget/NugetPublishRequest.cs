using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolSet.Versions;

namespace ToolSet.Nuget
{
    public class NugetPublishRequest : RequestBase
    {

        public string NugetPath { get; set; }

        public NugetPublishRequest(string[] args) : base(args)
        {
        }

        public override void ProcessArgs()
        {
            if (Arguments.Length < 2)
                throw new NotEnoughArgumentsException();
            NugetPath = Arguments[1];
        }

        public override void Execute(Dispatcher service)
        {
            string[] files = Directory.GetFiles(MainDirectory, "*.csproj", SearchOption.AllDirectories);

            IFileHandler handler = service.GetHandler(NugetPath);

            foreach (var project in files)
            {
                ProjectFile f = new ProjectFile(project, new PhysicalFileReader());
                string version = f.GetVersion(4);
                string[] nugets = Directory.GetFiles(f.Folder, "*." + version + ".nupkg", SearchOption.AllDirectories);

                if (nugets.Length > 0)
                {
                    Console.Write(f.ProjectName + "-v" + version);
                    service.GotoColumn(6);

                    if (handler.UploadPackage(f.ProjectName, version, nugets[0]))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine(" Added");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.WriteLine(" Already exists");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                }

            }
        }

        public override string[] GetHelp()
        {
            return new string[] {
                "[nuget server path]",
                "ftp:[ftp user]/[ftp password]@[server name]:[ftpport]::[A:active,P:passive]::[target folder in ftp server]",
                "Finds nuget packages in working directory and copies them to nuget server path"
            }; 
        }
    }
}
