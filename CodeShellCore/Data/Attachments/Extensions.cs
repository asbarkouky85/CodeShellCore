using CodeShellCore.Data.Helpers;
using CodeShellCore.Files;
using CodeShellCore.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace CodeShellCore.Data.Attachments
{
    public static class Extensions
    {
        public static void SaveFiles<T>(this IEnumerable<T> models, string folder, bool publicFolder = true) where T : class, IAttachmentModel
        {
            if (models == null)
                return;
            string dir = Path.Combine(Shell.AppRootPath, folder);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            foreach (T item in ChangeSet.Create(models).Added)
            {
                item.FilePath = item.File.SaveFile(folder, publicFolder);
            }
        }
    }
}
