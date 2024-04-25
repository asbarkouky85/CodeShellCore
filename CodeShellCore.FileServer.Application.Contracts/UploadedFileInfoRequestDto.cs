using System;
using System.Collections;
using System.Collections.Generic;

namespace CodeShellCore.FileServer
{
    public class UploadedFileInfoRequestDto
    {
        public IEnumerable<long> Ids { get; set; }
    }
}