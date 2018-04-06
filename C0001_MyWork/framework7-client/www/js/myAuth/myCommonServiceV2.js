// 2018年4月6日废弃记录.

// 查询功能被移动到 routes.js 处理.
// 编辑/删除 没有问题。
// 插入代码搞不好要单独一个文件.
// 结果将导致，一个功能模块的代码，分布在多个文件中.


var myCommonService = {

	// 服务器地址.
	_myServerHost : "http://localhost:24853/",


	// 初始化默认的数据列表视图.
	// 默认列表视图 没有查询条件， 只有简单的翻页.
	initDefaultDataListView : function(elName, webApiPath) {
		var appVue = new Vue({
			el: elName,
			data: {
				// 数据列表.
				dataList:[],
				// 第几页.
				pageIndex:1,
				// 共几页.
				pageCount:1,
				// 显示分页标志.
				showPageInfo:true,
			},
			created:function(){
			　　// ajax获取后台数据
				this.loadData();
			},
			methods:{
				// 翻页
				pageUp:function() {
					// 上一页.
					if(this.pageIndex > 0) {
						this.pageIndex--;
						this.loadData();
						document.body.scrollTop = 0;
					}
				},
				pageDown:function() {
					// 下一页.
					// 上一页.
					if(this.pageIndex < this.pageCount) {
						this.pageIndex++;
						this.loadData();
						document.body.scrollTop = 0;
					}
				},

				// ajax 加载数据.
				loadData:function() {
					var _this = this;
					// Show Preloader
					app.preloader.show();
					var apiUrl = myCommonService._myServerHost + webApiPath;
					Framework7.request.json(
						apiUrl,
						{
							pageNo : _this.pageIndex,
							pageSize : 5
						},
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
	},




	// 初始化默认的数据明细视图.
	initDefaultDataDetailView : function(elName, queryWebApiPath, updateWebApiPath, deleteWebApiPath) {
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
					// Show Preloader
					app.preloader.show();
					var apiUrl = myCommonService._myServerHost + queryWebApiPath;
					Framework7.request.json(
						apiUrl,
						{},
						function (data) {
							// Hide Preloader
							app.preloader.hide();
							// console.log(data);
							_this.dataItem = data.resultData;
						}
					);
				},
				doEdit:function() {
					this.editMode = true;
				},
				cancelEdit:function() {
					this.editMode = false;
				},
				saveData:function() {
					var _this = this;
					// Show Preloader
					app.preloader.show();
					var apiUrl = myCommonService._myServerHost + updateWebApiPath;
					Framework7.request.postJSON(
						apiUrl,
						_this.dataItem,
						function (data) {
							// Hide Preloader
							app.preloader.hide();
							// console.log(data);
							if(data.isSuccess) {
								app.router.navigate(_myListUrl);
							} else {
								app.dialog.alert(data.resultMessage);
							}
						}
					);
				},
				deleteData:function() {
					var _this = this;
					app.dialog.confirm('确定要删除此数据么? 此操作不可恢复。', function () {
						// Show Preloader
						app.preloader.show();
						var apiUrl = myCommonService._myServerHost + deleteWebApiPath;
						Framework7.request.postJSON(
							apiUrl,
							{
								id : _this.dataItem.primaryKey
							},
							function (data) {
								// Hide Preloader
								app.preloader.hide();
								// console.log(data);
								if(data.isSuccess) {
									app.router.navigate(_myListUrl);
								} else {
									app.dialog.alert(data.resultMessage);
								}
							}
						);
					});
				},
				goBack:function() {
					app.router.navigate(_myListUrl);
				}
			}
		});
	},



	// 初始化默认的数据编辑视图.
	// 默认数据编辑视图，只有单独的主键.
	initDefaultDataEditView : function(elName, queryWebApiPath, saveWebApiPath) {
		var appVue = new Vue({
			el: elName,
			data: {
				// 数据.
				dataItem : null
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
					var apiUrl = myCommonService._myServerHost + queryWebApiPath;
					Framework7.request.json(
						apiUrl,
						{},
						function (data) {
							// Hide Preloader
							app.preloader.hide();
							// console.log(data);
							_this.dataItem = data.resultData;
						}
					);
				},
				saveData:function() {
					var _this = this;
					// Show Preloader
					app.preloader.show();

					var apiUrl = myCommonService._myServerHost + saveWebApiPath;
					Framework7.request.postJSON(
						apiUrl,
						_this.dataItem,
						function (data) {
							// Hide Preloader
							app.preloader.hide();
							console.log(data);

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
	}


}