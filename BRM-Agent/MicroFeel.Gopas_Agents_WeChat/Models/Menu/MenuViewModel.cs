namespace MicroFeel.Pramy.Agent.Models.Menu
{
    public class MenuViewModel : MenuParentModel
    {
        public override string type { get; set; } = "view";
        public string url { get; set; }
    }
}
