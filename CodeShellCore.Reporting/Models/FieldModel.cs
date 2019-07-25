using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Reporting.Models
{
    public class FieldModel
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }

    public class DataSetModel
    {
        public string DataSourceName { get; set; }
        public string Name { get; set; }
        public string Fields { get; set; }
    }
}
