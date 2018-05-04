"use strict";

// 股票池服务.
var stockPoolService = _myWorkServiceList.stockPool;

// 服务配置.
stockPoolService.config = _myWorkConfig;


// 初始化 列表画面.
stockPoolService.initListView = function(elName, pageNo) {

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


