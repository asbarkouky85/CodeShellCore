﻿using CodeShellCore.Moldster.Pages;
using CodeShellCore.Text;
using System;
        [System.Runtime.Serialization.IgnoreDataMember]
        public PageCategory PageCategory { get; set; }
        public string TypeString { get { return ((PageParameterTypes)Type).StringFormat(); } }