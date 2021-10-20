using CodeShellCore.Cli;
using CodeShellCore.Cli.Routing;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ToolSet.Versions
{
    public class ProjectVersionRequestHandler : CliRequestHandler<ProjectVersionRequest>
    {
        public ProjectVersionRequestHandler(IServiceProvider provider) : base(provider)
        {
        }

        protected override void Build(ICliRequestBuilder<ProjectVersionRequest> builder)
        {
            builder.FillProperty(e => e.MainDirectory, 'd', "folder", true);
            builder.FillProperty(e => e.Project, 'p', "project", true);
            builder.FillProperty(e => e.Version, 'v', "version", true);
            builder.FillProperty(e => e.IsWeb, 'w', "web");
            builder.FillProperty(e => e.PublishProfile, 'u', "publish-profile");
        }

        protected override Task<CodeShellCore.Helpers.Result> HandleAsync(ProjectVersionRequest request)
        {
            Console.Write("Altering version for Project " + request.Project);

            var search = request.MainDirectory;
            if (Directory.Exists(Path.Combine(request.MainDirectory, request.Project)))
                search = Path.Combine(request.MainDirectory, request.Project);
            string[] files = Directory.GetFiles(search, "*" + request.Project + ".csproj", SearchOption.AllDirectories);
            foreach (string path in files)
            {
                ProjectFile f = new ProjectFile(path, new PhysicalFileReader());
                f.SetVersion(request);
                f.Save();
            }

            GotoColumn(7);
            using (ColorSetter.Set(ConsoleColor.Cyan))
            {
                Console.Write("-> v" + request.GetLongVersionString() + "\t");
            }

            using (ColorSetter.Set(ConsoleColor.Green))
            {
                Console.Write("SUCCESS");
            }

            Console.WriteLine();
            return Task.FromResult(new CodeShellCore.Helpers.Result());
        }
    }
}
