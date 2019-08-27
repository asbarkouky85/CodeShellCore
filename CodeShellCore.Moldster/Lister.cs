﻿using System;
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

        public static Lister<T> Make<T>(string listName, string collectionName = null)
        {
            return new Lister<T>(listName, collectionName);
        }

    }

    public class Lister<T> : Lister
    {
        private long entityId;

        public override string CollecionName { get { return "C" + entityId + (CollectionIdentifier == null ? "" : "__" + CollectionIdentifier); } }
        public override bool IsLookup { get { return true; } set { } }
        public Lister(string listName, string collecionIdentifier = null)
        {
            ListName = listName;
            CollectionIdentifier = collecionIdentifier;
        }
    }
}
