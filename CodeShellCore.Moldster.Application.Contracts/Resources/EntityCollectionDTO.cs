using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Text;

namespace CodeShellCore.Moldster.Resources
{
    public abstract class EntityCollectionDTOBase
    {
        public string Identifier { get; set; }
    }
    public class EntityCollectionDTO<T> : EntityCollectionDTOBase
    {
        [IgnoreDataMember]
        public Expression<Func<T, bool>> Expression { get; set; }
    }
}
