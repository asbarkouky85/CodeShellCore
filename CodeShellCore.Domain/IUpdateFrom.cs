using CodeShellCore.Data;

namespace CodeShellCore
{
    public interface IUpdateFrom<TEntity, TPrime> : IEntity<TPrime>
    {
        void UpdateFrom(TEntity entity);
    }
}