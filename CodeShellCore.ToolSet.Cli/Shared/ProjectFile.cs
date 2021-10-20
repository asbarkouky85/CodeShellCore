using CodeShellCore.ToolSet.Versions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace CodeShellCore.ToolSet
{
    public class ProjectFile
    {
        private List<string> contents;
        Dictionary<string, ParameterValue> values = new Dictionary<string, ParameterValue>();

        private int _targetFrameWorkLine = 5;
        private string _filePath;
        private readonly IFileReader reader;
        private string _tagPattern;
        private string _tagFormatter;
        private string _assemblyName;
        public string Folder { get; private set; }
        public string ProjectName { get; private set; }
        public bool IsCore { get; private set; }

        public ProjectFile(string path, IFileReader reader)
        {
            if (!reader.FileExists(path))
                throw new FileNotFoundException(path);

            ProjectName = reader.GetFileName(path).Replace(".csproj", "");
            Folder = reader.GetFolderFullName(path);
            contents = reader.GetAllLines(path);
            IsCore = _isCore(out _targetFrameWorkLine);
            if (!IsCore)
                _targetFrameWorkLine = contents.Count >= 5 ? 5 : (contents.Count - 1);
            _tagPattern = IsCore ? "<{0}>(.*?)</{0}>" : @"\[assembly: {0}\((.*?)\)\]";
            _tagFormatter = IsCore ? "<{0}>{1}</{0}>" : "[assembly: {0}(\"{1}\")]";

            if (IsCore)
            {
                _filePath = path;
            }
            else
            {
                _filePath = Path.Combine(Folder, @"Properties\AssemblyInfo.cs");
                if (!reader.FileExists(_filePath))
                    throw new FileNotFoundException(_filePath);
                contents = reader.GetAllLines(_filePath);
            }
            ReadVersionParameters();
            this.reader = reader;
        }

        private List<string> RemoveExistingTags(string[] vParams)
        {
            List<string> nList = new List<string>();
            for (var i = 0; i < contents.Count; i++)
            {
                bool found = false;
                foreach (var v in vParams)
                {
                    var val = GetTagContent(contents[i], v);
                    if (val != null)
                    {
                        values[v] = new ParameterValue
                        {
                            Name = v,
                            Value = val,
                            Line = contents[i]
                        };
                        found = true;
                    }
                }
                if (!found)
                    nList.Add(contents[i]);
            }
            return nList;
        }

        public void ReadVersionParameters()
        {

            string[] vParams = new string[0];
            if (IsCore)
                vParams = new[] { "Version", "AssemblyName", "AssemblyVersion", "FileVersion" };
            else
                vParams = new[] { "AssemblyTitle", "AssemblyProduct", "AssemblyVersion", "AssemblyFileVersion" };

            contents = RemoveExistingTags(vParams);

            bool added = false;
            var nContents = new List<string>();
            for (var i = 0; i < contents.Count; i++)
            {
                nContents.Add(contents[i]);
                if (i >= _targetFrameWorkLine && !added)
                {
                    for (var p = 0; p < vParams.Length; p++)
                    {
                        ParameterValue pValue = new ParameterValue { Name = vParams[p] };
                        if (values.TryGetValue(vParams[p], out ParameterValue ex))
                            pValue = ex;
                        else
                            values[vParams[p]] = pValue;

                        string line = "    " + string.Format(_tagFormatter, pValue.Name, pValue.Value);
                        pValue.Index = i + p + 1;
                        nContents.Add(line);
                        added = true;
                    }
                }
            }
            contents = nContents;

        }

        bool _isCore(out int targetFrameWorkLine)
        {
            string targ = null;
            targetFrameWorkLine = 0;
            for (var i = 0; i < contents.Count; i++)
            {
                targ = GetTagContent(contents[i], "TargetFramework", "<{0}>(.*?)</{0}>");
                if (targ != null)
                {
                    targetFrameWorkLine = i;
                    break;
                }
            }
            return targ != null;
        }

        public string GetAssemblyName()
        {
            _assemblyName = values["AssemblyName"].Value;
            _assemblyName = string.IsNullOrEmpty(_assemblyName) ? ProjectName : _assemblyName;
            return _assemblyName;
        }

        public string GetTagContent(string subject, string tag, string usePattern = null)
        {
            var pat = usePattern ?? _tagPattern;
            string patternString = string.Format(pat, tag); //(isCore ?? IsCore) ? $"<{tag}>(.*?)</{tag}>" : @"\[assembly: " + tag + @"\((.*?)\)\]";
            Regex pattern = new Regex(patternString);
            var s = pattern.Match(subject);
            if (s.Success)
                return s.Groups[1].Value;

            return null;
        }

        public string GetVersion(int digits)
        {
            string ver = values["AssemblyVersion"].Value;

            if (string.IsNullOrEmpty(ver))
                return null;
            var splits = ver.Split('.');
            var res = new List<string>();

            for (int i = 0; i < digits && i < splits.Length; i++)
            {
                if (i == 3 && splits[i] == "0")
                    continue;
                res.Add(splits[i]);
            }

            return string.Join(".", res);
        }

        public void Save()
        {
            foreach (var item in values)
            {
                ChangeParameterValue(item.Value);
            }
            reader.WriteAllLines(_filePath, contents);
        }

        public void SetVersion(ProjectVersionRequest req)
        {
            if (IsCore)
            {
                values["Version"].Value = req.GetLongVersionString();
                values["AssemblyVersion"].Value = req.GetLongVersionString();
                values["FileVersion"].Value = req.GetLongVersionString();
                values["AssemblyName"].Value = GetAssemblyName();
            }
            else
            {
                values["AssemblyTitle"].Value = ProjectName + "-v" + req.GetShortVersion();
                values["AssemblyProduct"].Value = ProjectName + "-v" + req.GetShortVersion();
                values["AssemblyVersion"].Value = req.GetShortVersion();
                values["AssemblyFileVersion"].Value = req.GetShortVersion();
            }

            if (req.IsWeb)
            {
                SetPublishProfile(req.PublishProfile, req.GetLongVersionString());
            }

        }

        public void SetPublishProfile(string profileName, string version)
        {
            string publishFile = Path.Combine(Folder, "Properties\\PublishProfiles\\" + profileName + ".pubxml");
            if (!reader.FileExists(publishFile))
                throw new Exception("Could not find file : " + publishFile);

            string contents = reader.ReadAllText(publishFile);
            Regex prof = new Regex(@"<publishUrl>(.*?)</publishUrl>");
            Match coll = prof.Match(contents);
            if (coll.Groups.Count > 1)
            {
                string currentUrl = coll.Groups[1].Value;
                string folder = reader.GetFolderFullName(publishFile);
                string newUrl = Path.Combine(folder, ProjectName + "-v" + version);
                contents = prof.Replace(contents, "<publishUrl>" + newUrl + "</publishUrl>");
                reader.WriteAllText(publishFile, contents);
                Console.WriteLine("\tPublish Url Altered :\t" + publishFile);
            }
        }

        void ChangeParameterValue(ParameterValue v)
        {
            string tag = v.Value;
            string patternString = string.Format(_tagPattern, v.Name);
            string changeTo = string.Format(_tagFormatter, v.Name, v.Value); // IsCore ? $"<{tag}>{v.Value}</{tag}>" : @"\[assembly: " + tag + $"(\"{v.Value}\")]";
            Regex pattern = new Regex(patternString);
            var st = pattern.Replace(v.Line ?? contents[v.Index], changeTo);
            contents[v.Index] = st;
        }
    }
}
