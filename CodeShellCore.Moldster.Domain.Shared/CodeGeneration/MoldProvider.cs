using System.Resources;
using System.Text;

namespace CodeShellCore.Moldster.CodeGeneration
{
    public abstract class MoldProvider
    {
        protected abstract ResourceManager ResourceManager { get; }

        public virtual byte[] GetResourceByNameAsBytes(string name)
        {
            return (byte[])ResourceManager.GetObject(name);
        }

        public virtual string GetResourceByNameAsString(string name)
        {
            var ob = ResourceManager.GetObject(name);
            if (ob.GetType() == typeof(string))
                return (string)ob;
            else if (ob.GetType() == typeof(byte[]))
            {
                var str = Encoding.UTF8.GetString((byte[])ob);
                return str;
            }
            return null;
        }

        public virtual string GetBaseComponentMold(bool serviced)
        {
            return serviced ? GetResourceByNameAsString(MoldNames.ServicedBaseComponent_ts) : GetResourceByNameAsString(MoldNames.BaseComponent_ts);
        }

        public virtual string GetDomainModuleMold()
        {
            return GetResourceByNameAsString(MoldNames.DomainModule_ts);
        }
    }
}
