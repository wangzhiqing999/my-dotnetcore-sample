using AntDesign;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using W4112_AntDesignProWasm.Models;

namespace W4112_AntDesignProWasm.Pages.Account.Center
{
    public partial class Applications
    {
        private readonly ListGridType _listGridType = new ListGridType
        {
            Gutter = 24,
            Column = 4
        };

        [Parameter] public IList<ListItemDataType> List { get; set; }
    }
}