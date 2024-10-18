using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using W4113_AntDesignProServer.Models;
using W4113_AntDesignProServer.Services;

namespace W4113_AntDesignProServer.Pages.Profile
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