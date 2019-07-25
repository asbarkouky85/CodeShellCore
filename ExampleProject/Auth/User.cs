using System;
using System.Collections.Generic;
using System.Text;

namespace ExampleProject.Auth
{
    public class User
    {
        public long Id { get; set; }
        public string LogonName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long PersonId { get; set; }
    }
}
