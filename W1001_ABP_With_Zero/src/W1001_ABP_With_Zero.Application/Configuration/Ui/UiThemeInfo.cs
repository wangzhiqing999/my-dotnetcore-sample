namespace W1001_ABP_With_Zero.Configuration.Ui
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