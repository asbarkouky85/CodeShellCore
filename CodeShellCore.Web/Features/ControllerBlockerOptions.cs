using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Web.Features
{
    public class ControllerBlockerOptions
    {
        List<Type> _types = new List<Type>();

        public IReadOnlyList<Type> BlockedControllers => _types;

        public void AddToBlocked<T>() where T : Controller
        {
            _types.Add(typeof(T));
        }
    }
}
