﻿using CodeShellCore.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Example.UI.Controllers
{
    [ApiAuthorize(AllowAnonymous = true)]
    public class FilesController : CodeShellCore.Web.Controllers.BaseFilesController
    {
        public FilesController()
        {
        }
    }
}
