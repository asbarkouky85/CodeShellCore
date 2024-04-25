using CodeShellCore.Data;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Mapping;
using CodeShellCore.Data.Services;
using CodeShellCore.Linq;
using CodeShellCore.MQ.Events;
using CodeShellCore.Services;

namespace CodeShellCore.Data.Events
{
    public class CrudEvent<T> : CrudEventBase where T : class
    {
        public T Data { get; private set; }

        private CrudEvent()
        {

        }

        public CrudEvent(T data, ActionType type, long? tenant = null)
        {
            Data = data;
            Type = type;
            TenantId = tenant;
        }

        public CrudEvent<TObject> GetEventFor<TObject>(bool ignorId = false) where TObject : class
        {
            TObject ob = Data.MapTo<TObject>(ignorId);
            CrudEvent<TObject> ev = new CrudEvent<TObject>(ob, Type, TenantId);
            return ev;
        }

        public CrudEvent<TObject> GetEventFor<TObject>(IObjectMapper mapper, bool ignorId = false) where TObject : class
        {
            TObject ob = mapper.Map<T, TObject>(Data);
            CrudEvent<TObject> ev = new CrudEvent<TObject>(ob, Type, TenantId);
            return ev;
        }

        public SubmitResult Apply(IEntityService<T> service)
        {

            SubmitResult res = new SubmitResult(0);
            switch (Type)
            {
                case ActionType.Add:
                    res = service.Create(Data);
                    break;
                case ActionType.Update:
                    res = service.Update(Data);
                    break;
                case ActionType.Delete:
                    res = service.Delete(Data);
                    break;
            }
            return res;
        }
    }
}
