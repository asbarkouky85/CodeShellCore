using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asga.Public.Business
{
    public interface IHomeSlideService : IEntityService<HomeSlide>
    {
        SubmitResult SetSorting(IEnumerable<long> ids);
        SubmitResult SetActive(long id,bool state);
    }
}
