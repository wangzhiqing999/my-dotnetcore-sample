
var myServerHost = "http://localhost:24853/";

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
		async: function (routeTo, routeFrom, resolve, reject) {
			// Router instance
			var router = this;
			// App instance
			var app = router.app;
			// Show Preloader
			app.preloader.show();
			var apiUrl = myServerHost + 'api/MyAuth/MyModuleType';
			app.request.json(
				apiUrl,
				{ },
				function (data) {
					// Hide Preloader
					app.preloader.hide();
					resolve(
						{
							componentUrl: './pages/MyAuth/MyModuleType.html',
						},
						{
							context: {
								moduleTypeData: data,
							}
						}
					);
				},
				function () {
					// Hide Preloader
					app.preloader.hide();
				});
		}
	},

	// 模块
	{
		path: '/MyAuth/MyModule/',
		async: function (routeTo, routeFrom, resolve, reject) {
			// Router instance
			var router = this;
			// App instance
			var app = router.app;
			// Show Preloader
			app.preloader.show();
			var apiUrl = myServerHost + 'api/MyAuth/MyModule';
			app.request.json(
				apiUrl,
				{ },
				function (data) {
					// Hide Preloader
					app.preloader.hide();
					resolve(
						{
							componentUrl: './pages/MyAuth/MyModule.html',
						},
						{
							context: {
								moduleData: data,
							}
						}
					);
				},
				function () {
					// Hide Preloader
					app.preloader.hide();
				});
		}
	},



	// 组织机构
	{
		path: '/MyAuth/MyOrganization/',
		async: function (routeTo, routeFrom, resolve, reject) {
			// Router instance
			var router = this;
			// App instance
			var app = router.app;
			// Show Preloader
			app.preloader.show();
			var apiUrl = myServerHost + 'api/MyAuth/MyOrganization';
			app.request.json(
				apiUrl,
				{ },
				function (data) {
					// Hide Preloader
					app.preloader.hide();
					resolve(
						{
							componentUrl: './pages/MyAuth/MyOrganization.html',
						},
						{
							context: {
								resultData: data,
							}
						}
					);
				},
				function () {
					// Hide Preloader
					app.preloader.hide();
				});
		}
	},

	// 角色
	{
		path: '/MyAuth/MyRole/',
		async: function (routeTo, routeFrom, resolve, reject) {
			// Router instance
			var router = this;
			// App instance
			var app = router.app;
			// Show Preloader
			app.preloader.show();
			var apiUrl = myServerHost + 'api/MyAuth/MyRole';
			app.request.json(
				apiUrl,
				{ },
				function (data) {
					// Hide Preloader
					app.preloader.hide();
					resolve(
						{
							componentUrl: './pages/MyAuth/MyRole.html',
						},
						{
							context: {
								roleData: data,
							}
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
