using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Data.Attachments
{
    public interface IHasUploads
    {
        void LoadFile(string serviceUrl);
    }
}
