using System;
using System.Collections.Generic;
using System.Text;

namespace Asga.Data
{
    public class ListingDTO : AsgaModelBase
    {
        public long Id { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
    }
}
