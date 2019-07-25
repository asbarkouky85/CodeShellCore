using CodeShellCore.Services.Recursive;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Web.Controllers
{
    public interface ITreeController
    {
        [HttpGet]
        IEnumerable<IRecursiveModel> GetTree();
    }
}
