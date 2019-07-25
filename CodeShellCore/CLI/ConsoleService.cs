using CodeShellCore.Services.Http;
using CodeShellCore.Text;
using CodeShellCore.Services;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Diagnostics;
using CodeShellCore.Helpers;

namespace CodeShellCore.Cli
{
    public class ConsoleService : ServiceBase
    {
        public void WriteSuccess(TimeSpan? time = null, int column = 0)
        {
            string elapsed = time.HasValue ? " " + time.Value.TotalSeconds.ToString("F4") + "s" : "";
            Console.ForegroundColor = ConsoleColor.Green;
            if (column != 0)
                GotoColumn(column);
            Console.Write("SUCCESS" + elapsed);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        private bool isConsole
        {
            get
            {
                try
                {
                    var n = Console.CursorLeft;
                    return true;
                }
                catch {
                    return false;
                }
            }
        }

        public void GotoColumn(int column)
        {
            if (!isConsole)
                return;
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
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(content);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void WriteFailed(TimeSpan? time = null)
        {
            string elapsed = time.HasValue ? " - " + time.Value.TotalSeconds.ToString("F4") + "s" : "";
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("FAILED" + elapsed);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();
        }


        private void WriteResult(HttpResult res)
        {
            WriteException(res.ExceptionMessage, res.Message, res.StackTrace);
            if (res.InnerResult != null)
            {
                WriteResult(res.InnerResult);
            }
        }

        public void WriteException(string message, string type, string[] stack)
        {
            Console.WriteLine();
            Console.WriteLine("Exception Type :\t" + type);
            Console.WriteLine("Exception Message :\t" + message);
            Console.WriteLine();
            Console.WriteLine("Stack Trace : ");
            Console.WriteLine();

            if (stack != null)
            {
                foreach (string st in stack)
                    Console.WriteLine(st.Trim());
            }

        }

        public void WriteExceptionShort(string message, string type)
        {
            Console.WriteLine();
            Console.WriteLine("(" + type + ") :\t" + message);
            Console.WriteLine();

        }

        public void RunCommand(string folder, string command, string arguments = null)
        {
            using (var x = SW.Measure())
            {
                ProcessStartInfo inf = new ProcessStartInfo
                {
                    WorkingDirectory = folder,
                    FileName = command,

                };
                if (arguments != null)
                    inf.Arguments = arguments;
                Console.WriteLine();
                using (ColorSetter.Set(ConsoleColor.Cyan))
                {
                    Console.Write("Executing : ");
                }
                Console.Write($"{command} {arguments}");
                Console.WriteLine();
                using (ColorSetter.Set(ConsoleColor.Yellow))
                {
                    Console.WriteLine("----------------------------------------");
                }
                Process p = Process.Start(inf);
                p.WaitForExit();

                using (ColorSetter.Set(ConsoleColor.Yellow))
                {
                    Console.WriteLine("----------------------------------------");
                }
                WriteSuccess(x.Elapsed);
                Console.WriteLine();
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
                    Console.WriteLine();
                    WriteException(ex.InnerException, full);
                }
                else
                {
                    Console.WriteLine("-----------------------------------");
                }
            }

        }
    }
}
