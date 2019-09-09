using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MQ.WeChat.Models.TemplateMessageResult
{
    public class ErrorResult : Result
    {
        public override int Status { get; set; } = 0x12131992;
    }
}
