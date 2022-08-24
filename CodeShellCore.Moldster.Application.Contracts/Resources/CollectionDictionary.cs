using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Resources
{
    public class CollectionDictionary
    {
        public Dictionary<long, Dictionary<string, EntityCollectionDTOBase>> Collections { get; set; }
    }
}
