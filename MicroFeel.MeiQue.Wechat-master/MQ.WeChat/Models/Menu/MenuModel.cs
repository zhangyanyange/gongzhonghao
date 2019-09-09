using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MQ.WeChat.Models
{
    public class MenuModel
    {
        public string name { get; set; }
        public List<MenuParentModel> sub_button { get; set; }
    }
}
