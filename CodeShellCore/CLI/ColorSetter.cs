using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Cli
{
    public class ColorSetter : IDisposable
    {
        public bool IsDisposed { get; private set; }
        private ConsoleColor _fallBack;
        public ConsoleColor CurrentColor { get; private set; }
        public ColorSetter(ConsoleColor color)
        {
            _fallBack = Console.ForegroundColor;
            CurrentColor = color;
            Console.ForegroundColor = color;
        }

        public static ColorSetter Set(ConsoleColor color)
        {
            return new ColorSetter(color);
        }

        public void Dispose()
        {
            Console.ForegroundColor = _fallBack;
            IsDisposed = true;
        }

        public static ColorSetter NumberGradient(long number)
        {
            var color = ConsoleColor.Gray;
            if (number == 0)
                color = ConsoleColor.White;
            else if (number < 10)
                color = ConsoleColor.Cyan;
            else
                color = ConsoleColor.Yellow;

            return new ColorSetter(color);
        }
    }
}
