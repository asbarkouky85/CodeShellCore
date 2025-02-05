using System;

namespace CodeShellCore.Data.Auditing
{
    public interface IFullAudited
    {
        long? DeletedBy { get; }
        DateTime? DeletedOn { get; }
        bool IsDeleted { get; }
    }
}
