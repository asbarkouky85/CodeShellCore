using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace CodeShellCore.Text.ResourceReader
{
    [XmlRoot(ElementName ="root")]
    public class ResourceContainer
    {
        [XmlElement(ElementName = "data")]
        public DataItem[] DataItems { get; set; } = new DataItem[0];
        [XmlElement(ElementName = "resheader")]
        public ResHeaderItem[] Headers { get; set; }
    }
}
