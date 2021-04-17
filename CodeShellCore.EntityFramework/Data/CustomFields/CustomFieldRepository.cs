using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeShellCore.Data.EntityFramework;
using CodeShellCore.Helpers;
using Microsoft.EntityFrameworkCore;

namespace CodeShellCore.Data.CustomFields
{
    public class CustomFieldRepository<T, TContext> :
        Repository_Int64<T, TContext>,
        ICustomFieldRepository where TContext : DbContext
        where T : class, ICustomField, IModel<long>
    {
        public CustomFieldRepository(TContext con) : base(con)
        {

        }
        public virtual Dictionary<string, string> LoadFor<T1>(long id)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            string type = typeof(T1).Name;
            var list = Loader.Where(c => c.EntityId == id && c.EntityType == type).ToList();
            foreach (var item in list)
            {
                result.Add(item.Name, item.Value);
            }
            return result;
        }

        public virtual void ReplaceFor<T1>(long id, Dictionary<string, string> data)
        {
            string t = typeof(T1).Name;
            Delete(d => d.EntityId == id && d.EntityType == t);
            SaveFor<T1>(id, data);
        }

        public virtual void SaveFor<T1>(long id, Dictionary<string, string> dic)
        {
            string type = typeof(T1).Name;
            foreach (KeyValuePair<string, string> entry in dic)
            {
                if (string.IsNullOrEmpty(entry.Value))
                    continue;
                var obj = Activator.CreateInstance<T>();
                obj.EntityId = id;
                obj.EntityType = type;
                obj.Name = entry.Key;
                obj.Value = entry.Value;
                obj.Id = Utils.GenerateID();
                Add(obj);
            }
        }
    }
}
