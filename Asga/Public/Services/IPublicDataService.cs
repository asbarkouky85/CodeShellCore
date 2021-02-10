using Asga.Public.Dto;
using CodeShellCore.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asga.Public.Services
{
    public interface IPublicDataService : IServiceBase
    {
        HomePageData Get();
        FooterModel GetFooterData();
    }
}
