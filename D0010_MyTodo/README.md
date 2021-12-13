# 一个支持 Docker 的 Todo 应用的例子代码




### 创建项目.

dotnet core web api 项目
版本 .Net 6.0



### 添加 NuGet 引用.

Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.Design
Microsoft.EntityFrameworkCore.Tools
Microsoft.EntityFrameworkCore.Sqlite


### 编写 EF Core 代码.

MyTodoContext
Todo


### 生成数据库.

Add-Migration MyInitMigration
Script-Migration
Update-Database


### 编写 服务代码.

ITodoService
DefaultTodoService


### 编写 Api 控制器.

TodoController


### 填写依赖注入相关代码.

Program.cs




### 测试运行.
在 Visual Studio 2022 中运行
浏览器中 观察 执行结果。

创建项目时生成的测试 api
https://localhost:7284/WeatherForecast

自己写代码的 api
https://localhost:7284/api/Todo/GetTodoList?showIsDone=false






### 测试发布
发布到文件夹： bin\Release\net6.0\publish

cmd 执行.

C:\My-Github\my-dotnetcore-sample\D0010_MyTodo\D0010_MyTodo\bin\Release\net6.0\publish>D0010_MyTodo.exe
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5000
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:5001
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Production
info: Microsoft.Hosting.Lifetime[0]
      Content root path: C:\My-Github\my-dotnetcore-sample\D0010_MyTodo\D0010_MyTodo\bin\Release\net6.0\publish\
	  
	  
	  
浏览器测试访问
http://localhost:5000/WeatherForecast
http://localhost:5000/api/Todo/GetTodoList?showIsDone=false







### 测试发布到 IIS 当中
先下载 ASP.NET Core 6.0 Runtime (v6.0.0) - Windows Hosting Bundle
dotnet-hosting-6.0.0-win.exe
安装

IIS 网站中，添加网站，根目录为 C:\My-Github\my-dotnetcore-sample\D0010_MyTodo\D0010_MyTodo\bin\Release\net6.0\publish
网站名称为 D0010_MyTodo
端口为 8083

IIS 应用程序池中，修改 D0010_MyTodo 的 【基本设置】，将  .NET CLR版本，修改为 【无托管代码】

浏览器测试访问
http://localhost:8083/WeatherForecast
http://localhost:8083/api/Todo/GetTodoList?showIsDone=false







### 测试到 Ubuntu 下执行.

参考安装文档：https://docs.microsoft.com/zh-cn/dotnet/core/install/linux-ubuntu

先执行下面3个命令.
wget https://packages.microsoft.com/config/ubuntu/21.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
rm packages-microsoft-prod.deb

执行安装命令.
sudo apt-get update; \
  sudo apt-get install -y apt-transport-https && \
  sudo apt-get update && \
  sudo apt-get install -y dotnet-sdk-6.0

将本机的 C:\My-Github\my-dotnetcore-sample\D0010_MyTodo\D0010_MyTodo\bin\Release\net6.0\publish
复制到 Ubuntu 下的  /home/wang 目录下


运行
export ASPNETCORE_URLS="http://192.168.1.140:5001"
dotnet D0010_MyTodo.dll

浏览器测试访问
http://192.168.1.140:5001/WeatherForecast
http://192.168.1.140:5001/api/Todo/GetTodoList?showIsDone=false


注意：默认的情况下，是 http://localhost:5000
这种情况下，Windows 开发机器上，无法访问这个地址。







### 测试到 Docker 下执行.

将代码复制到 Ubuntu 下的  /home/wang 目录下

wang@wang003:~$ cd D0010_MyTodo/
wang@wang003:~/D0010_MyTodo$ ls
D0010_MyTodo


生成镜像
wang@wang003:~/D0010_MyTodo$ sudo docker build -t my-todo -f ./D0010_MyTodo/Dockerfile .


wang@wang003:~/D0010_MyTodo$ sudo docker image ls
REPOSITORY                        TAG       IMAGE ID       CREATED              SIZE
my-todo                           latest    e888a1e4be96   About a minute ago   235MB
......
mcr.microsoft.com/dotnet/sdk      6.0       c8231459539b   10 days ago          715MB
mcr.microsoft.com/dotnet/aspnet   6.0       e38d4d116f1a   10 days ago          207MB


创建容器并运行.
wang@wang003:~/D0010_MyTodo$ sudo docker run --name my-todo -d -p 5000:80 my-todo

6b6098ab84b36e799f90e91ced4d15156296eb0d693ab20e3d9d534fa89b7d70


浏览器测试访问
http://192.168.1.140:5000/WeatherForecast
http://192.168.1.140:5000/api/Todo/GetTodoList?showIsDone=false


访问前一个没有问题， 访问后一个翻车.
sudo docker logs 6b60
查询日志.
显示是 数据库查询的时候发生异常.


sudo docker exec -it  6b60 /bin/bash
进入容器

wang@wang003:~/D0010_MyTodo$ sudo docker exec -it  6b60 /bin/bash
root@6b6098ab84b3:/app# ls
D0010_MyTodo                                    SQLitePCLRaw.batteries_v2.dll
D0010_MyTodo.deps.json                          SQLitePCLRaw.core.dll
D0010_MyTodo.dll                                SQLitePCLRaw.provider.e_sqlite3.dll
D0010_MyTodo.pdb                                Swashbuckle.AspNetCore.Swagger.dll
D0010_MyTodo.runtimeconfig.json                 Swashbuckle.AspNetCore.SwaggerGen.dll
Microsoft.Data.Sqlite.dll                       Swashbuckle.AspNetCore.SwaggerUI.dll
Microsoft.EntityFrameworkCore.Abstractions.dll  appsettings.Development.json
Microsoft.EntityFrameworkCore.Relational.dll    appsettings.json
Microsoft.EntityFrameworkCore.Sqlite.dll        data
Microsoft.EntityFrameworkCore.dll               runtimes
Microsoft.Extensions.DependencyModel.dll        web.config
Microsoft.OpenApi.dll

观察到，容器中，并没有 data 目录，也就没有数据库文件.

wang@wang003:~/D0010_MyTodo$ sudo docker stop  6b60
6b60
停止容器.
wang@wang003:~/D0010_MyTodo$ sudo docker rm  6b60
6b60
删除容器.


加上 -v 参数，来创建容器.

wang@wang003:~/D0010_MyTodo/D0010_MyTodo/data$ sudo docker run --name my-todo -d -p 5000:80 -v /home/wang/D0010_MyTodo/D0010_MyTodo/data:/app/data my-todo
36686ae2d136ab5f4cb71b56fb75e8bdb4bb1e79c00b144a59fab4e22ad40b82

浏览器测试访问
http://192.168.1.140:5000/WeatherForecast
http://192.168.1.140:5000/api/Todo/GetTodoList?showIsDone=false

两个都能正常访问.



