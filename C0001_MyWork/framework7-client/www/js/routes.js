// 路由的文档， 参考： http://www.framework7.cn/docs/routes.html

routes = [
	{
		path: '/',
		url: './index.html',
	},
	{
		name: 'about',
		path: '/about/',
		url: './pages/about.html',
		on: {
			pageBeforeIn: function (event, page) {
				// do something before page gets into the view
			},
			pageAfterIn: function (e, page) {
				// do something after page gets into the view
			},
			pageInit: function (e, page) {
				// do something when page initialized
			},
			pageBeforeRemove: function (event, page) {
				// do something before page gets removed from DOM
			},
		}
	},

	// 模块类型.
	{
		path: '/MyAuth/MyModuleType/',
		url: './pages/MyAuth/MyModuleType.html',
		on: {
			pageInit: function (e, page) {
				myCommonService.initDefaultDataListView('#moduleTypePage', 'api/MyAuth/MyModuleType');
			}
		}
	},

	// 模块
	{
		path: '/MyAuth/MyModule/',
		url: './pages/MyAuth/MyModule.html',
		on: {
			pageInit: function (e, page) {
				myCommonService.initDefaultDataListView('#modulePage', 'api/MyAuth/MyModule');
			}
		}
	},

	// 组织机构
	{
		path: '/MyAuth/MyOrganization/',
		url: './pages/MyAuth/MyOrganization.html',
		on: {
			pageInit: function (e, page) {
				myCommonService.initDefaultDataListView('#organizationPage', 'api/MyAuth/MyOrganization');
			}
		}
	},

	// 角色
	{
		path: '/MyAuth/MyRole/',
		url: './pages/MyAuth/MyRole.html',
		on: {
			pageInit: function (e, page) {
				myCommonService.initDefaultDataListView('#rolePage', 'api/MyAuth/MyRole');
			}
		}
	},

	// 用户.
	{
		path: '/MyAuth/MyUser/',
		url: './pages/MyAuth/MyUser.html',
		on: {
			pageInit: function (e, page) {
				myCommonService.initDefaultDataListView('#userPage', 'api/MyAuth/MyUser');
			}
		}
	},

	// Default route (404 page). MUST BE THE LAST
	{
		path: '(.*)',
		url: './pages/404.html',
	},
];
