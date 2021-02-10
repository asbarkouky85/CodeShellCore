using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace CodeShellCore.Text.ResourceReader
{
    //[XmlRoot(ElementName ="data")]
    public class DataItem
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "xml:space")]
        public string Space { get; set; }

        [XmlElement(ElementName ="value")]
        public string Value { get; set; }
        
    }

    
}
