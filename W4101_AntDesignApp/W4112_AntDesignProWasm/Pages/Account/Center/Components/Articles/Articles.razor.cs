using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using W4112_AntDesignProWasm.Models;

namespace W4112_AntDesignProWasm.Pages.Account.Center
{
    public partial class Articles
    {
        [Parameter] public IList<ListItemDataType> List { get; set; }
    }
}