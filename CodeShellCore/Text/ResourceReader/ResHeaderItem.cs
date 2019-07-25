using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace CodeShellCore.Text.ResourceReader
{
    public class ResHeaderItem
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "value")]
        public string Value { get; set; }
    }
}
