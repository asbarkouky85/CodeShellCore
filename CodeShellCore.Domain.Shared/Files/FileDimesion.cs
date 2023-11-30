using System;

namespace CodeShellCore.Files
{
    public class FileDimesion : IDimension
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public int CompareTo(IDimension other)
        {
            if (Width > other.Width || Height > other.Height)
                return -1;
            else if (Width == other.Width && Height == other.Height)
                return 0;
            else
                return 1;
        }

        public static bool operator >(FileDimesion a, IDimension b) => a.CompareTo(b) < 0;
        public static bool operator <(FileDimesion a, IDimension b) => a.CompareTo(b) > 0;

        public static bool operator ==(FileDimesion a, IDimension b) => a.CompareTo(b) == 0;
        public static bool operator !=(FileDimesion a, IDimension b) => a.CompareTo(b) != 0;

        public override bool Equals(object obj)
        {
            return this == obj;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
