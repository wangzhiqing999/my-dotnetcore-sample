# 创建一个 React 项目。

在 Visual Studio 2017 中， 创建项目
在 .NET Core 选项卡下， 选择 ASP.NET Core Web 应用程序。
在 .NET Core / ASP.NET Core 2.0 的选项下， 选择 React.js 的项目模板。

项目创建完毕后，会卡顿一段时间，下载各种依赖项目。



项目最终由 Webpack 进行打包处理。
打包结果存储在  wwwroot 的 dist 目录下。



## 依赖项目

### react
作用：React 是 React 库的入口

### react-dom
作用：react-dom 包提供了 DOM 特定的方法，可以在你的应用程序的顶层使用

### react-hot-loader
作用：就是使用 react 编写代码时，能让修改的部分自动刷新。但这和自动刷新网页是不同的，因为 hot-loader 并不会刷新网页，而仅仅是替换你修改的部分

### react-router-dom
作用：路由的处理。（普通页面跳转，是要浏览器刷新页面，进行处理。 react的页面跳转，浏览器不发生刷新的操作，全部在客户端的 js 那里处理。 ）




## 代码的说明.

### /Views/Home/Index.cshtml
这个是首页的代码，基本是最终运行时，客户端的 html 页面内容。
主要的注意点是 <div id="react-app">Loading...</div>  中间的  react-app， 没事不要去修改。


### /ClientApp/boot.tsx
项目的启动，定义在 renderApp() 方法中。

### /ClientApp/routes.tsx
路由的定义， 也就是 每一个 URL 地址， 具体对应于  /ClientApp/components/ 下面的那个模块。

### /ClientApp/components/ 目录下的 .tsx 文件.
基本是一个页面区域，一个 .tsx 文件。
Layout.tsx 是布局， 也就是 左菜单， 右内容。
NavMenu.tsx 是菜单





## 测试追加业务功能代码.

1.依赖项， 管理 NuGet 程序包， 添加
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools

2.编写 EF CodeFirst 相关代码.
Model 目录下，新增相关的数据类.
DataAccess 目录下，新增 EF 的 Context 类.

3.程序包管理器控制台上运行
Add-Migration MyFirstMigration
Update-Database
插入测试数据.

4.添加 操作使用 Entity Framework 的 API 控制器.
选择 Report 类. 生成 ReportsController.cs

5.修改 /Startup.cs
添加
// 数据库连接字符串，定义在 appsettings.json 文件中.
string connString = Configuration.GetConnectionString("MyReportConnection");
// 数据库配置
services.AddDbContext<MyReportContext>(options => options.UseSqlServer(connString));

6.修改 /appsettings.json
添加
  "ConnectionStrings": {
    "MyReportConnection": "Data Source=localhost\\SQLEXPRESS;Initial Catalog=TestReport; Integrated Security=True;"
  },

7./ClientApp/components 目录下，创建 TypeScript JSX 文件.
Report.tsx

8.修改 /ClientApp/routes.tsx
添加
import { ReportList } from './components/Report';
<Route path='/report' component={ReportList} />

9.修改 /ClientApp/components/NavMenu.tsx
添加
<li>
	<NavLink to={'/report'} activeClassName='active'>
		<span className='glyphicon glyphicon-th-list'></span> Test Report
	</NavLink>
</li>

10. 尝试运行.