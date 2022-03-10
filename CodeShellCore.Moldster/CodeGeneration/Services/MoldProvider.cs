using System.IO;
using System.Resources;

namespace CodeShellCore.Moldster.CodeGeneration.Services
{
    public abstract class MoldProvider
    {
        protected abstract ResourceManager ResourceManager { get; }

        public byte[] GetResourceByNameAsBytes(string name)
        {
            var str = ResourceManager.GetStream(name);
            using (var mem = new MemoryStream())
            {
                str.CopyTo(mem);
                return mem.ToArray();
            }

        }

        public string GetResourceByNameAsString(string name)
        {
            return ResourceManager.GetString(name);
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
