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

        List<string> _partitionPath(string path)
        {
            string[] spl = path.Split('/');
            List<string> lst = new List<string>();
            foreach (var s in spl)
            {
                if (!string.IsNullOrEmpty(s))
                    lst.Add(s.Trim());
            }
            return lst;
        }

        /// <summary>
        /// id is returned in the data dictionary as LastId
        /// </summary>
        /// <param name="dom"></param>
        /// <returns></returns>
        public SubmitResult CreatePathAndGetId(string dom)
        {
            if (dom[0] != '/')
                dom = "/" + dom;
            var parts = _partitionPath(dom);
            string searchTerm = "";
            List<Domain> domains = new List<Domain>();
            long? lastId = null;
            foreach (var part in parts)
            {
                searchTerm += "/" + part;
                Domain domain = Repository.FindSingle(d => d.NameChain == searchTerm);
                if (domain == null)
                {
                    domain = new Domain
                    {
                        Id = Utils.GenerateID(),
                        Name = part,
                        ParentId = lastId
                    };
                    
                    Repository.Add(domain);
                }
                lastId = domain.Id;
            }
            var res= Unit.SaveChanges();
            res.Data["LastId"] = lastId;
            return res;
        }
    }
}
