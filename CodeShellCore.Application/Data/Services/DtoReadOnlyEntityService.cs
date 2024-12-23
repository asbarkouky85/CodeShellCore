using CodeShellCore.Data.Localization;
using CodeShellCore.Data.Lookups;
using CodeShellCore.Data.Mapping;
using CodeShellCore.Linq;
using CodeShellCore.MultiTenant;
using CodeShellCore.Security;
using CodeShellCore.Text;
using CodeShellCore.Text.Localization;
using CodeShellCore.Types;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeShellCore.Data.Services
{
    public class DtoReadOnlyEntityService<T, TPrime, TOptionsDto, TListDto, TSingleDto> : IDtoReadOnlyEntityService<TPrime, TOptionsDto, TListDto, TSingleDto>
        where T : class, IEntity<TPrime>
        where TSingleDto : class
        where TListDto : class
        where TOptionsDto : PagedListRequestDto
    {
        protected IUnitOfWork DefaultUnit { get; }
        protected IKeyRepository<T, TPrime> Repository { get; private set; }
        protected IObjectMapper Mapper { get; private set; }
        protected ILookupsService LookupsService { get; private set; }
        protected IUserAccessor UserAccessor { get; private set; }
        protected Language Language => Store.GetRequiredService<Language>();
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

        public virtual async Task<PagedResult<TListDto>> Get(TOptionsDto options)
        {
            var mapped = options.GetOptionsFor<TListDto>();
            return await Repository.FindAndMap(mapped);
        }

        public virtual async Task<PagedResult<TListDto>> GetCollection(string id, TOptionsDto options)
        {
            var mapped = options.GetOptionsFor<TListDto>();
            return await DefaultUnit.GetCollectionRepositoryFor<T>().LoadCollectionAndMap(id, mapped);
        }

        public virtual async Task<Dictionary<string, IEnumerable<Named<object>>>> GetEditLookups(Dictionary<string, string> dto)
        {
            return await LookupsService.GetRequestedLookups(dto);
        }

        public virtual async Task<Dictionary<string, IEnumerable<Named<object>>>> GetListLookups(Dictionary<string, string> dto)
        {
            return await LookupsService.GetRequestedLookups(dto);
        }

        public virtual async Task<TSingleDto> GetSingle(TPrime id)
        {
            var item = await GetSingleById(id);
            return Mapper.Map<T, TSingleDto>(item);
        }

        public virtual async Task<bool> IsUnique(IsUniqueDto dto)
        {
            var id = dto.Id.ConvertTo<TPrime>();
            var exp = Expressions.Unique<T, TPrime>(id, dto.Property, dto.Value);
            return !(await Repository.Exist(exp));
        }

        protected virtual async Task<T> GetSingleById(TPrime id)
        {
            return await Repository.FindSingleById(id);
        }
    }
}
