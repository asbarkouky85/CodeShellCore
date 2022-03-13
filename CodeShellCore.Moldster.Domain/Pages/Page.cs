﻿using CodeShellCore.Moldster.Domains;
using CodeShellCore.Moldster.Navigation;
using CodeShellCore.Moldster.PageCategories;
using CodeShellCore.Moldster.Resources;
using System;
        [System.Runtime.Serialization.IgnoreDataMember]
        public Domain Domain { get; set; }
        [System.Runtime.Serialization.IgnoreDataMember]
        public PageCategory PageCategory { get; set; }
        [System.Runtime.Serialization.IgnoreDataMember]
        public Resource Resource { get; set; }
        [System.Runtime.Serialization.IgnoreDataMember]
        public ResourceAction ResourceAction { get; set; }
        [System.Runtime.Serialization.IgnoreDataMember]
        public ResourceCollection SourceCollection { get; set; }
        [System.Runtime.Serialization.IgnoreDataMember]
        public Tenant Tenant { get; set; }