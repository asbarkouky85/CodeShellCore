using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Files.Reporting
{
   public static class RdlcXmlTemplates
    {
        public static string DataSetTemplate
        {
            get
            {
                return
@"<DataSet Name=""%Name%"">
    <Query>
        <DataSourceName>%DataSourceName%</DataSourceName >
        <CommandText>/* Local Query */</CommandText>
    </Query>

    <Fields>
        %Fields%
    </Fields>
</DataSet>";
            }
        }
        public static string DataSourceTemplate
        {
            get
            {
                return
@"<DataSource Name=""%Name%"">
    <ConnectionProperties>
		<DataProvider>System.Data.DataSet</DataProvider>
		<ConnectString>/* Local Connection */</ConnectString>
    </ConnectionProperties>
    <rd:DataSourceID>7c181f69-2fa1-4195-8673-4bbf91f5bbcf</rd:DataSourceID>
</DataSource>";
            }
        }
        public static string FieldTemplate {
            get
            {
                return
@"<Field Name=""%Name%"">
	<DataField>%Name%</DataField>
	<rd:TypeName>%Type%</rd:TypeName>
</Field>
";
            } }
        public static string ReportTemplate
        {
            get
            {
                return
@"<?xml version=""1.0"" encoding=""utf-8""?>
<Report xmlns=""http://schemas.microsoft.com/sqlserver/reporting/2008/01/reportdefinition"" xmlns:rd=""http://schemas.microsoft.com/SQLServer/reporting/reportdesigner"">
  <Body>
    <Height>3in</Height>
  </Body>
  
  <Page></Page>
  <Width>6.5in</Width>
  <DataSources>
  </DataSources>
  <DataSets>
  </DataSets>
</Report>";
            }
        }

    }
}
