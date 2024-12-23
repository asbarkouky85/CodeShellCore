using System.IO;
using System.Threading.Tasks;
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

        public Task Save(string path, ResourceContainer cont)
        {
            return Task.Run(() =>
            {

                XmlSerializer ser = new XmlSerializer(typeof(ResourceContainer));
                File.WriteAllText(path, "");
                using (var str = File.OpenWrite(path))
                {
                    ser.Serialize(str, cont);
                }
            });
        }
    }
}
