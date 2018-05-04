"use strict";


// 默认的配置.
var _myWorkConfig = {
	// 服务器地址.
	myServerHost : "http://localhost:24853/",
	// 区域.
	myArea : "MyWork",
	// 翻页大小.
	myPageSize : 5,
	
	// 空白查询条件.
	myEmpryQueryString : "_"
};




// 服务列表.
var _myWorkServiceList = {

	// 股票池服务.
	stockPool : new myCommonService("StockPool"),

};





// 空白数据列表. (插入时使用.)
var _myWorkEmptyData = {

	// 股票池.
	myStockPool : function() {
		return {
			// 股票池名称.
			stockPoolName : "",
		};
	},

	

};