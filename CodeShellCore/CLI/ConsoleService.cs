using CodeShellCore.Http;
using CodeShellCore.Text;
using CodeShellCore.Services;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Diagnostics;
using CodeShellCore.Helpers;
using CodeShellCore.CLI;

namespace CodeShellCore.Cli
{
    public class ConsoleService : ServiceBase
    {
        protected  IOutputWriter Out;

        public ConsoleService(IOutputWriter writer)
        {
            Out = writer;
        }
        public virtual int SuccessCol { get { return 8; } }
        public void WriteSuccess(TimeSpan? time = null, int column = 0)
        {
            column = column == 0 ? SuccessCol : column;
            string elapsed = time.HasValue ? " " + time.Value.TotalSeconds.ToString("F4") + "s" : "";
            using (Out.Set(ConsoleColor.Green))
            {
                GotoColumn(column);
                Out.Write("SUCCESS" + elapsed);
            }

        }

        protected void WriteFileOperation(string processName, string fileName, bool lineAfter = true)
        {
            Out.Write($"{processName} [");

            WriteColored($"{fileName}", ConsoleColor.Yellow);
            Out.Write("]...");
            if (lineAfter)
                Out.WriteLine();
        }

        public void GotoColumn(int column)
        {
            Out.GotoColumn(column);
        }

        public void WriteWarning(string content)
        {
            using (Out.Set(ConsoleColor.Yellow))
            {
                Out.WriteLine(content);
            }
        }

        public void WriteFailed(TimeSpan? time = null, Result res = null, bool lineAfter = false)
        {
            string elapsed = time.HasValue ? " - " + time.Value.TotalSeconds.ToString("F4") + "s" : "";
            using (Out.Set(ConsoleColor.Red))
            {
                Out.Write("FAILED" + elapsed);
                if (res != null)
                {
                    Out.WriteLine(res.Message);
                    Out.WriteLine(res.ExceptionMessage);
                }
            }
            if (res?.StackTrace != null)
            {
                using (Out.Set(ConsoleColor.DarkRed))
                {
                    foreach (var f in res.StackTrace)
                        Out.WriteLine(f);
                }
            }
            if (lineAfter)
                Out.WriteLine();
        }


        private void WriteResult(Result res)
        {
            WriteException(res.ExceptionMessage, res.Message, res.StackTrace);
            if (res.InnerResult != null)
            {
                WriteResult(res.InnerResult);
            }
        }

        public void WriteException(string message, string type, string[] stack)
        {
            using (Out.Set(ConsoleColor.Red))
            {
                Out.WriteLine();
                Out.WriteLine("Exception Type :\t" + type);
                Out.WriteLine("Exception Message :\t" + message);
            }
            Out.WriteLine();
            if (stack != null)
            {
                using (Out.Set(ConsoleColor.DarkRed))
                {
                    foreach (string st in stack)
                        Out.WriteLine(st.Trim());
                }
                Out.WriteLine();
            }

        }

        public void WriteExceptionShort(string message, string type)
        {
            Out.WriteLine();
            Out.WriteLine("(" + type + ") :\t" + message);
            Out.WriteLine();

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
            Out.WriteLine();
            using (Out.Set(ConsoleColor.Cyan))
            {
                Out.Write("Executing : ");
            }
            Out.WriteLine($"{command} {arguments}");

            var p = new Process();
            p.StartInfo = inf;
            return p;
            
        }

        

        public void WriteColored(string text, ConsoleColor color)
        {
            using (Out.Set(color))
            {
                Out.Write(text);
            }
        }

        public void WriteException(Exception ex, bool full = true)
        {
            if (ex is TargetInvocationException)
            {
                WriteException(ex.InnerException);
                return;
            }


            if (ex is CodeShellHttpException && ex.Message.TryRead(out HttpResult res))
            {
                WriteResult(res);
            }
            else
            {
                string[] lines = ex.StackTrace?.Split(new char[] { '\n' });
                if (full)
                    WriteException(ex.Message, ex.GetType().Name, lines);
                else
                    WriteExceptionShort(ex.Message, ex.GetType().Name);

                if (ex.InnerException != null)
                {
                    Out.WriteLine();
                    WriteException(ex.InnerException, full);
                }
                else
                {
                    Out.WriteLine("-----------------------------------");
                }
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
            using (var x = SW.Measure())
            {
                var p = GetCommandProcess(folder, command, arguments, useShell);
                p.Start();
                p.WaitForExit();
                Out.WriteLine();
                WriteSuccess(x.Elapsed);
                Out.WriteLine();
                return p.ExitCode;
            }
        }
    }
}
