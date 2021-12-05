﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.ToolSet.DtoGeneration
{
    public class DtoGenerationRequest
    {
        public string Options { get; set; }
        public string EntityType { get; set; }
        public string ProjectFile { get; set; }
        public bool NoBuild { get; set; }
    }
}
