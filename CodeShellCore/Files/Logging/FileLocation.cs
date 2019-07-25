using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace CodeShellCore.Files.Logging
{
    public class FileLocation
    {
        [XmlAttribute(AttributeName = "AppName")]
        public string AppName { get; set; }
        [XmlAttribute(AttributeName = "FolderPath")]
        public string FolderPath { get; set; }
        [XmlAttribute(AttributeName = "FilePath")]
        public string FilePath { get; set; }

        public override string ToString()
        {
            return AppName;
        }
    }
}
