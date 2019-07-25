using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Xml.Serialization;
using CodeShellCore.Helpers;

namespace CodeShellCore.Files.Logging
{
    public class LocationCollection
    {
        [XmlElement(ElementName = "Root")]
        public FileLocation[] Locations { get; set; }


        [XmlIgnore]
        public Dictionary<string, FileLocation> Dictionary { get; private set; }

        [XmlIgnore]
        XmlSerializer Serializer = new XmlSerializer(typeof(LocationCollection));

        [XmlIgnore]
        string FilePath;

        [XmlIgnore]
        private static string DefaultFolder = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments) + @"\logs";

        public static LocationCollection GetCollection(string path)
        {
            try
            {

                LocationCollection locs;
                if (File.Exists(path))
                {
                    XmlSerializer ser = new XmlSerializer(typeof(LocationCollection));

                    using (FileStream st = File.OpenRead(path))
                    {
                        locs = ser.Deserialize(st) as LocationCollection;
                    }

                }
                else
                {
                    locs = new LocationCollection();
                }

                locs.FilePath = path;
                locs.Dictionary = new Dictionary<string, FileLocation>();

                foreach (FileLocation loc in locs.Locations)
                    locs.Dictionary[loc.AppName] = loc;
                return locs;
            }
            catch
            {
                return new LocationCollection
                {
                    FilePath = path,
                    Dictionary = new Dictionary<string, FileLocation>(),
                    Locations = new FileLocation[0]
                };
            }

        }

        public void Save()
        {
            Locations = Dictionary.Select(d => d.Value).ToArray();
            if (Locations == null)
                Locations = new FileLocation[] { };

            File.WriteAllText(FilePath, "");
            FileStream str = File.OpenWrite(FilePath);
            Serializer.Serialize(str, this);
            str.Close();
        }



        public static string NewFileName()
        {
            DateTime t = DateTime.Now;
            string fileName = t.Year
                + "-" + t.Month.ToString("D2")
                + "-" + t.Day.ToString("D2")
                + "_" + t.Hour.ToString("D2")
                + "-" + t.Minute.ToString("D2")
                + "-" + t.Second.ToString("D2")
                + ".log";
            return fileName;
        }

        public FileLocation GetFileLocation(string app, string folder = null)
        {
            if (folder != null)
                folder = Utils.GetAsAbsolutePath(folder);

            FileLocation f;
            if (Dictionary.TryGetValue(app, out f))
            {
                if (folder != null && f.FolderPath != folder)
                {
                    f.FolderPath = folder;
                    f.FilePath = Path.Combine(folder, NewFileName());
                    Save();
                }
            }
            else if (folder != null)
            {
                f = new FileLocation
                {
                    FolderPath = folder,
                    FilePath = Path.Combine(folder, NewFileName()),
                    AppName = app
                };
                Dictionary[app] = f;
                Save();
            }
            else
            {
                f = new FileLocation
                {
                    FolderPath = DefaultFolder,
                    FilePath = Path.Combine(DefaultFolder, NewFileName()),
                    AppName = app
                };
            }
            return f;
        }
    }
}
