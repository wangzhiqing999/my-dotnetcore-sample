using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using W4112_AntDesignProWasm.Models;
using W4112_AntDesignProWasm.Services;

namespace W4112_AntDesignProWasm.Pages.Profile
{
    public partial class Basic
    {
        private BasicProfileDataType _data = new BasicProfileDataType();

        [Inject] protected IProfileService ProfileService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            _data = await ProfileService.GetBasicAsync();
        }
    }
}