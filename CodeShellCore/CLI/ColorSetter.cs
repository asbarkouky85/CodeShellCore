using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Cli
{
    public class ColorSetter : IDisposable
    {
        ConsoleColor _current;
        private ColorSetter(ConsoleColor color)
        {
            _current = Console.ForegroundColor;
            Console.ForegroundColor = color;
        }

        public static ColorSetter Set(ConsoleColor color)
        {
            return new ColorSetter(color);
        }

        public void Dispose()
        {
            Console.ForegroundColor = _current;
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
