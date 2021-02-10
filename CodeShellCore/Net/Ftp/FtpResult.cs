using CodeShellCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace CodeShellCore.Net.Ftp
{
    public class FtpResult : HttpResult
    {
        public override bool IsSuccess => ((int)StatusCode) >=200 && ((int)StatusCode) < 300;
        public byte[] Bytes { get; set; } = new byte[0];
        public FtpStatusCode StatusCode { get; set; }
    }
}
