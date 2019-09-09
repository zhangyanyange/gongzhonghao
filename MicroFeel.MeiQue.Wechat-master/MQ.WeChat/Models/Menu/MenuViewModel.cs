using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MQ.WeChat.Models
{
    public class MenuViewModel : MenuParentModel
    {
        public override string type { get; set; } = "view";
        public string url { get; set; }
    }
}
