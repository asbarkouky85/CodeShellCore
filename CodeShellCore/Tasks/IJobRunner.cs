using System.Timers;

namespace CodeShellCore.Tasks
{
    public interface IJobRunner
    {
        Timer Timer { get; }
        ITimedJob Job { get; set; }
    }
}