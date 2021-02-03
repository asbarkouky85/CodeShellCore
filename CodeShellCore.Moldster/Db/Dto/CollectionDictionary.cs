using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Dto
{
    public class CollectionDictionary
    {
        public Dictionary<long, Dictionary<string, EntityCollectionDTOBase>> Collections { get; set; }
    }
}
