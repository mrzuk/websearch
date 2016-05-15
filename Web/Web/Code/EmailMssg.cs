using RazorEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Code
{
    public class EmailMssg
    {
        public string TemplateString { get; set; }
        public object TemplateModel { get; set; }

        public string Subject { get; set; }

        public string SenderAddress { get; set; }
        public List<string> Receivers{ get;set;}

        public bool IsHtml { get; set; }
        public string GetMessageContent()
        {
            return Razor.Parse(this.TemplateString, this.TemplateModel);
        }
    }
}