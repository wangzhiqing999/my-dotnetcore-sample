"use strict";

// 用户服务.
var myUserService = _myServiceList.myUser;


// 初始化 用户-系统管理画面.
myUserService.initEditSystemView = function(elName, id) {

	var _thisService = this;

	// 当前服务.
	var appVue = new Vue({
		el: elName,
		data: {
			// 用户代码.
			userID : id,
			// 数据.
			dataList : [],
			// 全部系统可访问.
			allSystemAccessAble : false,
			// 全部可访问系统的角色可访问.
			allRoleAccessAble : false,
		},
		created:function(){
		　　// ajax获取后台数据
			this.loadData();
		},
		methods:{
			// 所有系统可访问标志发生变化.
			allSystemAccessAbleChange: function() {
				// 如果是取消全部系统权限的情况下. 需要取消全部角色.
				if(this.allSystemAccessAble === false) {
					this.allRoleAccessAble = false;
					this.allRoleAccessAbleChange();
				}
				// 全部变更系统.
				var len = this.dataList.length;
				for(var i = 0; i < len; i ++) {
					this.dataList[i].accessAble = this.allSystemAccessAble;
				}
			},
			allRoleAccessAbleChange: function() {
				// 遍历全部系统.
				var len = this.dataList.length;
				for(var i = 0; i < len; i ++) {
					// 仅仅当系统选择的情况下，处理角色.
					if(this.dataList[i].accessAble === true) {
						var len2 = this.dataList[i].roles.length;
						for(var j = 0; j < len2; j ++) {
							this.dataList[i].roles[j].accessAble = this.allRoleAccessAble;
						}
					}
				}
			},
			oneSystemAccessAbleChange: function(module) {
				if(module.accessAble === false) {
					// 系统不可用的情况下， 需要把角色自动不可用.
					var len2 = module.roles.length;
						for(var j = 0; j < len2; j ++) {
							module.roles[j].accessAble = false;
						}
				}
			},
			// ajax 加载数据.
			loadData:function() {
				var _this = this;
				var loadUrl = _thisService.config.myServerHost + "api/MyAuth/MyUser/GetManagerAbleSystem/" + _this.userID;
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
				var saveUrl = _thisService.config.myServerHost + "api/MyAuth/MyUser/UpdateManagerAbleSystem/" + _this.userID;
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