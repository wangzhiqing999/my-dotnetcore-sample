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
	}
	
	
}