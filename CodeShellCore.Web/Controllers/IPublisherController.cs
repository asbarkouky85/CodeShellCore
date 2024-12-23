using CodeShellCore.Net;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Web.Controllers
{
    public interface IPublisherController
    {
        PublisherResult HandleRequest(PublisherRequest req);
    }
}
