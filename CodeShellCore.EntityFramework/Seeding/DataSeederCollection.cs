using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Seeding
{
    public class DataSeederCollection<T> where T : DbContext
    {
        private List<Type> _lst = new List<Type>();
        public void AddSeeder<TSeeder>() where TSeeder : class, IDataSeedContributor<T>
        {
            _lst.Add(typeof(TSeeder));
        }

        public IEnumerable<Type> Seeders => _lst;
    }
}
