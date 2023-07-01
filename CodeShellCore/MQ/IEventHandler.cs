using System.Threading.Tasks;

namespace CodeShellCore.MQ
{
    public interface IEventHandler
    {

        Task Handle<T>(T item) where T : class;
    }
}
