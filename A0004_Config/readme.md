# .NET Core �����ļ�����.


����Ŀ�� .Net Core 2.0 �汾�����С�


Nuget ��ȡ
Microsoft.Extensions.Configuration.Ini
Microsoft.Extensions.Configuration.Json
Microsoft.Extensions.Configuration.Xml

���ڽ���  ini , json,  xml  �ļ���









## A0004_Config_V5
ʹ�� .NET 5.0 �汾.


Nuget ��ȡ
Microsoft.Extensions.Configuration.Ini
Microsoft.Extensions.Configuration.Json
Microsoft.Extensions.Configuration.Xml

���ڽ���  ini , json,  xml  �ļ���



Nuget ��ȡ
Yt.Extensions.Configuration.Properties
Yt.Extensions.Configuration.Yaml

���ڽ���  propertie , yaml �ļ���


Nuget ��ȡ
Yt.Extensions.Configuration.Consul
Yt.Extensions.Configuration.Etcd

���ڽ���  Consul , Etcd �������ϵ��������ݡ�






### Consul ��װ�����

���Է����� 192.168.1.153

��װ
sudo docker run --name consul -d -p 8500:8500 consul

�鿴����ҳ
http://192.168.1.153:8500/

�� ��Key/Value�� �ĵط���������ڲ��Ե� ������Ϣ.
���һ�� Key = TestCode
Value =  һ�� Json �ַ���
��Save��







### Etcd ��װ�����

���Է����� 192.168.1.153

��װ
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


���� /home/wang/e3w/conf/ Ŀ¼�£�׼����һ�������ļ�
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


����
sudo docker run  --name e3w \
  -d \
  -p 8080:8080 \
  -v /home/wang/e3w/conf/config.default.ini:/app/conf/config.default.ini \
  soyking/e3w:latest


���������
http://192.168.1.153:8080/
���һ�� 
Key = ABC
Value =  һ�� Json �ַ���
��UPDATE��



