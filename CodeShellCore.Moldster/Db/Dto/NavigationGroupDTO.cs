﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Db.Dto
{
    public class NavigationGroupDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<NavigationPageDTO> Pages { get; set; }
    }
}
