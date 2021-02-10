using CodeShellCore.Text.Localization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Web.Controllers
{
    public interface ILocalizableEntityController
    {
        IActionResult GetLocalizationData(long id);
        IActionResult SetLocalizationData(long id, [FromBody]Dictionary<string, LocalizablesDTO> data);
    }
}
