using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace CodeShellCore.Reporting.DataSetXML
{
    [XmlRoot("Report")]
    public class Report
    {
        public DataSets DataSets { get; set; }
        [XmlNamespaceDeclarations]
        public XmlSerializerNamespaces xmlns;
        [XmlAttribute(AttributeName = "rd")]
        public string rd;
    }

    public class DataSets
    {
        [XmlArray(ElementName = "DataSet")]
        public DataSetItem[] Sets { get; set; }

    }
    public class QueryItem
    {
        public string DataSourceName { get; set; }
        public string CommandText { get; set; }
    }
    public class DataSetItem
    {
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }


        //public QueryItem Query { get; set; }
        ////[XmlArray(ElementName ="Field")]
        ////[XmlElement(ElementName = "Fields")]
        //public FieldItem[] Fields { get; set; }

    }

    public class FieldItem
    {
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }
        public string DataField { get; set; }



        //[XmlElement(ElementName = "rd:TypeName")]
        //public string TypeName { get; set; }
    }
}
