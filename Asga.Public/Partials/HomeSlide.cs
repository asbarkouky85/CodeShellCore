using CodeShellCore.Files;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Asga.Public
{
   public partial class HomeSlide
    {
        [NotMapped]
        public TmpFileData ImageFile { get; set; }
    }
}
