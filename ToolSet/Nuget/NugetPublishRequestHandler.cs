﻿using CodeShellCore.Cli.Routing;
using CodeShellCore.Helpers;
using System;
using System.IO;
using System.Threading.Tasks;
using ToolSet.Versions;

namespace ToolSet.Nuget
{
    public class NugetPublishRequestHandler : CliRequestHandler<NugetPublishRequest>
    {
        public NugetPublishRequestHandler(IServiceProvider provider) : base(provider)
        {
        }

        protected override void Build(ICliRequestBuilder<NugetPublishRequest> builder)
        {
            builder.FillProperty(e => e.MainDirectory, 's', "source", true);
            builder.FillProperty(e => e.NugetPath, 't', "target", true);
        }

        public string[] GetHelp()
        {
            return new string[] {
                "[nuget server path]",
                "ftp:[ftp user]/[ftp password]@[server name]:[ftpport]::[A:active,P:passive]::[target folder in ftp server]",
                "Finds nuget packages in working directory and copies them to nuget server path"
            };
        }

        public IToolSetFileHandler GetHandler(string nugetPath, bool isFile = false)
        {
            bool isFtp = nugetPath.StartsWith("ftp:");
            if (isFtp)
                return new FtpFileHandler(nugetPath, isFile);
            else
                return new DefaultFileHandler(nugetPath, isFile);
        }

        protected override Task<Result> HandleAsync(NugetPublishRequest request)
        {
            string[] files = Directory.GetFiles(request.MainDirectory, "*.csproj", SearchOption.AllDirectories);

            IToolSetFileHandler handler = GetHandler(request.NugetPath);

            foreach (var project in files)
            {
                ProjectFile f = new ProjectFile(project, new PhysicalFileReader());
                string version = f.GetVersion(4);
                string[] nugets = Directory.GetFiles(f.Folder, "*." + version + ".nupkg", SearchOption.AllDirectories);

                if (nugets.Length > 0)
                {
                    Console.Write(f.ProjectName + "-v" + version);
                    GotoColumn(6);

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
            return Task.FromResult(new Result());
        }
    }
}