"use strict";

// 用户服务.
var myModuleService = _myServiceList.myModule;


// 初始化 列表画面.
myModuleService.initListView = function(elName, pageNo, systemCode, moduleTypeCode) {

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
			// 显示分页标志.
			showPageInfo:true,
			
			// 查询的系统代码.
			searchSystemCode:systemCode,
			// 查询的模块类型.
			searchModuleType:moduleTypeCode,
		},
		// 计算值.
		computed: {
			// 额外的路径.
			expPath: function () {
				return '/' + this.searchSystemCode + '/' + this.searchModuleType + '/';
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
				apiData.moduleType = _thisService.getSearchInfo(_this.moduleTypeCode);
				
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

						if(_this.pageCount == 1) {
							// 仅有1页的情况下， 不显示翻页.
							_this.showPageInfo = false;
						} else {
							_this.showPageInfo = true;
						}
					}
				);
			}
		}
	});
};



// 打开查询面板.
myModuleService.initSearchView = function(elName) {
	var _thisService = this;
	// 当前服务.
	var appVue = new Vue({
		el: elName,
		data: {
			// 系统代码.
			systemCode:"",
			// 模块类型代码.
			moduletypeCode:""
		},
		methods:{
			doSearch: function() {
				var queryUrl = "/MyAuth/MyModule/List/1/" 
					+ _thisService.formatSearchInfo(this.systemCode) + "/" 
					+ _thisService.formatSearchInfo(this.moduletypeCode) + "/";
				app.router.navigate(queryUrl);
			}
		}
	});
}


