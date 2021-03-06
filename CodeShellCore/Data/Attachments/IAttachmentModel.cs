﻿using CodeShellCore.Data;
using CodeShellCore.Files;
using CodeShellCore.Files.Uploads;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Data.Attachments
{
    public interface IAttachmentModel : IHasUploads, IEditable
    {
        TmpFileData File { get; set; }
        string EntityType { get; set; }
        long EntityId { get; set; }
        string FilePath { get; set; }


    }
}
