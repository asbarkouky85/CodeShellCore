﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CodeShellCore.Moldster.Configurator.Dtos
{
    public class PreviewTask
    {
        public string TenantCode { get; set; }
        public string Process { get; set; }
        public bool IsStarted { get; set; }
        public bool FailedToStart { get; set; }
        public void WaitForStartResult()
        {
            while (!IsStarted && !FailedToStart) ;
        }

    }
}
