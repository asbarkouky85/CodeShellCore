using CodeShellCore.Data;
using CodeShellCore.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Text.Localization
{
   public interface ILoadDTO<T,TDTO> where TDTO: class,IDTO<T> where T:class
    {
        LoadResult<TDTO> LoadDTO(ListOptions<TDTO> opts);
    }
}
