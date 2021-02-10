using CodeShellCore.Cli;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.CLI
{
    public interface IOutputWriter
    {
        void WriteLine(bool replaceLast = false);
        void Write(string v, bool replaceLast = false);
        ColorSetter Set(ConsoleColor yellow);
        void WriteLine(string v, bool replaceLast = false);
        void GotoColumn(int column);
    }
}
