﻿using CodeShellCore.Cli.Routing;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolSet.Zip
{
    public class ZipRequestHandler : CliRequestHandler<ZipRequest>
    {
        public ZipRequestHandler(IServiceProvider provider) : base(provider)
        {
        }

        protected override void Build(ICliRequestBuilder<ZipRequest> builder)
        {
            builder.FillProperty(e => e.FolderLocation, 's', "source", true);
            builder.FillProperty(e => e.TargetLocation, 't', "target", true);
        }

        protected override Task<CodeShellCore.Helpers.Result> HandleAsync(ZipRequest request)
        {
            if (!Directory.Exists(request.FolderLocation))
                throw new DirectoryNotFoundException(request.FolderLocation);
            Console.Write("Compressing '" + request.FolderLocation + "' to '" + request.TargetLocation + "'...");
            if (File.Exists(request.TargetLocation))
                File.Delete(request.TargetLocation);
            ZipFile.CreateFromDirectory(request.FolderLocation, request.TargetLocation, CompressionLevel.Optimal, false);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Success");
            Console.ForegroundColor = ConsoleColor.Gray;
            return Task.FromResult(new CodeShellCore.Helpers.Result());
        }
    }
}