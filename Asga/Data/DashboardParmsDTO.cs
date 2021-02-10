using CodeShellCore.Data;
using CodeShellCore.Linq.Filtering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asga.Data
{
   public class DashboardParmsDTO: IEmplement
    {
        public string EntityName { get; set; }
        public string CollectionId { get; set; }
        public IEnumerable<PropertyFilter> Filters { get; set; }
        public string GroupByColumn { get; set; }
    }

    public class DashboardStatusQuery: DashboardParmsDTO
    {
        public IEnumerable<long> Statuses { get; set; }
        
    }

    public class DashboardFilter {
        public long PartyId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}
