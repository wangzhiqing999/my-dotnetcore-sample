# .Net Core 的 Web Api 服务端 

涉及到的相关问题：

.Net Core Entity Framework 
使用的是 Microsoft.EntityFrameworkCore.SqlServer


Web API 允许跨域操作的相关代码.


Web API jwt token 认证的相关处理.


测试运行
先编辑 appsettings.json 修改数据库连接字符串.

然后更新数据库表结构.
```
Update-Database
```


然后 运行 MyWork.Web 项目.



# framework7 的 Web Api 客户端

代码是 framework7-template-vue-simple 的目录结构 + framework7-template-split-view 的页面.

测试运行

确认机器上已装好 Node.js

然后在 framework7-client 目录下

安装相关依赖的包
```
npm install
```

运行
```
npm run serve
```

