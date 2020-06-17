using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Text;

namespace ExampleProject.Commander.Services
{
    public class ScopedClass
    {
        static int IdInc = 0;
        public int Id { get; set; }
        public ScopedClass()
        {
            Id = IdInc++;
        }
    }
    public class InjectionTest
    {
        public ScopedClass ScopedClass;
        public InjectionTest(IServiceProvider prov)
        {
            ScopedClass = prov.GetService<ScopedClass>();
        }
    }
}
