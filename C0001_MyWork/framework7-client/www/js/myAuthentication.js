var myAuthentication = {

	// 组织机构代码.
	orgCode : "",

	// 用户名.
	username : "",

	// 密码.
	password : "",

	// 登录后的 Token.
	accessToken : "",

	// 完成登录.
	doLogin : function(callback) {
		var _this = this;
		var loginUrl = _defaultConfig.myServerHost + "api/Authentication/Token";
		// Show Preloader
		app.preloader.show();
		Framework7.request.postJSON(
			loginUrl,
			{
				organization : _this.orgCode,
				user : _this.username,
				password : _this.password
			},
			function (data) {
				// Hide Preloader
				app.preloader.hide();
				// console.log(data);
				// 获取登录的 Token.
				_this.accessToken = data.token;

				// After the following setup all XHR requests will have additional 'Autorization' header
				Framework7.request.setup({
					headers: {
						'Authorization': 'Bearer ' + _this.accessToken
					}
				});

				// 调用回调函数，完成【登录成功的相关处理】
				callback();
			},
			function (xhr, status) {
				// Hide Preloader
				app.preloader.hide();
				app.dialog.alert("用户名或者密码不正确！");
			}
		);
	}
};