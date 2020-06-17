using System.Collections.Generic;

namespace CodeShellCore.Reporting
{
    public enum ReportTypes
    {
        /// <summary>
        /// TIFF file
        /// </summary>
        Image,
        /// <summary>
        /// PDF
        /// </summary>
        PDF,
        /// <summary>
        /// Excel 2003
        /// </summary>
        Excel,
        /// <summary>
        /// Excel EXCELOPENXML
        /// </summary>
        ExcelXML,
        /// <summary>
        /// Word 2003
        /// </summary>
        Word,
        /// <summary>
        /// WORDOPENXML
        /// </summary>
        WordXML

    }

    public static class ReportType
    {
        private static Dictionary<int, string> _types;
        private static Dictionary<int, string> types
        {
            get
            {
                if (_types == null)
                {
                    _types = new Dictionary<int, string>();
                    _types[0] = "IMAGE";
                    _types[1] = "PDF";
                    _types[2] = "Excel";
                    _types[3] = "EXCELOPENXML";
                    _types[4] = "Word";
                    _types[5] = "WORDOPENXML";
                }
                return _types;
            }
        }
        private static Dictionary<int, string> _mimes;
        private static Dictionary<int, string> Mimes
        {
            get
            {
                if (_mimes == null)
                {
                    _mimes = new Dictionary<int, string>();
                    _mimes[0] = "image/tiff";
                    _mimes[1] = "application/pdf";
                    _mimes[2] = "application/vnd.ms-excel";
                    _mimes[3] = "application/vnd.ms-excel";
                    _mimes[4] = "application/msword";
                    _mimes[5] = "application/msword";
                }
                return _mimes;
            }
        }

        private static Dictionary<int, string> _ext;
        private static Dictionary<int, string> Extensions
        {
            get
            {
                if (_ext == null)
                {
                    _ext = new Dictionary<int, string>();
                    _ext[0] = ".tiff";
                    _ext[1] = ".pdf";
                    _ext[2] = ".xls";
                    _ext[3] = ".xls";
                    _ext[4] = ".doc";
                    _ext[5] = ".doc";
                }
                return _ext;
            }
        }
        public static string GetString(this ReportTypes type)
        {

            return types[(int)type];
        }

        public static string GetMime(this ReportTypes type) {
            return Mimes[(int)type];
        }

        public static string GetExtension(this ReportTypes type)
        {
            return Extensions[(int)type];
        }
    }
}
