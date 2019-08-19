using CodeShellCore.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExampleProject.Entities
{
    public class Employee : IModel<long>
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
