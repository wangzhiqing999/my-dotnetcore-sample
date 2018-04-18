"use strict";

// 角色服务.
var myRoleService = _myServiceList.myRole;




// 初始化 列表画面.
myRoleService.initListView = function(elName, pageNo, systemCode) {

	var _thisService = this;

	// 当前服务.
	var appVue = new Vue({
		el: elName,
		data: {
			// 数据列表.
			dataList:[],
			// 第几页.
			pageIndex:pageNo,
			// 共几页.
			pageCount:1,

			// 查询的系统代码.
			searchSystemCode:systemCode
		},
		// 计算值.
		computed: {
			// 额外的路径.
			expPath: function () {
				return '/' + this.searchSystemCode + '/';
			}
		},		
		created:function(){
		　　// ajax获取后台数据
			this.loadData();
		},
		methods:{
			// ajax 加载数据.
			loadData:function() {
				var _this = this;
				// Show Preloader
				app.preloader.show();
				var apiUrl = _thisService.listWebApiAddress();
				var apiData = _thisService.getListRequest(_this.pageIndex);
				
				apiData.systemCode = _thisService.getSearchInfo(_this.searchSystemCode);
				
				Framework7.request.json(
					apiUrl,
					apiData,
					function (data) {
						// Hide Preloader
						app.preloader.hide();
						_this.dataList=[];
						var rowCount = data.queryResultData.length;
						for(var i = 0; i < rowCount; i ++) {
							_this.dataList.push(data.queryResultData[i]);
						}
						// 第几页.
						_this.pageIndex = data.queryPageInfo.pageIndex;
						// 共几页.
						_this.pageCount = data.queryPageInfo.pageCount;
					}
				);
			}
		}
	});
};



// 打开查询面板.
myRoleService.initSearchView = function(elName) {
	var _thisService = this;
	// 当前服务.
	var appVue = new Vue({
		el: elName,
		data: {
			// 系统代码.
			systemCode:""
		},
		methods:{
			doSearch: function() {
				var queryUrl = "/MyAuth/MyRole/List/1/" 
					+ _thisService.formatSearchInfo(this.systemCode) + "/";
				app.router.navigate(queryUrl);
			}
		}
	});
}




// 初始化 角色-模块管理画面.
myRoleService.initEditModuleView = function(elName, id) {

	var _thisService = this;

	// 当前服务.
	var appVue = new Vue({
		el: elName,
		data: {
			// 角色代码.
			roleCode : id,
			// 数据.
			dataList : [],
			// 全部模块可访问.
			allModuleAccessAble : false,
			// 全部可访问模块的动作可访问.
			allActionAccessAble : false,
		},
		created:function(){
		　　// ajax获取后台数据
			this.loadData();
		},
		methods:{
			// 所有模块可访问标志发生变化.
			allModuleAccessAbleChange: function() {
				// 如果是取消全部模块权限的情况下. 需要取消全部动作.
				if(this.allModuleAccessAble === false) {
					this.allActionAccessAble = false;
					this.allActionAccessAbleChange();
				}
				// 全部变更模块.
				var len = this.dataList.length;
				for(var i = 0; i < len; i ++) {
					this.dataList[i].accessAble = this.allModuleAccessAble;
				}
			},
			allActionAccessAbleChange: function() {
				// 遍历全部模块.
				var len = this.dataList.length;
				for(var i = 0; i < len; i ++) {
					// 仅仅当模块选择的情况下，处理动作.
					if(this.dataList[i].accessAble === true) {
						var len2 = this.dataList[i].actions.length;
						for(var j = 0; j < len2; j ++) {
							this.dataList[i].actions[j].accessAble = this.allActionAccessAble;
						}
					}
				}
			},
			oneModuleAccessAbleChange: function(module) {
				if(module.accessAble === false) {
					// 模块不可用的情况下， 需要把动作自动不可用.
					var len2 = module.actions.length;
						for(var j = 0; j < len2; j ++) {
							module.actions[j].accessAble = false;
						}
				}
			},
			// ajax 加载数据.
			loadData:function() {
				var _this = this;
				var loadUrl = _thisService.config.myServerHost + "api/MyAuth/MyRole/GetManagerAbleModule/" + _this.roleCode;
				// Show Preloader
				app.preloader.show();
				Framework7.request.json(
					loadUrl,
					{},
					function (data) {
						console.log(data);
						// Hide Preloader
						app.preloader.hide();
						_this.dataList = data;
					}
				);
			},
			saveData:function() {
				var _this = this;
				var saveUrl = _thisService.config.myServerHost + "api/MyAuth/MyRole/UpdateManagerAbleModule/" + _this.roleCode;
				// Show Preloader
				app.preloader.show();
				Framework7.request.postJSON(
					saveUrl,
					_this.dataList,
					function (data) {
						// Hide Preloader
						app.preloader.hide();
						if(data.isSuccess) {
							app.router.navigate(_myListUrl);
						} else {
							app.dialog.alert(data.resultMessage);
						}
					}
				);
			},
			goBack:function() {
				app.router.navigate(_myListUrl);
			}
		}
	});
};