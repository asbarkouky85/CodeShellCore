using CodeShellCore.Data.Helpers;
using CodeShellCore.Files;
using CodeShellCore.Files.Uploads;
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

            foreach (T item in ChangeSet.Create(models).Added)
            {
                item.FilePath = item.File.SaveFile(folder, publicFolder);
            }
        }
    }
}
