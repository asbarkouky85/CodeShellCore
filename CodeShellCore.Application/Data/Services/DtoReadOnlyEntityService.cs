using CodeShellCore.Data.Lookups;
using CodeShellCore.Data.Mapping;
using CodeShellCore.Linq;
using CodeShellCore.Text;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using CodeShellCore.Data.Localization;
using CodeShellCore.Security;
using CodeShellCore.MultiTenant;
using CodeShellCore.Types;
using CodeShellCore.Files.Uploads;

namespace CodeShellCore.Data.Services
{
    public class DtoReadOnlyEntityService<T, TPrime, TOptionsDto, TListDto, TSingleDto> : IDtoReadOnlyEntityService<TPrime, TOptionsDto, TListDto, TSingleDto>
        where T : class, IEntity<TPrime>
        where TSingleDto : class
        where TListDto : class
        where TOptionsDto : LoadOptions
    {
        protected IUnitOfWork DefaultUnit { get; }
        protected IKeyRepository<T, TPrime> Repository { get; private set; }
        protected IObjectMapper Mapper { get; private set; }
        protected ILookupsService LookupsService { get; private set; }
        protected IUserAccessor UserAccessor { get; private set; }
        protected CurrentTenant CurrentTenant { get; private set; }
        private ILocalizationDataService _localizationDataService;
        protected InstanceStore Store { get; private set; }
        protected ILocalizationDataService LocalizationDataService
        {
            get
            {
                if (_localizationDataService != null)
                {
                    return _localizationDataService;
                }
                else
                {
                    _localizationDataService = DefaultUnit.ServiceProvider.GetRequiredService<ILocalizationDataService>();
                    return _localizationDataService;
                }
            }
        }

        public DtoReadOnlyEntityService(IUnitOfWork unit)
        {
            DefaultUnit = unit;
            Repository = unit.GetRepositoryFor<T, TPrime>();
            Mapper = unit.ServiceProvider.GetService<IObjectMapper>();
            LookupsService = unit.ServiceProvider.GetService<ILookupsService>();
            UserAccessor = unit.ServiceProvider.GetService<IUserAccessor>();
            CurrentTenant = unit.ServiceProvider.GetService<CurrentTenant>();
            Store = new InstanceStore(() => unit.ServiceProvider);
        }

        public virtual LoadResult<TListDto> Get(TOptionsDto options)
        {
            var mapped = options.GetOptionsFor<TListDto>();
            return Repository.FindAndMap(mapped);
        }

        public virtual LoadResult<TListDto> GetCollection(string id, TOptionsDto options)
        {
            var mapped = options.GetOptionsFor<TListDto>();
            return DefaultUnit.GetCollectionRepositoryFor<T>().LoadCollectionAndMap(id, mapped);
        }

        public virtual Dictionary<string, IEnumerable<Named<object>>> GetEditLookups(Dictionary<string, string> dto)
        {
            return LookupsService.GetRequestedLookups(dto);
        }

        public virtual Dictionary<string, IEnumerable<Named<object>>> GetListLookups(Dictionary<string, string> dto)
        {
            return LookupsService.GetRequestedLookups(dto);
        }

        public virtual TSingleDto GetSingle(TPrime id)
        {
            var item = GetSingleById(id);
            return Mapper.Map<T, TSingleDto>(item);
        }

        public virtual bool IsUnique(IsUniqueDto dto)
        {
            var id = dto.Id.ConvertTo<TPrime>();
            var exp = Expressions.Unique<T, TPrime>(id, dto.Property, dto.Value);
            return !Repository.Exist(exp);
        }

        protected virtual T GetSingleById(TPrime id)
        {
            return Repository.FindSingleById(id);
        }
    }
}
