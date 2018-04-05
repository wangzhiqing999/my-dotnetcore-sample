// 路由的文档， 参考： http://www.framework7.cn/docs/routes.html


// 服务器地址.
var _myServerHost = "http://localhost:24853/";
// 翻页大小.
var _myPageSize = 5;
// 列表页面URL地址.
var _myListUrl = "";

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
			pageMounted: function (e, page) {
				console.log('page mounted');
			},
			pageInit: function (e, page) {
				console.log("pageInit .");
			},
			pageBeforeIn: function (e, page) {
				console.log('page before in');
			},
			pageAfterIn: function (e, page) {
				console.log('page after in');
			},
			pageBeforeOut: function (e, page) {
				console.log('page before out');
			},
			pageAfterOut: function (e, page) {
				console.log('page after out');
			},
			pageBeforeRemove: function (e, page) {
				console.log('page before remove');
			}
		}
	},

	// 模块类型.
	{
		path: '/MyAuth/MyModuleType/',
		url: './pages/MyAuth/MyModuleType.html',
		on: {
			pageBeforeIn: function (e, page) {
				myCommonService.initDefaultDataListView('#moduleTypePage', 'api/MyAuth/MyModuleType');
			}
		}
	},

	// 模块
	{
		path: '/MyAuth/MyModule/',
		url: './pages/MyAuth/MyModule.html',
		on: {
			pageBeforeIn: function (e, page) {
				myCommonService.initDefaultDataListView('#modulePage', 'api/MyAuth/MyModule');
			}
		}
	},

	// ######  组织机构 ######
	{
		path: '/MyAuth/MyOrganization/:pageNo/',
		options: {
			animate: false,
			history: false,
			pushState: false,
			reloadAll: true,
		},
		async: function (routeTo, routeFrom, resolve, reject) {
			_myListUrl = routeTo.url;
			var router = this;
			var app = router.app;
			app.preloader.show();
			var apiUrl = _myServerHost + 'api/MyAuth/MyOrganization';
			var pageNo = routeTo.params.pageNo;
			app.request.json(
				apiUrl,
				{
					pageNo : pageNo,
					pageSize : _myPageSize
				},
				function (data) {
					app.preloader.hide();
					resolve(
						{
							componentUrl: './pages/MyAuth/MyOrganization.html',
						},
						{
							context: data,
						}
					);
				},
				function () {
					// Hide Preloader
					app.preloader.hide();
				});
		}
	},
	{
		path: '/MyAuth/MyOrganization/Detail/:organizationId',
		url: './pages/MyAuth/MyOrganization/detail.html',
		on: {
			pageBeforeIn: function (e, page) {
				// console.log("pageInit:", e, page);
				myCommonService.initDefaultDataDetailView(
					'#organizationPageDetail',
					'api/MyAuth/MyOrganization/Get/' + page.route.params.organizationId,
					'api/MyAuth/MyOrganization/Update',
					'api/MyAuth/MyOrganization/Delete'
					);
			}
		}
	},


	// 角色
	{
		path: '/MyAuth/MyRole/:pageNo/',
		options: {
			animate: false,
			history: false,
			pushState: false,
			reloadAll: true,
		},
		async: function (routeTo, routeFrom, resolve, reject) {
			var router = this;
			var app = router.app;
			app.preloader.show();
			var apiUrl = _myServerHost + 'api/MyAuth/MyRole';
			var pageNo = routeTo.params.pageNo;
			app.request.json(
				apiUrl,
				{
					pageNo : pageNo,
					pageSize : _myPageSize
				},
				function (data) {
					app.preloader.hide();
					resolve(
						{
							componentUrl: './pages/MyAuth/MyRole.html',
						},
						{
							context: data,
						}
					);
				},
				function () {
					// Hide Preloader
					app.preloader.hide();
				});
		}
	},

	// 用户.
	{
		path: '/MyAuth/MyUser/:pageNo/',
		options: {
			animate: false,
			history: false,
			pushState: false,
			reloadAll: true,
		},
		async: function (routeTo, routeFrom, resolve, reject) {
			var router = this;
			var app = router.app;
			app.preloader.show();
			var apiUrl = _myServerHost + 'api/MyAuth/MyUser';
			var pageNo = routeTo.params.pageNo;
			app.request.json(
				apiUrl,
				{
					pageNo : pageNo,
					pageSize : _myPageSize
				},
				function (data) {
					app.preloader.hide();
					resolve(
						{
							componentUrl: './pages/MyAuth/MyUser.html',
						},
						{
							context: data,
						}
					);
				},
				function () {
					// Hide Preloader
					app.preloader.hide();
				});
		}
	},

	// Default route (404 page). MUST BE THE LAST
	{
		path: '(.*)',
		url: './pages/404.html',
	},
];
