using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Mapping;
using CodeShellCore.Data.Services;
using CodeShellCore.Linq;
using CodeShellCore.MQ.Events;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CodeShellCore.Data.Events
{
    public class CrudEvent<T> : CrudEventBase where T : class
    {
        public T Data { get; private set; }


        public CrudEvent()
        {

        }

        [JsonConstructor]
        public CrudEvent(T data, ActionType type, long? tenantId = null)
        {
            Data = data;
            Type = type;
            TenantId = tenantId;
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

        public async Task<SubmitResult> Apply(IEntityService<T> service)
        {

            SubmitResult res = new SubmitResult(0);
            switch (Type)
            {
                case ActionType.Add:
                    res = await service.Create(Data);
                    break;
                case ActionType.Update:
                    res = await service.Update(Data);
                    break;
                case ActionType.Delete:
                    res = await service.Delete(Data);
                    break;
            }
            return res;
        }
    }
}
