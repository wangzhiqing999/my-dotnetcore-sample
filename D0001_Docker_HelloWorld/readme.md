
### 前置条件
VS2017下，创建一个 .Net Core 的 Web 项目。
使用 MVC
不使用身份验证。
发布到项目的 \bin\Release\PublishOutput 目录下。


Windows 环境下，在这个目录，运行
...\D0001_Docker_HelloWorld\bin\Release\PublishOutput>dotnet D0001_Docker_HelloWorld.dll
Hosting environment: Production
Content root path: E:\My-GitHub\my-dotnetcore-sample\D0001_Docker_HelloWorld\D0001_Docker_HelloWorld\bin\Release\PublishOutput
Now listening on: http://localhost:5000
Application started. Press Ctrl+C to shut down.

浏览器能够访问此页面。



也就是开发的操作，全部在 Windows 下面完成。
Linux 服务器，只复制 通过 Dockerfile，制作镜像的操作。


Linux 环境下 （Ubuntu 16）
已安装好了 docker


### 安装 microsoft/aspnetcore
sudo docker pull microsoft/aspnetcore


### 验证安装是否成功.
sudo docker images
REPOSITORY             TAG                 IMAGE ID            CREATED             SIZE
microsoft/aspnetcore   latest              db030c19e94b        4 weeks ago         347 MB


### 准备工作
将 Windows 下发布好的 PublishOutput 目录，复制到 Linux 的目录下。
编辑 Dockerfile ， 也放到 PublishOutput 的目录下.


### 创建镜像 (在 PublishOutput 目录下运行 )
sudo docker build -t myaspnetcore:1.0 .


### 验证是否创建成功
sudo docker images
REPOSITORY             TAG                 IMAGE ID            CREATED             SIZE
myaspnetcore           1.0                 f2812721ac4d        58 seconds ago      350 MB
microsoft/aspnetcore   latest              db030c19e94b        4 weeks ago         347 MB


### 运行容器
sudo docker run --name myaspnetcore -d -p 5000:80 myaspnetcore:1.0
注意：在 Linux 下，运行 dotnet D0001_Docker_HelloWorld.dll 使用的是 80 端口。
这里的 -p 5000:80,  是指把 容器里面的 80 端口，映射到 外部的 5000 端口。

本机测试是否能访问 (条件允许的情况下，可使用浏览器来访问)
curl http://localhost:5000


### 查询进程
sudo docker ps
CONTAINER ID        IMAGE               COMMAND                  CREATED             STATUS              PORTS                    NAMES
50f26a2ac890        myaspnetcore:1.0    "dotnet D0001_Dock..."   6 minutes ago       Up 6 minutes        0.0.0.0:3000->3000/tcp   myaspnetcore


### 进入容器
sudo docker exec -it myaspnetcore /bin/bash
遇到故障的情况下，才做此操作，进行故障处理/排除。
例如，如果前面的 -p 5000:80， 写成了 -p 5000:5000
那么，后面的 curl http://localhost:5000 将报错。
这种情况下， 进入容器，在容器里面，尝试运行 dotnet D0001_Docker_HelloWorld.dll
将返回 80 端口已被占用的错误信息。


### 设置自动启动
sudo docker update --restart=always 50f26a2ac890
(重新启动虚拟机，验证是否自动启动了)




## 清理工作.

### 停止容器
sudo docker stop 50f26a2ac890

### 验证是否成功停止.
sudo docker ps

### 查询容器
sudo docker ps -a
CONTAINER ID        IMAGE               COMMAND                  CREATED             STATUS                     PORTS               NAMES
50f26a2ac890        myaspnetcore:1.0    "dotnet D0001_Dock..."   27 minutes ago      Exited (0) 8 minutes ago                       myaspnetcore

### 删除容器
sudo docker rm 50f26a2ac890

### 删除镜像
sudo docker rmi f2812721ac4d

### 验证删除是否成功.
sudo docker images
REPOSITORY             TAG                 IMAGE ID            CREATED             SIZE
microsoft/aspnetcore   latest              db030c19e94b        4 weeks ago         347 MB


