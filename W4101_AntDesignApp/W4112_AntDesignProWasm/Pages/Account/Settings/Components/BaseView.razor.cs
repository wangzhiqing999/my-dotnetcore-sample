using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using W4112_AntDesignProWasm.Models;
using W4112_AntDesignProWasm.Services;

namespace W4112_AntDesignProWasm.Pages.Account.Settings
{
    public partial class BaseView
    {
        private CurrentUser _currentUser = new CurrentUser();

        [Inject] protected IUserService UserService { get; set; }

        private void HandleFinish()
        {
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            _currentUser = await UserService.GetCurrentUserAsync();
        }
    }
}