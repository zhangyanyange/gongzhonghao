using System.Collections.Generic;

namespace MicroFeel.Pramy.Agent.Models.Menu
{
    public class MenuModel
    {
        public string name { get; set; }
        public List<MenuParentModel> sub_button { get; set; }
    }
}
