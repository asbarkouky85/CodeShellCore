using CodeShellCore.Moldster.PageCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Views
{
    public class NullLayoutsService : ILayoutsService
    {
        public List<LayoutFileDTO> GetLayouts(bool nameOnly = false)
        {
            return new List<LayoutFileDTO>();
        }
    }
}
