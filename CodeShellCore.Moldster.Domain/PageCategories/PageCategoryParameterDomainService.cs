using CodeShellCore.Data.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.PageCategories
{
    public class PageCategoryParameterDomainService : IPageCategoryParameterDomainService
    {
        private bool disposedValue;
        private readonly IMoldsterUnit Unit;

        public PageCategoryParameterDomainService(IMoldsterUnit unit)
        {
            this.Unit = unit;
        }

        public async Task<SubmitResult> UpdateParameters(PageCategory cat, List<PageCategoryParameter> lst)
        {
            await Unit.PageCategoryParameterRepository.UpdateParameters(cat.Id, lst);
            return await Unit.SaveChanges(throwException: true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {

                }
                disposedValue = true;
            }
        }


        public void Dispose()
        {

            Dispose(disposing: true);
            System.GC.SuppressFinalize(this);
        }
    }
}
