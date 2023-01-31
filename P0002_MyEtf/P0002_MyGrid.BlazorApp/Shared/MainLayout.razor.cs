
using BootstrapBlazor.Components;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.JSInterop;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using static System.Net.Mime.MediaTypeNames;

using P0002_MyEtf.Service;
using P0002_MyGrid.Service;

namespace P0002_MyGrid.BlazorApp.Shared
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class MainLayout
    {
        private bool UseTabSet { get; set; } = true;

        private string Theme { get; set; } = "";

        private bool IsOpen { get; set; }

        private bool IsFixedHeader { get; set; } = true;

        private bool IsFixedFooter { get; set; } = true;

        private bool IsFullSide { get; set; } = true;

        private bool ShowFooter { get; set; } = true;


        [Inject]
        [NotNull]
        IJSRuntime? JS { set; get; }


        /// <summary>
        /// 登录状态.
        /// </summary>
        [Inject]
        [NotNull]
        private AuthenticationStateProvider? AuthenticationStateProvider { set; get; }




        /// <summary>
        /// 
        /// </summary>
        [Inject]
        [NotNull]
        private WebClientService? WebClientService { get; set; }

        [Inject]
        [NotNull]
        private NavigationManager? NavigationManager { get; set; }


        [Inject]
        [NotNull]
        private IIPLocatorProvider? IPLocatorProvider { get; set; }



        [Inject]
        [NotNull]
        private IEtfMasterService? _EtfMasterService { get; set; }



        [Inject]
        [NotNull]
        private IGridService? _GridService { get; set; }


        /// <summary>
        /// 菜单数据.
        /// </summary>
        private List<MenuItem>? Menus { get; set; }



        
        /// <summary>
        /// 存储会话数据.
        /// </summary>
        [Inject]
        [NotNull]
        private ProtectedSessionStorage? _ProtectedSessionStore { set; get; }




        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            // 本地开发测试时使用.
            Menus = GetIconSideMenuItems();
        }





        private List<MenuItem> GetIconSideMenuItems()
        {

    

            List<string> itemCodeList = this._GridService.GetItemCodes();

            var gridMenus = new List<MenuItem>();
            foreach (string itemCode in itemCodeList)
            {
                var etfMaster = this._EtfMasterService.GetEtfMaster(itemCode);

                MenuItem etfMenu = new MenuItem()
                {
                    Text = etfMaster.EtfName,
                    Icon = "fa-solid fa-fw fa-flag",
                    Url = $"/gridinfo/{itemCode}"
                };

                gridMenus.Add(etfMenu);
            }




            var menus = new List<MenuItem>
            {
                new MenuItem() { Text = "Index", Icon = "fa-solid fa-fw fa-flag", Url = "/" , Match = NavLinkMatch.All},

                new MenuItem() { 
                    Text = "MyGrid", 
                    Icon = "fa-solid fa-fw fa-check-square",

                    Items = gridMenus
                },


                new MenuItem() { Text = "持仓", Icon = "fa-solid fa-fw fa-flag", Url = "/holdinginfo"},

            };

         


            return menus;
        }















    }
}