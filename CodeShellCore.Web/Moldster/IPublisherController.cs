using CodeShellCore.Net;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Web.Moldster
{
    public interface IPublisherController
    {
        IActionResult HandleRequest(PublisherRequest req);
    }
}
