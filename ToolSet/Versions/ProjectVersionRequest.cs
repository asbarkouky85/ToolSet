using System;
using System.IO;

namespace ToolSet.Versions
{
    public class ProjectVersionRequest : RequestBase
    {
        public string Project { get; set; }
        public string ShortVersion { get; set; }
        public string LongVersion { get; set; }
        public bool IsWeb { get; set; }
        public string PublishProfile { get; set; }
        public ProjectVersionRequest(string[] args, bool isWeb = false) : base(args)
        {
            IsWeb = isWeb;
        }

        public override void ProcessArgs()
        {
            string versionArg = "";
            if (IsWeb)
            {
                if (Arguments.Length < 4)
                    throw new NotEnoughArgumentsException();
                PublishProfile = Arguments[3];
            }
            else if (Arguments.Length < 3)
            {
                throw new NotEnoughArgumentsException();
            }

            versionArg = Arguments[2];
            Project = Arguments[1];
            if (IsWeb)
                PublishProfile = Arguments[3];

            ShortVersion = ToShortVersion(versionArg);
            LongVersion = ToLongVersion(versionArg);
        }

        public override void Execute(Dispatcher service)
        {
            Console.Write("Altering version for Project " + Project);

            var search = MainDirectory;
            if (Directory.Exists(Path.Combine(MainDirectory, Project)))
                search = Path.Combine(MainDirectory, Project);
            string[] files = Directory.GetFiles(search, "*" + Project + ".csproj", SearchOption.AllDirectories);
            foreach (string path in files)
            {
                ProjectFile f = new ProjectFile(path, new PhysicalFileReader());
                f.SetVersion(this);
                f.Save();
            }
            service.GotoColumn(7);
            using (ColorSetter.Set(ConsoleColor.Cyan))
            {
                Console.Write("-> v" + LongVersion + "\t");
            }
            using (ColorSetter.Set(ConsoleColor.Green))
            {
                Console.Write("SUCCESS");
            }

            Console.WriteLine();
        }

        public override string[] GetHelp()
        {
            return new string[] {
                "[project name] [version (x.x.x.x)]",
                "Modifies .csproj with project name and sets its version"
            };
        }
    }
}
