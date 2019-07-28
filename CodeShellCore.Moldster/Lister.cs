using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster
{
    public class Lister
    {
        public string ListName { get; protected set; }
        public string CollectionIdentifier { get; set; }
        public virtual string CollecionName { get; protected set; }
        public virtual bool IsLookup { get; set; }

        public static Lister Make(string listName, bool lookup = true)
        {
            return new Lister
            {
                IsLookup = lookup,
                ListName = listName
            };
        }
        
    }
}
