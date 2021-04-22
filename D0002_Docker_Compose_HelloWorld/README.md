
### 前置条件

开发计算机， Win 10,  Visual Studio 2019下，创建 .Net Core 的 Web 项目。
创建时，选择 Enable Docker


参考的文档.
https://docs.microsoft.com/zh-cn/visualstudio/containers/tutorial-multicontainer?view=vs-2019



测试服务器， ubuntu 20.04
IP地址 192.168.1.39
已安装好 Docker
已安装好 Docker Compose



### 准备工作.

源代码全部 复制到 Ubuntu 下面。

实际工作中，可能是通过 Git 拉取代码的操作.



### 创建镜像

在 ~/D0002_Docker_Compose_HelloWorld$ 目录下，运行
sudo docker build -t testwebapi -f ./TestWebApi/Dockerfile .
sudo docker build -t testwebsite -f ./TestWebSite/Dockerfile .


[
注意：
不要去 ~/D0002_Docker_Compose_HelloWorld/TestWebApi$  目录下，简单运行
sudo docker build -t testwebapi .
结果将报错，提示 COPY 的时候，文件不存在！
]



docker build 执行完毕后，尝试运行
sudo docker image ls
观察 testwebapi 与 testweb 是否在列表中.

~/D0002_Docker_Compose_HelloWorld$ sudo docker image ls
REPOSITORY                        TAG                 IMAGE ID            CREATED             SIZE
testwebsite                       latest              1d0f7cc76d87        3 seconds ago       210MB
<none>                            <none>              6f3de0a9b5d4        7 seconds ago       645MB
testwebapi                        latest              410f10b25669        2 minutes ago       209MB
<none>                            <none>              fa3a635e5b91        2 minutes ago       665MB
mcr.microsoft.com/dotnet/sdk      5.0-buster-slim     bd73c72c93a1        8 days ago          626MB
mcr.microsoft.com/dotnet/aspnet   5.0-buster-slim     36007796eeee        8 days ago          205MB
openjdk                           8-jdk-alpine        a3562aa0b991        23 months ago       105MB

注意： 存在有 <none> 的镜像，  观察之前的 sudo docker build -t testweb -f ./TestWeb/Dockerfile . 的日志.

通过

sudo docker image prune

命令进行清除处理.


清除后
sudo docker image ls
REPOSITORY                        TAG                 IMAGE ID            CREATED              SIZE
testwebsite                       latest              1d0f7cc76d87        30 seconds ago      210MB
testwebapi                        latest              410f10b25669        2 minutes ago       209MB
mcr.microsoft.com/dotnet/sdk      5.0-buster-slim     bd73c72c93a1        8 days ago          626MB
mcr.microsoft.com/dotnet/aspnet   5.0-buster-slim     36007796eeee        8 days ago          205MB
openjdk                           8-jdk-alpine        a3562aa0b991        23 months ago       105MB




### 测试运行 testwebapi
sudo docker run --name testwebapi -d -p 5001:8081 testwebapi:latest


### 查询日志.
docker logs testwebapi


测试访问
http://192.168.1.39:5001/api/HelloWorld/123



### 查询进程

sudo docker ps
CONTAINER ID        IMAGE               COMMAND                  CREATED             STATUS              PORTS          			NAMES
1f72ab813838        testwebapi:latest   "dotnet TestWebApi.d…"   44 seconds ago      Up 41 seconds       0.0.0.0:5000->8081/tcp   	testwebapi


### 停止容器
sudo docker stop 1f72ab813838


### 删除容器
sudo docker rm 1f72ab813838





## 测试运行 testwebsite

sudo docker run --name testwebsite -d -p 5002:8082 testwebsite:latest


### 查询日志.
docker logs testwebsite


测试访问
http://192.168.1.39:5002/


### 查询进程

sudo docker ps
CONTAINER ID        IMAGE               COMMAND                  CREATED              STATUS              PORTS           			NAMES
e4eea4010bba        testwebsite:latest   "dotnet TestWebSite.…"   20 seconds ago      Up 18 seconds       0.0.0.0:5002->8082/tcp   testwebsite


### 停止容器
sudo docker stop e4eea4010bba


### 删除容器
sudo docker rm e4eea4010bba








### 测试多个项目的 Docker Compose



sudo docker-compose build

完成后，通过
sudo docker image ls
查询

通过
sudo docker image prune
清除 <none>


注意：
上面这个操作，是假定，没有经过前面的，单独 build 的操作。
如果已经单独执行过
sudo docker build -t testwebapi -f ./TestWebApi/Dockerfile .
sudo docker build -t testwebsite -f ./TestWebSite/Dockerfile .

那么， 上面的
sudo docker-compose build
就不必执行了。




### 测试运行
sudo docker-compose up -d


如果提示
ERROR: Couldn't connect to Docker daemon at http+docker://localhost - is it running?
If it's at a non-standard location, specify the URL with the DOCKER_HOST environment variable.


解决办法：将当前用户加入docker用户组：
$ sudo gpasswd -a ${USER} docker

重新登录.



再次运行
sudo docker-compose up -d



### 查询
docker-compose ps
                    Name                              Command           State             Ports
--------------------------------------------------------------------------------------------------------
d0002_docker_compose_helloworld_testwebapi_1    dotnet TestWebApi.dll    Up      0.0.0.0:9091->8081/tcp
d0002_docker_compose_helloworld_testwebsite_1   dotnet TestWebSite.dll   Up      0.0.0.0:9092->8082/tcp



### 查看日志.
sudo docker-compose logs

当查询结果，显示服务未能启动的情况下，查看日志，看看问题出在哪里。






### 测试访问

http://192.168.1.39:9091/api/HelloWorld/1234567
用于验证 testwebapi 正常启动.

http://192.168.1.39:9092/
用于验证 testwebsite 正常启动.

http://192.168.1.39:9092/Privacy
用于验证 testwebsite 在内部 访问 testwebapi



### 停止.
sudo docker-compose stop testwebsite
sudo docker-compose stop testwebapi



### 删除.
sudo docker-compose rm testwebsite
sudo docker-compose rm testwebapi

