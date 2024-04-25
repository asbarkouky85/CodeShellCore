using CodeShellCore.Files.Reporting;
using Microsoft.Reporting.NETCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Reporting
{
    public class GenericReportMaker<T> : ReportMaker where T : ReportModel
    {
        public T Data { get; private set; }
        public GenericReportMaker(T model, ReportTypes t) : base(model.Template, t)
        {
            Data = model;
        }

        protected override void AddDataSources()
        {
            var inf = typeof(T).GetProperties();
            foreach (var p in inf)
            {
                if (p.Name != "Template")
                    LocalReport.DataSources.Add(new ReportDataSource { Name= p.Name, Value= p.GetValue(Data) });
            }
        }
    }
}
