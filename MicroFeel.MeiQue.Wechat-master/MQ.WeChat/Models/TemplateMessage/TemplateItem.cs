using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MQ.WeChat.Models.TemplateMessage
{
    public class TemplateItem
    {
        public string template_id { get; set; }
        public string title { get; set; }
        public string primary_industry { get; set; }
        public string deputy_industry { get; set; }
        public string content { get; set; }
        public string example { get; set; }
    }
}
