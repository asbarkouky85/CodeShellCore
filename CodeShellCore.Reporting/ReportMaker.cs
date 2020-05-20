using AspNetCore.Reporting;
using CodeShellCore.Files;
using CodeShellCore.Files.Reporting;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace CodeShellCore.Reporting
{
    public class ReportMaker
    {
        public LocalReport LocalReport { get; private set; }
        public DeviceInfoXML DeviceInfo { get; private set; }
        protected Dictionary<string, IEnumerable> _datasources;
        protected ReportTypes reportType;
        public Dictionary<string, IEnumerable> DataSources
        {
            get
            {
                if (_datasources == null)
                    _datasources = new Dictionary<string, IEnumerable>();
                return _datasources;
            }
        }
        public ReportMaker(string reportFileName, ReportTypes type)
        {
            string path = Path.Combine(Shell.ReportsRoot, reportFileName + ".rdlc");
            if (!File.Exists(path))
                throw new FileNotFoundException("File Not Found", path);

            LocalReport = new LocalReport(path);
            reportType = type;

            DeviceInfo = DeviceInfoXML.Make(type);
        }

        public static GenericReportMaker<T> CreateFor<T>(T model,ReportTypes type=ReportTypes.PDF) where T : ReportModel
        { 
            return new GenericReportMaker<T>(model, type);
        }

        protected virtual void AddDataSources()
        {
            foreach (var kvp in DataSources)
            {
                LocalReport.AddDataSource(kvp.Key, kvp.Value);
            }
        }

        public virtual FileBytes GetFile(string downloadName = null)
        {
            AddDataSources();
            var rep = LocalReport.Execute(ConvertType());
            return new FileBytes
            {
                FileName = downloadName,
                Bytes = rep.MainStream,
                Extension = reportType.GetExtension(),
                MimeType = reportType.GetMime()
            };
        }

        protected RenderType ConvertType()
        {
            switch (reportType)
            {
                case ReportTypes.Image:
                    return RenderType.Image;
                case ReportTypes.PDF:
                    return RenderType.Pdf;
                case ReportTypes.Excel:
                    return RenderType.Excel;
                case ReportTypes.ExcelXML:
                    return RenderType.ExcelOpenXml;
                case ReportTypes.Word:
                    return RenderType.Word;
                case ReportTypes.WordXML:
                    return RenderType.WordOpenXml;

            }
            return RenderType.Pdf;
        }

        public void SetPortrait()
        {
            DeviceInfo.PageHeight = 11.69;
            DeviceInfo.PageWidth = 8.27;
        }

        public void SetLandScape()
        {
            DeviceInfo.PageWidth = 11.69;
            DeviceInfo.PageHeight = 8.27;
        }
    }
}
