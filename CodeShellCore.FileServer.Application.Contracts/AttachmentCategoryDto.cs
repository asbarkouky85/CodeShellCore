using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.FileServer
{
    public class AttachmentCategoryDto
    {
        public string Name { get; set; }
        public string ValidExtensions { get; set; }
        public int MaxSize { get; set; }
        public DimensionDto MaxDimension { get; set; }
        public int? MaxCount { get; set; }
    }
}
