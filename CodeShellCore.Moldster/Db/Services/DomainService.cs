using CodeShellCore.Text;
using CodeShellCore.Helpers;
using System;
using System.Collections.Generic;
using System.Reflection;
using CodeShellCore.Data.Services;
using CodeShellCore.Moldster.Db.Data;
using CodeShellCore.Data.Helpers;

namespace CodeShellCore.Moldster.Db.Services
{
    public class DomainService : EntityService<Domain>
    {
        protected IConfigUnit Unit;
        private static Dictionary<Type, long> EntityIds;

        public DomainService(IConfigUnit unit) : base(unit)
        {
            Unit = unit;
        }

        public long GetEntityId<T>()
        {
            if (EntityIds == null)
                EntityIds = GetEntityIds();

            long id;
            if (EntityIds.TryGetValue(typeof(T), out id))
                return id;

            DomainEntity ent = GetEntity(typeof(T).FullName);
            EntityIds = null;
            return ent.Id;

        }

        public Dictionary<Type, long> GetEntityIds()
        {
            var d = Unit.DomainEntityRepository.FindAs(e => new
            {
                e.Name,
                e.Id
            });
            Assembly assem = Assembly.Load("FMS");

            Dictionary<Type, long> dic = new Dictionary<Type, long>();
            foreach (var x in d)
            {
                Type entityType = assem.GetType(x.Name);
                if (entityType != null)
                    dic[entityType] = x.Id;
            }

            return dic;
        }

        public long GetDomainId(string domain)
        {
            if (!Unit.DomainRepository.Exist(d => d.Name == domain))
            {
                Domain d = new Domain
                {
                    Name = domain
                };
                Unit.DomainRepository.Add(d);
                Unit.SaveChanges();
                return d.Id;
            }
            else
            {
                return Unit.DomainRepository.GetSingleValue(e => e.Id, d => d.Name == domain);
            }

        }

        public DomainEntity GetEntity(string fullName)
        {
            if (Unit.DomainEntityRepository.Exist(d => d.Name == fullName))
                return Unit.DomainEntityRepository.FindSingle(d => d.Name == fullName);

            DomainEntity ent = new DomainEntity
            {
                Name = fullName
            };
            string domain = fullName.GetBeforeLast(".").Replace("FMS.", "");


            ent.DomainId = GetDomainId(domain);

            Unit.DomainEntityRepository.Add(ent);
            Unit.SaveChanges();
            return ent;

        }

        

        
    }
}
