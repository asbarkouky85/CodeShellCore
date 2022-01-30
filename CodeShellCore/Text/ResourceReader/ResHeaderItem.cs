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

        public static ResHeaderItem[] Default
        {
            get
            {
                ResHeaderItem[] lst = new ResHeaderItem[4];
                lst[0] = new ResHeaderItem
                {
                    Name= "resmimetype",
                    Value= "text/microsoft-resx"
                };
                lst[1] = new ResHeaderItem
                {
                    Name = "version",
                    Value = "1.3"
                };
                lst[2] = new ResHeaderItem
                {
                    Name = "reader",
                    Value = "System.Resources.ResXResourceReader, System.Windows.Forms, Version=2.0.3500.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
                };
                lst[3] = new ResHeaderItem
                {
                    Name = "writer",
                    Value = "System.Resources.ResXResourceWriter, System.Windows.Forms, Version=2.0.3500.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
                };
                return lst;
            }
        }
    }
}
