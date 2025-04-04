using AntDesign;
using System.Collections.Generic;

namespace W4113_AntDesignProServer.Pages.Account.Settings
{
    public partial class Index
    {
        private readonly Dictionary<string, string> _menuMap = new Dictionary<string, string>
        {
            {"base", "Basic Settings"},
            {"security", "Security Settings"},
            {"binding", "Account Binding"},
            {"notification", "New Message Notification"},
        };

        private string _selectKey = "base";

        private void SelectKey(MenuItem item)
        {
            _selectKey = item.Key;
        }
    }
}