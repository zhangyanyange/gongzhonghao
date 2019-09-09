﻿using System.Collections.Generic;

namespace MicroFeel.Pramy.Agent.Models.Template
{
    public class TemplateMessage
    {
        public List<string> Contents { get; set; }
        public List<string> OpenIds { get; set; }
        public string First { get; set; }
        public string Remark { get; set; }
        public string Url { get; set; }
        public string TemplateId { get; set; }
    }
}
