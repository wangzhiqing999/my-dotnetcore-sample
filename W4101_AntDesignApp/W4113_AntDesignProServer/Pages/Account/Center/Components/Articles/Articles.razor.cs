using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using W4113_AntDesignProServer.Models;

namespace W4113_AntDesignProServer.Pages.Account.Center
{
    public partial class Articles
    {
        [Parameter] public IList<ListItemDataType> List { get; set; }
    }
}