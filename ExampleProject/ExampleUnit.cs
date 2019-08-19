using CodeShellCore.Data;
using CodeShellCore.Data.EntityFramework;
using ExampleProject.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExampleProject
{
    public class ExampleUnit : UnitOfWork<ExampleContext>
    {
        public IRepository<Employee> EmployeeRespository => GetRepositoryFor<Employee>();
    }
}
