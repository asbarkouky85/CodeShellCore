using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeShellCore.Data.EntityFramework;
using CodeShellCore.Helpers;
using Microsoft.EntityFrameworkCore;

namespace CodeShellCore.Data.CustomFields
{
    public class CustomFieldRepository<T, TContext> :
        Repository_Int64<T, TContext>,
        ICustomFieldRepository where TContext : DbContext
        where T : class, ICustomField, IEntity<long>
    {
        public CustomFieldRepository(TContext con) : base(con)
        {

        }
        public virtual async Task<Dictionary<string, string>> LoadFor<TEntity>(long id)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            string type = typeof(TEntity).Name;
            var list = await Loader.Where(c => c.EntityId == id && c.EntityType == type).ToListAsync();
            foreach (var item in list)
            {
                result.Add(item.Name, item.Value);
            }
            return result;
        }

        public virtual async Task ReplaceFor<T1>(long id, Dictionary<string, string> data)
        {

            string t = typeof(T1).Name;
            await Delete(d => d.EntityId == id && d.EntityType == t);
            await SaveFor<T1>(id, data);

        }

        public virtual Task SaveFor<T1>(long id, Dictionary<string, string> dic)
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
            return Task.CompletedTask;
        }
    }
}
