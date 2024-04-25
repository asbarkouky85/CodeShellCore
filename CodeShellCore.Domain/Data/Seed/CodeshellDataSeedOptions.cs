using CodeShellCore.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeShellCore.Data.Seed
{
    public class CodeshellDataSeedOptions
    {
        public IReadOnlyList<Type> DataSeeders => _seeders.AsReadOnly();
        private List<Type> _seeders = new List<Type>();
        public CodeshellDataSeedOptions()
        {

        }

        public void AddDataSeedContributor<T>() where T : class, IDataSeedContributor
        {
            _seeders.Add(typeof(T));
        }

        public void AddDataSeedContributor(Type t)
        {
            if (!t.Implements(typeof(IDataSeedContributor)))
                throw new ArgumentException($"Type {t.Name} does not implement {typeof(IDataSeedContributor).Name}");
            _seeders.Add(t);
        }
    }
}
