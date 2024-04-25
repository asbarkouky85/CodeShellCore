using CodeShellCore.Files;
using System.Collections.Generic;

namespace CodeShellCore.FileServer
{
    public class Dimension : IDimension
    {
        public int Width { get; protected set; }
        public int Height { get; protected set; }

        public Dimension(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public int CompareTo(IDimension other)
        {
            if (Width > other.Width || Height > other.Height)
                return -1;
            else if (Width == other.Width && Height == other.Height)
                return 0;
            else
                return 1;
        }

    }
}
