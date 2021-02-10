using System.Collections.Generic;

namespace Asga.Auth.Dto
{
    public class SimpleListModel<T>
    {
        public IEnumerable<T> List { get; set; }
        public int Count { get; set; }
    }
}
