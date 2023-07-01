//using AspNetCore.Reporting;
using CodeShellCore.Files;
using CodeShellCore.Files.Reporting;
using Microsoft.Reporting.NETCore;
using System.IO;


namespace CodeShellCore.Reporting
{
    public class ReportMaker
    {
        public LocalReport LocalReport { get; private set; }
        public DeviceInfoXML DeviceInfo { get; private set; }
       // protected Dictionary<string, IEnumerable> _datasources;
        protected ReportDataSource _datasources;
        protected ReportTypes reportType;
        public ReportDataSource DataSources
        {
            get
            {
                if (_datasources == null)
                    _datasources = new ReportDataSource();
                return _datasources;
            }
        }
        public ReportMaker(string reportFileName, ReportTypes type)
        {
            string path = Path.Combine(Shell.ReportsRoot, reportFileName + ".rdlc");
            if (!File.Exists(path))
                throw new FileNotFoundException("File Not Found", path);

            LocalReport = new LocalReport();
            LocalReport.ReportPath= path;
            reportType = type;

            DeviceInfo = DeviceInfoXML.Make(type);
        }

        public static GenericReportMaker<T> CreateFor<T>(T model,ReportTypes type=ReportTypes.PDF) where T : ReportModel
        { 
            return new GenericReportMaker<T>(model, type);
        }

        protected virtual void AddDataSources()
        {
           
                LocalReport.DataSources.Add(DataSources);
            
        }

        public virtual FileBytes GetFile(string downloadName = null)
        {
            AddDataSources();
            var rep = LocalReport.Render(reportType.ToString());
            return new FileBytes
            {
                FileName = downloadName,
                Bytes = rep,
                Extension = reportType.GetExtension(),
                MimeType = reportType.GetMime()
            };
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
