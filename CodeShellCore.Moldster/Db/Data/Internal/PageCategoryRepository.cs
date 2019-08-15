using CodeShellCore.Data.EntityFramework;
using CodeShellCore.Moldster.Definitions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Db.Data.Internal
{
    public class PageCategoryRepository : MoldsterRepository<PageCategory, MoldsterContext>, IPageCategoryRepository
    {
        public PageCategoryRepository(MoldsterContext con) : base(con)
        {
        }


    }
}
