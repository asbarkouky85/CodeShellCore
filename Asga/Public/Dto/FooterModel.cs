using System;
using System.Collections.Generic;
using System.Text;

namespace Asga.Public.Dto
{
    public class ContactDTO
    {
        public string Value;
        public string Icon;
    }
    public class FooterModel
    {
        public IEnumerable<ContactDTO> Contacts { get; set; }
    }
}
