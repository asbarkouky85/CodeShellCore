using CodeShellCore.Data;

namespace CodeShellCore
{
    public class Entity<TPrime> : IEntity<TPrime>
    {
        public TPrime Id { get; set; }
    }
}
