namespace CodeShellCore.Tasks
{
    public struct TimeOfDay
    {
        public int Hour;
        public int Minute;
        public int Second;

        public TimeOfDay(int hour, int minute, int second)
        {
            Hour = hour;
            Minute = minute;
            Second = second;
        }
    }
}
