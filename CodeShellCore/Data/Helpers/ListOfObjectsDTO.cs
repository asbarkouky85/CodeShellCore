using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Data.Helpers
{
  public  class ListOfObjectsDTO<T> where T : class
    {
        public IEnumerable<T> List { get; set; }
    }
}
