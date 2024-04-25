

namespace CodeShellCore.Reporting
{
    public class DeviceInfoXML
    {
        public ReportTypes Type
        {
            set
            {
                OutputFormat = value.GetString();
            }
        }
        private string OutputFormat { get; set; }
        public double PageWidth { get; set; }
        public double PageHeight { get; set; }
        public double MarginTop { get; set; }
        public double MarginLeft { get; set; }
        public double MarginRight { get; set; }
        public double MarginBottom { get; set; }

        private DeviceInfoXML(ReportTypes type, double pageWidth, double pageHeight)
        {
            Type = type;
            PageWidth = pageWidth;
            PageHeight = pageHeight;
            MarginTop = 0;
            MarginLeft = 0;
            MarginRight = 0;
            MarginBottom = 0;
        }

        public static DeviceInfoXML Make(ReportTypes type, double pageWidth =8.27, double pageHeight = 11.69)
        {
            return new DeviceInfoXML(type, pageWidth, pageHeight);
        }
        
        public override string ToString()
        {
            string st =
@"<DeviceInfo>
    <OutputFormat>{0}</OutputFormat>
    <PageWidth>{1}in</PageWidth>
    <PageHeight>{2}in</PageHeight>
    <MarginTop>{3}in</MarginTop>
    <MarginLeft>{4}in</MarginLeft>
    <MarginRight>{5}in</MarginRight>
    <MarginBottom>{6}in</MarginBottom>
</DeviceInfo>";

            return string.Format(st,
                    OutputFormat,
                    PageWidth.ToString("F2"),
                    PageHeight.ToString("F2"),
                    MarginTop.ToString("F2"),
                    MarginLeft.ToString("F2"),
                    MarginRight.ToString("F2"),
                    MarginBottom.ToString("F2")
                );
        }
    }
}