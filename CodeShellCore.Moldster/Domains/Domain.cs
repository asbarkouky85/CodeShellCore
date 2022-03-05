﻿using CodeShellCore.Data.Recursion;
using CodeShellCore.Moldster.PageCategories;
using CodeShellCore.Moldster.Pages;
using CodeShellCore.Moldster.Resources;
using System;
        public bool HasContents { get; set; }
        [NotMapped]
        public int ContentCount { get; set; }
        [NotMapped]
        public IEnumerable<IRecursiveModel> Children { get; set; }