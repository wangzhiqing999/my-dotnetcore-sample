

## 开发环境
Windows 11
Visual Studio 2022, 版本：17.11.4
Docker Desktop, 版本：4.34.3



## 创建项目
VS2022下，创建一个 ASP.Net Core Web 应用。

框架：.NET 8.0
身份验证类型：无
不配置 HTTPS
启用容器支持
容器OS:Linux
容器生成类型：Dockerfile
不使用顶级语句。


项目创建完成后，Visual Studio 后台的输出：

========== 容器必备项检查 ==========
正在验证是否安装了 Docker Desktop...
安装了 Docker Desktop。
========== 正在验证 Docker Desktop 是否正在运行... ==========
正在验证 Docker Desktop 是否正在运行...
Docker Desktop 正在运行。
========== 正在验证 Docker OS ==========
正在验证 Docker Desktop 的操作系统模式是否匹配项目的目标操作系统...
Docker Desktop 的操作系统模式与项目的目标操作系统匹配。
========== 拉取所需的映像 ==========
正在检查缺少的 Docker 映像...
正在拉取 Docker 映像。要取消此下载，请关闭命令提示符窗口。
docker pull mcr.microsoft.com/dotnet/aspnet:8.0
Docker 映像准备就绪。
========== 正在为 D0001_Docker_HelloWorld_Net8 预热容器 ==========
正在启动容器...




前往 Docker Desktop， 查看 Images。

PS C:\Users\wangz> docker images
REPOSITORY                        TAG       IMAGE ID       CREATED         SIZE
d0001dockerhelloworldnet8         dev       7f00166dfb9a   6 minutes ago   217MB
mcr.microsoft.com/dotnet/aspnet   8.0       70975ed8a403   3 days ago      217MB


PS C:\Users\wangz> docker ps
CONTAINER ID   IMAGE                           COMMAND                   CREATED         STATUS         PORTS
          NAMES
344d8808ed65   d0001dockerhelloworldnet8:dev   "dotnet --roll-forwa…"   6 minutes ago   Up 6 minutes   0.0.0.0:32768->8080/tcp   D0001_Docker_HelloWorld_Net8




回到 Visual Studio， 点击 [Container (Dockerfile)] 来运行。




## 发布到 Ubuntu 22.04.5 的 Docker 上去.


wang@pve003:~/D0001_Docker_HelloWorld_Net8$ docker build -t helloworld-net8 -f ./Dockerfile .

DEPRECATED: The legacy builder is deprecated and will be removed in a future release.
            Install the buildx component to build images with BuildKit:
            https://docs.docker.com/go/buildx/
			
19个步骤...


wang@pve003:~/D0001_Docker_HelloWorld_Net8$ docker images
REPOSITORY                        TAG            IMAGE ID       CREATED          SIZE
helloworld-net8                   latest         4c07947e07f4   8 seconds ago    226MB



wang@pve003:~/D0001_Docker_HelloWorld_Net8$ docker run --name helloworld-net8 -d -p 5000:8080 helloworld-net8:latest


尝试浏览器访问 
http://pve003:5000/

