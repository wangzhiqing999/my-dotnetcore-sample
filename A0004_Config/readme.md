# .NET Core 配置文件操作.


本项目在 .Net Core 2.0 版本下运行。


Nuget 获取
Microsoft.Extensions.Configuration.Ini
Microsoft.Extensions.Configuration.Json
Microsoft.Extensions.Configuration.Xml

用于解析  ini , json,  xml  文件。









## A0004_Config_V5
使用 .NET 5.0 版本.


Nuget 获取
Microsoft.Extensions.Configuration.Ini
Microsoft.Extensions.Configuration.Json
Microsoft.Extensions.Configuration.Xml

用于解析  ini , json,  xml  文件。



Nuget 获取
Yt.Extensions.Configuration.Properties
Yt.Extensions.Configuration.Yaml

用于解析  propertie , yaml 文件。


Nuget 获取
Yt.Extensions.Configuration.Consul
Yt.Extensions.Configuration.Etcd

用于解析  Consul , Etcd 服务器上的配置数据。






### Consul 安装与测试

测试服务器 192.168.1.153

安装
sudo docker run --name consul -d -p 8500:8500 consul

查看管理页
http://192.168.1.153:8500/

在 【Key/Value】 的地方，添加用于测试的 配置信息.
添加一个 Key = TestCode
Value =  一段 Json 字符串
【Save】







### Etcd 安装与测试

测试服务器 192.168.1.153

安装
export NODE1=192.168.1.153

sudo docker run --name etcd \
    -p 2379:2379 \
    -p 2380:2380 \
    quay.io/coreos/etcd:latest \
    /usr/local/bin/etcd \
    --data-dir=/etcd-data --name node1 \
    --initial-advertise-peer-urls http://${NODE1}:2380 --listen-peer-urls http://0.0.0.0:2380 \
    --advertise-client-urls http://${NODE1}:2379 --listen-client-urls http://0.0.0.0:2379 \
    --initial-cluster node1=http://${NODE1}:2380


本机 /home/wang/e3w/conf/ 目录下，准备好一个配置文件
wang@wang001:~/e3w/conf$ cat config.default.ini
[app]
port=8080
auth=false

[etcd]
root_key=e3w_test
dir_value=
addr=192.168.1.153:2379
username=
password=
cert_file=
key_file=
ca_file=


运行
sudo docker run  --name e3w \
  -d \
  -p 8080:8080 \
  -v /home/wang/e3w/conf/config.default.ini:/app/conf/config.default.ini \
  soyking/e3w:latest


浏览器访问
http://192.168.1.153:8080/
添加一个 
Key = ABC
Value =  一段 Json 字符串
【UPDATE】



