
### 前置条件

开发计算机， Win 10,  Visual Studio 2019下，创建 .Net Core 的 控制台 项目。



参考的文档.
https://docs.microsoft.com/zh-cn/dotnet/core/docker/build-container?tabs=windows





2021-12-07
开发计算机， Win 11,  Visual Studio 2022下，创建 .Net 6.0 的 控制台 项目。
后续操作，与 .Net 5.0 的，基本一致。
目的： 观察 .Net 5.0 与 .Net 6.0 上的差异。




2024-10-12
开发计算机， Win 11,  Visual Studio 2022下，创建 .Net 8.0 的 控制台 项目。
后续操作，与 .Net 6.0 的，基本一致。
目的： 观察 .Net 6.0 与 .Net 8.0 上的差异。








### C# 方面的操作

创建 .net Core 控制台项目， 版本选择 net5.0
修改默认的 Program.cs
编译通过。



### Docker 方面的操作

选择项目，鼠标右键，【添加】-->【Docker 支持...】
选 Linux
完成后，项目中增加一个 Dockerfile










### 准备工作.

源代码全部 复制到 Ubuntu 下面。

实际工作中，可能是通过 Git 拉取代码的操作.




### 创建镜像

在 ~/D0003_Docker_Console$ 目录下，运行
sudo docker build -t counter-image -f ./D0003_Docker_Console/Dockerfile .


.Net 6.0 的代码：
sudo docker build -t counter-image-v6 -f ./D0003_Docker_Console_V6/Dockerfile .



[
注意：
不要去 ~/D0003_Docker_Console/D0003_Docker_Console$  目录下，简单运行
sudo docker build -t counter-image .
结果将报错，提示 COPY 的时候，文件不存在！
]



docker build 执行完毕后，尝试运行
sudo docker image ls
观察 counter-image 是否存在.


~/D0003_Docker_Console$ sudo docker image ls
REPOSITORY                         TAG               IMAGE ID       CREATED          SIZE
counter-image                      latest            a425663cc9d3   48 seconds ago   186MB
……



.Net 6.0 代码执行后的查询结果：
~/D0003_Docker_Console$ sudo docker image ls
REPOSITORY                         TAG               IMAGE ID       CREATED              SIZE
counter-image-v6                   latest            1c56c1a7f304   57 seconds ago       187MB
mcr.microsoft.com/dotnet/sdk       6.0               c8231459539b   4 days ago      715MB
mcr.microsoft.com/dotnet/runtime   6.0               94a4f0160227   4 days ago      187MB
mcr.microsoft.com/dotnet/sdk       5.0               3d9edf094595   7 weeks ago     631MB
mcr.microsoft.com/dotnet/runtime   5.0               cb8ec4f45a39   7 weeks ago     186MB




通过
sudo docker image prune
命令进行清除名称为 <none> 的镜像.








### 创建容器
sudo docker create --name core-counter counter-image

.Net 6.0 的代码： (前提是之前创建的容器，已经被删除， 因为这里没有变更 容器的名字，还是使用  core-counter )
sudo docker create --name core-counter counter-image-v6




### 启动容器
sudo docker start core-counter


### 连接到容器，观察输出
sudo docker attach --sig-proxy=false core-counter
[Ctrl]+[c] 强制退出.


### 停止容器.
sudo docker stop core-counter


### 删除容器.
sudo docker rm core-counter





### 单次执行
sudo docker run -it --rm counter-image
[Ctrl]+[c] 强制退出.


### 单次执行-传递命令行参数
sudo docker run -it --rm counter-image 3


说明：
docker run -it，Ctrl+C 命令会停止在容器中运行的进程，进而停止容器。 由于提供了 --rm 参数，因此在进程停止时自动删除容器。




### 进入到容器中查看文件。
sudo docker run -it --rm --entrypoint "bash" counter-image
输入 exit 退出.









## 主机和容器之间的数据共享


主机上，在 /home/wangzhiqing/test 目录下。创建一个 title.txt， 内容为 test volume

查看主机的文件
~/test$ more title.txt
test volume



### 测试 Docker 中 volume 的使用 (配置文件).
sudo docker run -it --rm  -v /home/wangzhiqing/test:/app/conf counter-image 10

上面的命令，是将主机上的 /home/wangzhiqing/test ， 与容器中的 /app/conf 建立关联，这两个地址如果不存在都会创建，一旦容器运行，这两个目录会完全同步。


运行
~/D0003_Docker_Console$ sudo docker run -it --rm  -v /home/wangzhiqing/test:/app/conf counter-image 3
test volume: 1
test volume: 2
test volume: 3


运行  （运行前，计算机上不存在 /home/wangzhiqing/test3 目录）
~/D0003_Docker_Console$ sudo docker run -it --rm  -v /home/wangzhiqing/test3:/app/conf counter-image 3
Error: 1
Error: 2
Error: 3







### 测试 Docker 中 volume 的使用 (输出文件).  （运行前，计算机上不存在 /home/wangzhiqing/test2 目录）
~/D0003_Docker_Console$ sudo docker run -it --rm -v /home/wangzhiqing/test2:/app/data counter-image 10
Counter: 1
Counter: 2
Counter: 3
Counter: 4
Counter: 5
Counter: 6
Counter: 7
Counter: 8
Counter: 9
Counter: 10


~/D0003_Docker_Console$ cd /home/wangzhiqing/test2
~/test2$ ls
output.txt
~/test2$ more output.txt
Counter: 10




### 注意路径的问题.

C#代码中，如果使用绝对路径的话，例如  "/conf"  "/data"  这种写法。
那么，在 windows 下面，测试运行的时候， 存在翻车的风险。


C# 代码中，使用 相对路径的写法，例如 "./conf"  "./data"  这种写法。
这样，在 windows 下面，测试运行的时候， 可以正常运行。

就是在 Docker 中运行时，指定 volume 的时候，需要注意， 容器里面的目录， 是  "/app/conf" 与 "/app/data" 这样的路径
这个 app目录是从哪里来的？   是写在 Dockerfile 里面的。最后的 WORKDIR /app











## 测试 Ubuntu 中， 使用 crontab 定时执行 Docker 中的容器.

修改 Program.cs ， 输出时， 额外输出当前时间信息.

sudo docker build -t counter-image-v6 -f ./D0003_Docker_Console_V6/Dockerfile .

sudo docker run -it --name core-counter -v /home/wang/D0003_Docker_Console:/app/data counter-image-v6 1


sudo -s
root@wang001:/home/wang/D0003_Docker_Console#


单次运行：
root@wang001:/home/wang/D0003_Docker_Console# docker start core-counter
core-counter
root@wang001:/home/wang/D0003_Docker_Console# more output.txt
CounterV6: 1 @ 2021-12-23 13:36:25



运行 crontab 配置作业.
root@wang001:/home/wang/D0003_Docker_Console# crontab -e
no crontab for root - using an empty one

Select an editor.  To change later, run 'select-editor'.
  1. /bin/nano        <---- easiest
  2. /usr/bin/vim.basic
  3. /usr/bin/vim.tiny
  4. /bin/ed

Choose 1-4 [1]:


在 nano 编辑器中，输入：

* * * * * docker start core-counter

Ctrl+X 退出
提示是否保存时，选择 Y
然后是输入保存的文件名是，简单的直接按回车。

观察输出文件，查看 容器是否执行了。

root@wang001:/home/wang/D0003_Docker_Console# more output.txt
CounterV6: 1 @ 2021-12-23 13:46:02
root@wang001:/home/wang/D0003_Docker_Console# more output.txt
CounterV6: 1 @ 2021-12-23 13:47:02
root@wang001:/home/wang/D0003_Docker_Console# more output.txt
CounterV6: 1 @ 2021-12-23 13:48:03



再次运行 crontab 配置作业.
root@wang001:/home/wang/D0003_Docker_Console# crontab -e

在 nano 编辑器中，注释掉：

# * * * * * docker start core-counter

Ctrl+X 退出
提示是否保存时，选择 Y
然后是输入保存的文件名是，简单的直接按回车。


再次观察，容器已经不是每分钟执行的了。

root@wang001:/home/wang/D0003_Docker_Console# date
Thu 23 Dec 2021 09:51:43 PM CST
root@wang001:/home/wang/D0003_Docker_Console# more output.txt
CounterV6: 1 @ 2021-12-23 13:48:03
