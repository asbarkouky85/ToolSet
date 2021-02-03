using System;
using System.Collections.Generic;
using System.Reflection;
using System.Diagnostics;

namespace ToolSet
{
    public class ConsoleService
    {


        public ConsoleService()
        {

        }
        public virtual int SuccessCol { get { return 8; } }
        public void WriteSuccess(TimeSpan? time = null, int column = 0)
        {
            column = column == 0 ? SuccessCol : column;
            string elapsed = time.HasValue ? " " + time.Value.TotalSeconds.ToString("F4") + "s" : "";
            using (ColorSetter.Set(ConsoleColor.Green))
            {
                GotoColumn(column);
                Console.Write("SUCCESS" + elapsed);
            }

        }

        protected void WriteFileOperation(string processName, string fileName, bool lineAfter = true)
        {
            Console.Write($"{processName} [");

            WriteColored($"{fileName}", ConsoleColor.Yellow);
            Console.Write("]...");
            if (lineAfter)
                Console.WriteLine();
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

        public void WriteWarning(string content)
        {
            using (ColorSetter.Set(ConsoleColor.Yellow))
            {
                Console.WriteLine(content);
            }
        }

        public void WriteFailed(TimeSpan? time = null, Result res = null, bool lineAfter = false)
        {
            string elapsed = time.HasValue ? " - " + time.Value.TotalSeconds.ToString("F4") + "s" : "";
            using (ColorSetter.Set(ConsoleColor.Red))
            {
                Console.Write("FAILED" + elapsed);
                if (res != null)
                {
                    Console.WriteLine(res.Message);
                    Console.WriteLine(res.ExceptionMessage);
                }
            }
            if (res?.StackTrace != null)
            {
                using (ColorSetter.Set(ConsoleColor.DarkRed))
                {
                    foreach (var f in res.StackTrace)
                        Console.WriteLine(f);
                }
            }
            if (lineAfter)
                Console.WriteLine();
        }


        private void WriteResult(Result res)
        {
            WriteException(res.ExceptionMessage, res.Message, res.StackTrace);
        }

        public void WriteException(string message, string type, string[] stack)
        {
            using (ColorSetter.Set(ConsoleColor.Red))
            {
                Console.WriteLine();
                Console.WriteLine("Exception Type :\t" + type);
                Console.WriteLine("Exception Message :\t" + message);
            }
            Console.WriteLine();
            if (stack != null)
            {
                using (ColorSetter.Set(ConsoleColor.DarkRed))
                {
                    foreach (string st in stack)
                        Console.WriteLine(st.Trim());
                }
                Console.WriteLine();
            }

        }

        public void WriteExceptionShort(string message, string type)
        {
            Console.WriteLine();
            Console.WriteLine("(" + type + ") :\t" + message);
            Console.WriteLine();

        }

        public Process GetCommandProcess(string folder, string command, string arguments, bool useShell = false)
        {
            ProcessStartInfo inf = new ProcessStartInfo
            {
                WorkingDirectory = folder,
                FileName = command,
                UseShellExecute = useShell
            };


            if (arguments != null)
                inf.Arguments = arguments;
            Console.WriteLine();
            using (ColorSetter.Set(ConsoleColor.Cyan))
            {
                Console.Write("Executing : ");
            }
            Console.WriteLine($"{command} {arguments}");

            var p = new Process();
            p.StartInfo = inf;
            return p;

        }



        public void WriteColored(string text, ConsoleColor color)
        {
            using (ColorSetter.Set(color))
            {
                Console.Write(text);
            }
        }

        public void WriteException(Exception ex, bool full = true)
        {
            if (ex is TargetInvocationException)
            {
                WriteException(ex.InnerException);
                return;
            }



            string[] lines = ex.StackTrace?.Split(new char[] { '\n' });
            if (full)
                WriteException(ex.Message, ex.GetType().Name, lines);
            else
                WriteExceptionShort(ex.Message, ex.GetType().Name);

            if (ex.InnerException != null)
            {
                Console.WriteLine();
                WriteException(ex.InnerException, full);
            }
            else
            {
                Console.WriteLine("-----------------------------------");
            }


        }

        public string[] GetCommandOutput(string folder, string file, string args, out int exitCode)
        {
            var cmd = GetCommandProcess(folder, file, args);
            cmd.StartInfo.RedirectStandardOutput = true;
            List<string> st = new List<string>();
            cmd.Start();
            while (!cmd.StandardOutput.EndOfStream)
            {
                st.Add(cmd.StandardOutput.ReadLine());
            }
            cmd.WaitForExit();
            exitCode = cmd.ExitCode;
            return st.ToArray();
        }

        public int RunCommand(string folder, string command, string arguments = null, bool useShell = false)
        {

            var p = GetCommandProcess(folder, command, arguments, useShell);
            p.Start();
            p.WaitForExit();
            Console.WriteLine();
            WriteSuccess();
            Console.WriteLine();
            return p.ExitCode;

        }
    }
}
