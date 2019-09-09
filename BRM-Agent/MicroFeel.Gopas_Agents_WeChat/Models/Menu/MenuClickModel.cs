namespace MicroFeel.Pramy.Agent.Models.Menu
{
    public class MenuClickModel : MenuParentModel
    {
        public override string type { get; set; } = "click";
        public string key { get; set; }
    }
}
