using System;
using System.Collections.Generic;
using System.Text;

namespace Asga.Data
{
    public class ItemAmountDTO : ItemCommonDTO
    {
        public decimal Amount { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
