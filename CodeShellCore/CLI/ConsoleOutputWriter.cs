using CodeShellCore.Cli;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Cli
{
    public class ConsoleOutputWriter : IOutputWriter
    {
        public bool Suspend { get; set; }

        private bool isConsole
        {
            get
            {
                try
                {
                    var n = Console.CursorLeft;
                    return true;
                }
                catch
                {
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

        public ColorSetter Set(ConsoleColor yellow)
        {
            return ColorSetter.Set(yellow);
        }

        public void Write(string v, bool replaceLast = false)
        {
            if (replaceLast)
                Console.CursorTop -= 1;
            Console.Write(v);
        }

        public void WriteLine(bool replaceLast = false)
        {
            if (replaceLast)
                Console.CursorTop -= 1;
            Console.WriteLine();
        }

        public void WriteLine(string v, bool replaceLast = false)
        {
            if (replaceLast)
                Console.CursorTop -= 1;
            Console.WriteLine(v);
        }
    }
}
