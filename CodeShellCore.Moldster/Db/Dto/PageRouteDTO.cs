﻿using CodeShellCore.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Db.Dto
{
    public class PageRouteDTO : IDTO<PageRoute>
    {
        public long Id { get; set; }
        public string ListUrlString { get; set; }
        public string AddUrlString { get; set; }
        public string EditUrlString { get; set; }
        public string DetailsUrlString { get; set; }
    }
}