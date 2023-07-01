using CodeShellCore.Linq;
using CodeShellCore.Moldster.Environments.Services;
using CodeShellCore.Moldster.Sql;
using CodeShellCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeShellCore.Moldster.Environments
{
    public class EnvironmentService : StandAloneService, IEnvironmentsService
    {
        private readonly IPathsService paths;

        public EnvironmentService(IServiceProvider provider, IPathsService paths) : base(provider)
        {
            this.paths = paths;
        }

        public MoldsterEnvironment Post(MoldsterEnvironment dto)
        {
            return Put(dto);
        }

        public void Delete(string name)
        {
            var envs = paths.GetEnvironments();
            var env = envs.Where(e => e.Name == name).FirstOrDefault();
            if (env != null)
            {
                envs.Remove(env);
                paths.UpdateEnvironments(envs);
            }
        }

        public LoadResult<MoldsterEnvironment> Get()
        {
            var envs = paths.GetEnvironments();
            return new LoadResult<MoldsterEnvironment> { List = envs, TotalCount = envs.Count };
        }

        public IEnumerable<string> GetDatabaseList(string name)
        {
            var envs = paths.GetEnvironments();
            var env = envs.Where(e => e.Name == name).FirstOrDefault();
            if (env == null)
            {
                throw new Exception("Environment does not exist : " + name);
            }
            var acc = GetService<EnvironmentAccessor>();
            acc.CurrentEnvironment = env;
            var s = GetService<ISqlCommandService>();
            try
            {
                var dbs = s.GetDatabaseList();
                return dbs;
            }
            catch
            {
                return new List<string>();
            }

        }

        public MoldsterEnvironment Put(MoldsterEnvironment dto)
        {
            if (string.IsNullOrEmpty(dto.Name))
                return dto;
            var envs = paths.GetEnvironments();

            if (!envs.Any(e => e.Name == dto.Name))
            {
                envs.Add(dto);
            }
            else
            {
                for (var i = 0; i < envs.Count; i++)
                {
                    if (envs[i].Name == dto.Name)
                    {
                        envs[i] = dto;
                    }
                }
            }
            paths.UpdateEnvironments(envs);
            return dto;
        }


    }
}
