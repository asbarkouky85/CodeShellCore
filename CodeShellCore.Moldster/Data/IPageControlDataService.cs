﻿using System;
using System.Collections.Generic;
using System.Text;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Moldster;
using CodeShellCore.Moldster.Dto;
using CodeShellCore.Moldster.Razor;
using CodeShellCore.Services;

namespace CodeShellCore.Moldster.Data
{
    public interface IPageControlDataService : IServiceBase
    {
        IEnumerable<DomainWithPagesDTO> GetDomainWithPages(long tenantId, string domainName = null);
        SubmitResult UpdateTemplateControls(PageCategory p, List<ControlDTO> controls);
        SubmitResult DeleteUnusedControls(PageCategory p, List<ControlDTO> controls);
        SubmitResult UpdateTemplatePages(long id, long? tenantId = null);
    }
}
