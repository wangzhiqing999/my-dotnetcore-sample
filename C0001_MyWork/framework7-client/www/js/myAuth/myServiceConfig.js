

// 服务列表.
var _myServiceList = {
	// 模块类型服务.
	myModuleType : new myCommonService("MyModuleType"),
	// 模块.
	myModule : new myCommonService("MyModule"),
	// 组织机构服务.
	myOrganization : new myCommonService("MyOrganization"),
	// 角色服务.
	myRole : new myCommonService("MyRole"),
	// 用户服务.
	myUser : new myCommonService("MyUser"),
};






// 空白数据列表. (插入时使用.)
var _myEmptyData = {

	// 组织机构.
	myOrganization : function() {
		return {
			// 组织机构代码.
			loginOrganizationCode : "",
			// 组织机构名称.
			organizationName : "",
		};
	},

	// 角色.
	myRole : function() {
		return {
			// 角色代码.
			roleCode : "",
			// 角色名称.
			roleName : "",
			// 系统代码.
			systemCode : "",
		};
	}

};