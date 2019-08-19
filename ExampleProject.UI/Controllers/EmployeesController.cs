using CodeShellCore.Data.Services;
using CodeShellCore.Web.Controllers;
using ExampleProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleProject.UI.Controllers
{
    public class EmployeesController : EntityController<Employee, long>
    {
        public EmployeesController(IEntityService<Employee> service) : base(service)
        {
        }
    }
}
