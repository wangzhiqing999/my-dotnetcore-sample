using AntDesign;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using W4113_AntDesignProServer.Models;

namespace W4113_AntDesignProServer.Pages.Account.Center
{
    public partial class Projects
    {
        private readonly ListGridType _listGridType = new ListGridType
        {
            Gutter = 24,
            Column = 4
        };

        [Parameter]
        public IList<ListItemDataType> List { get; set; }
    }
}