using CodeShellCore.Data.Helpers;
using System.Collections.Generic;

namespace CodeShellCore.Moldster.PageCategories
{
    public class PageCategoryParameterDomainService : IPageCategoryParameterDomainService
    {
        private bool disposedValue;
        private readonly IConfigUnit Unit;

        public PageCategoryParameterDomainService(IConfigUnit unit)
        {
            this.Unit = unit;
        }

        public SubmitResult UpdateParameters(PageCategory cat, List<PageCategoryParameter> lst)
        {
            Unit.PageCategoryParameterRepository.UpdateParameters(cat.Id, lst);
            var s = Unit.SaveChanges(throwException: true);

            return s;
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
