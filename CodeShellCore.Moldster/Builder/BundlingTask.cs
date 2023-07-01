using CodeShellCore.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Builder
{
    public class BundlingTask
    {
        private static List<BundlingTask> Tasks = new List<BundlingTask>();
        private static long Ids = 1;

        public long Id { get; private set; }
        public string TenantCode { get; set; }
        public string Version { get; set; }
        public string Status { get; set; }
        public string Environment { get; set; }
        [IgnoreDataMember]
        public string ConnectionId { get; set; }
        public DateTime StartedOn { get; set; }
        public DateTime? CompletedOn { get; set; }
        public string Message { get; set; }
        [IgnoreDataMember]
        public EventHandler<Result> OnComplete;

        [IgnoreDataMember]
        public Task<Result> Task { get; set; }
        public BundlingTask()
        {
            Id = Ids++;
        }

        public static BundlingTask GetTask(string tenantId, string version)
        {
            return Tasks.Where(d => d.TenantCode == tenantId && d.Version == version).FirstOrDefault();
        }

        public static void Add(BundlingTask task)
        {
            Tasks.Add(task);
        }

        public static void ClearCompleted()
        {
            var ts = new List<BundlingTask>();
            foreach (var t in Tasks)
            {
                if (!t.Task.IsCompleted && !t.Task.IsCanceled)
                {
                    ts.Add(t);
                }
            }
            Tasks = ts;
        }
    }
}
