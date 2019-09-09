using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MQ.WeChat.Models
{
    public class MenuClickModel : MenuParentModel
    {
        public override string type { get; set; } = "click";
        public string key { get; set; }
    }
}
