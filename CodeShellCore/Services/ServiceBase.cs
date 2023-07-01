namespace CodeShellCore.Services
{
    public abstract class ServiceBase : IServiceBase
    {
        protected bool isDisposed=false;
        public ServiceBase()
        {
         
        }

        public virtual void Dispose()
        {
            isDisposed = true;
        }
    }
}
