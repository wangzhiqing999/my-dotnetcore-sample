namespace W1010_ABP_NetCode2.Configuration.Ui
{
    public class UiThemeInfo
    {
        public string Name { get; }
        public string CssClass { get; }

        public UiThemeInfo(string name, string cssClass)
        {
            Name = name;
            CssClass = cssClass;
        }
    }
}
