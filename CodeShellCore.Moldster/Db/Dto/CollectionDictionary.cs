using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Db.Dto
{
    public class CollectionDictionary
    {
        public Dictionary<long, Dictionary<string, EntityCollectionDTOBase>> Collections { get; set; }
    }
}
