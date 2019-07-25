using System.IO;
using System.Xml.Serialization;

namespace CodeShellCore.Text.ResourceReader
{
    public class ResxXmlReader
    {
        public bool TryRead(string filePate, out ResourceContainer cont)
        {
            XmlSerializer ser = new XmlSerializer(typeof(ResourceContainer));
            try
            {
                using (var str = File.OpenRead(filePate))
                {
                    cont = (ResourceContainer)ser.Deserialize(str);
                }
                return true;
            }
            catch/*(Exception ex)*/
            {
                cont = null;
                return false;
                //throw ex;
                //Logger.WriteException(ex);
            }
        }

        public void Save(string path, ResourceContainer cont)
        {
            XmlSerializer ser = new XmlSerializer(typeof(ResourceContainer));
            File.WriteAllText(path, "");
            using (var str = File.OpenWrite(path))
            {
                ser.Serialize(str, cont);
            }
        }
    }
}
