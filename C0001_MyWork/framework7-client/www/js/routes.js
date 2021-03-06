// 路由的文档， 参考： http://www.framework7.cn/docs/routes.html


// 默认的路由切换选项.
var _defaultRouteOptions = {
	animate: false,
	history: false,
	pushState: false,
	reloadAll: true,
};

// 列表页面URL地址.
var _myListUrl = "";

// 前往列表页面.
function gotoListPage() {
	app.router.navigate(_myListUrl);
}


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


	// ###### 模块类型 ######
	{
		path: '/MyAuth/MyModuleType/',
		redirect: '/MyAuth/MyModuleType/List/1/'
	},
	{
		path: '/MyAuth/MyModuleType/List/:pageNo/',
		options: _defaultRouteOptions,
		async: function (routeTo, routeFrom, resolve, reject) {
			_myListUrl = routeTo.url;
			var pageNo = routeTo.params.pageNo;
			var requestData = _myServiceList.myModuleType.getListRequest(pageNo);
			_myServiceList.myModuleType.list(requestData, function(data) {
				resolve(
						{
							componentUrl: './pages/MyAuth/MyModuleType/list.html',
						},
						{
							context: data,
						}
					);
			});
		}
	},


	// ###### 模块 ######
	{
		path: '/MyAuth/MyModule/',
		redirect: '/MyAuth/MyModule/List/1/' + _defaultConfig.myEmpryQueryString + '/' + _defaultConfig.myEmpryQueryString + '/'
	},
	{
		path: '/MyAuth/MyModule/Search/',
		url: './pages/MyAuth/MyModule/search.html',
		on: {
			pageInit: function (e, page) {
				_myServiceList.myModule.initSearchView('#modulePageSearch');
			}
		}
	},
	{
		path: '/MyAuth/MyModule/List/:pageNo/:systemCode/:moduleType/',
		url: './pages/MyAuth/MyModule/listV2.html',
		options: _defaultRouteOptions,
		on: {
			pageInit: function (e, page) {
				_myListUrl = page.route.path;
				_myServiceList.myModule.initListView(
					'#modulePageList',
					page.route.params.pageNo,
					page.route.params.systemCode,
					page.route.params.moduleType);
			}
		}

		/*
		async: function (routeTo, routeFrom, resolve, reject) {
			_myListUrl = routeTo.url;
			var pageNo = routeTo.params.pageNo;
			var requestData = _myServiceList.myModule.getListRequest(pageNo);
			_myServiceList.myModule.list(requestData, function(data) {
				resolve(
						{
							componentUrl: './pages/MyAuth/MyModule/list.html',
						},
						{
							context: data,
						}
					);
			});
		}
		*/
	},

	// ###### 组织机构 ######
	{
		path: '/MyAuth/MyOrganization/',
		redirect: '/MyAuth/MyOrganization/List/1/'
	},
	{
		path: '/MyAuth/MyOrganization/List/:pageNo/',
		options: _defaultRouteOptions,
		async: function (routeTo, routeFrom, resolve, reject) {
			_myListUrl = routeTo.url;
			var pageNo = routeTo.params.pageNo;
			var requestData = _myServiceList.myOrganization.getListRequest(pageNo);
			_myServiceList.myOrganization.list(requestData, function(data) {
				resolve(
						{
							componentUrl: './pages/MyAuth/MyOrganization/list.html',
						},
						{
							context: data,
						}
					);
			});
		}
	},
	{
		path: '/MyAuth/MyOrganization/Detail/:organizationId',
		url: './pages/MyAuth/MyOrganization/detail.html',
		on: {
			pageBeforeIn: function (e, page) {
				_myServiceList.myOrganization.initDetailView('#organizationPageDetail', page.route.params.organizationId);
			}
		}
	},
	{
		path: '/MyAuth/MyOrganization/Create/',
		url: './pages/MyAuth/MyOrganization/create.html',
		on: {
			pageBeforeIn: function (e, page) {
				var data = _myEmptyData.myOrganization();
				_myServiceList.myOrganization.initCreateView('#organizationPageCreate', data);
			}
		}
	},


	// ###### 角色 ######
	{
		path: '/MyAuth/MyRole/',
		redirect: '/MyAuth/MyRole/List/1/' + _defaultConfig.myEmpryQueryString + '/'
	},
	{
		path: '/MyAuth/MyRole/Search/',
		url: './pages/MyAuth/MyRole/search.html',
		on: {
			pageInit: function (e, page) {
				_myServiceList.myRole.initSearchView('#rolePageSearch');
			}
		}
	},
	{
		path: '/MyAuth/MyRole/List/:pageNo/:systemCode/',
		url: './pages/MyAuth/MyRole/listV2.html',
		options: _defaultRouteOptions,
		on: {
			pageInit: function (e, page) {
				_myListUrl = page.route.path;
				_myServiceList.myRole.initListView(
					'#rolePageList',
					page.route.params.pageNo,
					page.route.params.systemCode);
			}
		}


		/*
		async: function (routeTo, routeFrom, resolve, reject) {
			_myListUrl = routeTo.url;
			var pageNo = routeTo.params.pageNo;
			var requestData = _myServiceList.myRole.getListRequest(pageNo);
			_myServiceList.myRole.list(requestData, function(data) {
				resolve(
						{
							componentUrl: './pages/MyAuth/MyRole/list.html',
						},
						{
							context: data,
						}
					);
			});
		}
		*/
	},
	{
		path: '/MyAuth/MyRole/Detail/:roleCode',
		url: './pages/MyAuth/MyRole/detail.html',
		on: {
			pageBeforeIn: function (e, page) {
				_myServiceList.myRole.initDetailView('#rolePageDetail', page.route.params.roleCode);
			}
		}
	},
	{
		path: '/MyAuth/MyRole/Module/:roleCode',
		url: './pages/MyAuth/MyRole/editModule.html',
		on: {
			pageBeforeIn: function (e, page) {
				_myServiceList.myRole.initEditModuleView('#rolePageModule', page.route.params.roleCode);
			}
		}
	},
	{
		path: '/MyAuth/MyRole/Create/',
		url: './pages/MyAuth/MyRole/create.html',
		on: {
			pageBeforeIn: function (e, page) {
				var data = _myEmptyData.myRole();
				_myServiceList.myRole.initCreateView('#rolePageCreate', data);
			}
		}
	},


	// ###### 用户 ######
	{
		path: '/MyAuth/MyUser/',
		redirect: '/MyAuth/MyUser/List/1/' + _defaultConfig.myEmpryQueryString + '/'
	},
	{
		path: '/MyAuth/MyUser/Search/',
		url: './pages/MyAuth/MyUser/search.html',
		on: {
			pageInit: function (e, page) {
				_myServiceList.myUser.initSearchView('#userPageSearch');
			}
		}
	},
	{
		path: '/MyAuth/MyUser/List/:pageNo/:organizationID/',
		url: './pages/MyAuth/MyUser/listV2.html',
		options: _defaultRouteOptions,
		on: {
			pageInit: function (e, page) {
				_myListUrl = page.route.path;
				_myServiceList.myUser.initListView(
					'#userPageList',
					page.route.params.pageNo,
					page.route.params.organizationID);
			}
		}

		/*
		async: function (routeTo, routeFrom, resolve, reject) {
			_myListUrl = routeTo.url;
			var pageNo = routeTo.params.pageNo;
			var requestData = _myServiceList.myUser.getListRequest(pageNo);
			_myServiceList.myUser.list(requestData, function(data) {
				resolve(
						{
							componentUrl: './pages/MyAuth/MyUser/list.html',
						},
						{
							context: data,
						}
					);
			});
		}
		*/
	},
	{
		path: '/MyAuth/MyUser/Detail/:userID',
		url: './pages/MyAuth/MyUser/detail.html',
		on: {
			pageBeforeIn: function (e, page) {
				_myServiceList.myUser.initDetailView('#userPageDetail', page.route.params.userID);
			}
		}
	},
	{
		path: '/MyAuth/MyUser/System/:userID',
		url: './pages/MyAuth/MyUser/editSystem.html',
		on: {
			pageBeforeIn: function (e, page) {
				_myServiceList.myUser.initEditSystemView('#userPageSystem', page.route.params.userID);
			}
		}
	},
	{
		path: '/MyAuth/MyUser/Create/',
		url: './pages/MyAuth/MyUser/create.html',
		on: {
			pageBeforeIn: function (e, page) {
				var data = _myEmptyData.myUser();
				_myServiceList.myUser.initCreateView('#userPageCreate', data);
			}
		}
	},

	
	
	
	
	

	// ###### 股票池 ######
	{
		path: '/MyWork/StockPool/',
		redirect: '/MyWork/StockPool/List/1/'
	},	
	{
		path: '/MyWork/StockPool/List/:pageNo/',
		url: './pages/MyWork/StockPool/listV2.html',
		options: _defaultRouteOptions,
		on: {
			pageInit: function (e, page) {
				_myListUrl = page.route.path;
				_myWorkServiceList.stockPool.initListView(
					'#stockPoolPageList',
					page.route.params.pageNo);
			}
		}
	},
	{
		path: '/MyWork/StockPool/Create/',
		url: './pages/MyWork/StockPool/create.html',
		on: {
			pageBeforeIn: function (e, page) {
				var data = _myWorkEmptyData.myStockPool();
				_myWorkServiceList.stockPool.initCreateView('#stockPoolPageCreate', data);
			}
		}
	},
	{
		path: '/MyWork/StockPool/Detail/:stockPoolID',
		url: './pages/MyWork/StockPool/detail.html',
		on: {
			pageBeforeIn: function (e, page) {
				_myWorkServiceList.stockPool.initDetailView('#stockPoolPageDetail', page.route.params.stockPoolID);
			}
		}
	},
	{
		path: '/MyWork/StockPool/Stock/:stockPoolID',
		url: './pages/MyWork/StockPool/stock.html',
		on: {
			pageBeforeIn: function (e, page) {
				// _myWorkServiceList.stockPool.initDetailView('#stockPoolPageDetail', page.route.params.stockPoolID);
			}
		}
	},	

	
	
	
	// Default route (404 page). MUST BE THE LAST
	{
		path: '(.*)',
		url: './pages/404.html',
	},
];
