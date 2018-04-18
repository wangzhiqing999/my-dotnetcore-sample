"use strict";

// 默认的配置.
var _defaultConfig = {
	// 服务器地址.
	myServerHost : "http://localhost:24853/",
	// 区域.
	myArea : "MyAuth",
	// 翻页大小.
	myPageSize : 5,
	
	// 空白查询条件.
	myEmpryQueryString : "_"
};

// 空白的 Web Api 地址.
var _defaultWebApiPath = {
	// 查询列表地址.
	listWebApiPath : "",
	// 查询明细地址.
	detailWebApiPath : "/Get/",
	// 创建地址.
	createWebApiPath : "/Create",
	// 更新地址.
	updateWebApiPath : "/Update",
	// 删除地址.
	deleteWebApiPath : "/Delete"
};


function myCommonService(serviceName) {

	// 服务名.
	this.serviceName = serviceName;
	// 配置信息.
	this.config = _defaultConfig;
	// Web Api 地址.
	this.webApiPath = _defaultWebApiPath;
	// 格式化服务地址.
	this._formatServiceAddress = function(path) {
		return this.config.myServerHost + "api/" + this.config.myArea + "/" + this.serviceName + path;
	}

	// 列表数据的 Web Api 地址.
	this.listWebApiAddress = function() {
		return this._formatServiceAddress(this.webApiPath.listWebApiPath);
	};
	// 单行明细数据的  Web Api 地址.
	this.detailWebApiAddress = function(id) {
		return this._formatServiceAddress(this.webApiPath.detailWebApiPath) + id;
	};
	// 创建数据的  Web Api 地址.
	this.createWebApiAddress = function() {
		return this._formatServiceAddress(this.webApiPath.createWebApiPath);
	};
	// 编辑数据的  Web Api 地址.
	this.updateWebApiAddress = function() {
		return this._formatServiceAddress(this.webApiPath.updateWebApiPath);
	};
	// 删除数据的  Web Api 地址.
	this.deleteWebApiAddress = function() {
		return this._formatServiceAddress(this.webApiPath.deleteWebApiPath);
	};

	
	// 获取查询条件.
	this.getSearchInfo = function(data) {
		if(data == _defaultConfig.myEmpryQueryString) {
			return "";
		}
		return data;
	}
	// 格式化查询条件.
	this.formatSearchInfo = function(data) {
		if(data == null || data == "") {
			return _defaultConfig.myEmpryQueryString;
		}
		return data;
	}
	
	
	// 获取列表的请求.
	this.getListRequest = function(pageIndex) {
		return {
			pageNo : pageIndex,
			pageSize : this.config.myPageSize,
		};
	};
	
	
	

	// 查询列表
	this.list = function(requestData, callback) {
		// Show Preloader
		app.preloader.show();
		Framework7.request.json(
			this.listWebApiAddress(),
			requestData,
			function (data) {
				// Hide Preloader
				app.preloader.hide();
				callback(data);
			}
		);
	};

	// 查询明细.
	this.detail = function(id, callback) {
		// Show Preloader
		app.preloader.show();
		Framework7.request.json(
			this.detailWebApiAddress(id),
			{},
			function (data) {
				// Hide Preloader
				app.preloader.hide();
				callback(data);
			}
		);
	};

	// 创建.
	this.createData = function(item, callback) {
		// Show Preloader
		app.preloader.show();
		Framework7.request.postJSON(
			this.createWebApiAddress(),
			item,
			function (data) {
				// Hide Preloader
				app.preloader.hide();
				callback(data);
			}
		);
	}

	// 编辑
	this.updateData = function(item, callback) {
		// Show Preloader
		app.preloader.show();
		Framework7.request.postJSON(
			this.updateWebApiAddress(),
			item,
			function (data) {
				// Hide Preloader
				app.preloader.hide();
				callback(data);
			}
		);
	};

	// 删除.
	this.deleteData = function(id, callback) {
		// Show Preloader
		app.preloader.show();
		Framework7.request.postJSON(
			this.deleteWebApiAddress(),
			{
				id: id
			},
			function (data) {
				// Hide Preloader
				app.preloader.hide();
				callback(data);
			}
		);
	};


	// 初始化明细画面. (明细 / 编辑 / 删除)
	this.initDetailView = function(elName, id) {
		// 当前服务.
		var _thisService = this;
		var appVue = new Vue({
			el: elName,
			data: {
				// 数据.
				dataItem : null,
				// 编辑模式.
				editMode : false,
			},
			created:function(){
			　　// ajax获取后台数据
				this.loadData();
			},
			methods:{
				// ajax 加载数据.
				loadData:function() {
					var _this = this;
					_thisService.detail(id, function (data) {
						// console.log(data);
						_this.dataItem = data.resultData;
					});
				},
				doEdit:function() {
					this.editMode = true;
				},
				cancelEdit:function() {
					this.editMode = false;
				},
				saveData:function() {
					var _this = this;
					_thisService.updateData(_this.dataItem, function (data) {
						// console.log(data);
						if(data.isSuccess) {
							app.router.navigate(_myListUrl);
						} else {
							app.dialog.alert(data.resultMessage);
						}
					});
				},
				deleteData:function() {
					var _this = this;
					app.dialog.confirm('确定要删除此数据么? 此操作不可恢复。', function () {
						_thisService.deleteData(id, function (data) {
							// console.log(data);
							if(data.isSuccess) {
								app.router.navigate(_myListUrl);
							} else {
								app.dialog.alert(data.resultMessage);
							}
						});
					});
				},
				goBack:function() {
					app.router.navigate(_myListUrl);
				}
			}
		});
	};


	// 初始化创建画面.
	this.initCreateView = function(elName, emptyData) {
		// 当前服务.
		var _thisService = this;
		var appVue = new Vue({
			el: elName,
			data: {
				// 数据.
				dataItem : emptyData
			},
			created:function(){
			},
			methods:{
				createData:function() {
					var _this = this;
					_thisService.createData(_this.dataItem, function (data) {
						// console.log(data);
						if(data.isSuccess) {
							app.router.navigate(_myListUrl);
						} else {
							app.dialog.alert(data.resultMessage);
						}
					});
				},
				cancelCreate:function() {
					app.router.navigate(_myListUrl);
				}
			}
		});
	};

}