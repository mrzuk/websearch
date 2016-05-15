using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.DAL;

namespace Web.Code
{
    public class Configuration
    {
        public string ContactFormEmail { get; set; }
        public FtpConfiguration ftpConfig { get; set; }
        public static Configuration Create(WebDbEntities db) {
            Configuration config = new Configuration();

            FtpConfiguration ftpConfig = new FtpConfiguration();
            ftpConfig.Login = GetValue("emailLogin", db);
            ftpConfig.Password = GetValue("emailPassword", db);
            ftpConfig.Port = Int32.Parse(GetValue("smttpPort", db));
            ftpConfig.Server = GetValue("emailServer", db);

            config.ContactFormEmail = GetValue("contactUsEmail", db);
            config.ftpConfig = ftpConfig;

            return config;
        }

        private static string GetValue(string key, WebDbEntities db)
        {
            string value = db.Configurations.Where(c => c.Key == key).Select(c=>c.Value).FirstOrDefault();
            return value;
        }
    }
}