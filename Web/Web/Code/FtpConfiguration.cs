using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Code
{
    public class FtpConfiguration
    {
        public string Server { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public int Port { get; set; }

    }
}