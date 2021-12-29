


## 初始化安装.


### 下载安装 postgrest


前往

https://github.com/PostgREST/postgrest/releases/tag/v9.0.0

下载相应的软件包.




测试机 Ubuntu （192.168.1.140）下载 postgrest-v9.0.0-linux-static-x64.tar.xz

运行
wget https://github.com/PostgREST/postgrest/releases/download/v9.0.0/postgrest-v9.0.0-linux-static-x64.tar.xz

然后
xz -d postgrest-v9.0.0-linux-static-x64.tar.xz

然后
tar -xvf postgrest-v9.0.0-linux-static-x64.tar


完成后，当前目录下，  postgrest 就是可执行的程序。
运行提示如下:

./postgrest
Usage: postgrest [-e|--example] [--dump-config | --dump-schema] FILENAME
  PostgREST 9.0.0 / create a REST API to an existing Postgres database

Available options:
  -h,--help                Show this help text
  -e,--example             Show an example configuration file
  --dump-config            Dump loaded configuration and exit
  --dump-schema            Dump loaded schema as JSON and exit (for debugging,
                           output structure is unstable)
  FILENAME                 Path to configuration file (optional with PGRST_
                           environment variables)

To run PostgREST, please pass the FILENAME argument or set PGRST_ environment
variables.






### Postgres 客户端，执行下列 SQL.


create role my_etf_anon nologin;

grant usage on schema my_etf to my_etf_anon;

grant select on my_etf.etf_master to my_etf_anon;

grant select on my_etf.etf_day_line to my_etf_anon;
-- grant select on my_etf.etf_day_tr to my_etf_anon;
-- grant select on my_etf.etf_day_ema to my_etf_anon;
-- grant select on my_etf.etf_day_macd to my_etf_anon;

grant select on my_etf.etf_week_line to my_etf_anon;
-- grant select on my_etf.etf_week_ema to my_etf_anon;
-- grant select on my_etf.etf_week_macd to my_etf_anon;




create role my_etf_user noinherit login password 'my_etf_password';
grant my_etf_anon to my_etf_user;




### postgrest 配置文件.

然后，在 postgrest 的目录下， 创建一个配置文件  myetf.conf
内容如下
db-uri = "postgres://my_etf_user:my_etf_password@192.168.1.153:5432/postgres"
db-schema = "my_etf"
db-anon-role = "my_etf_anon"





## 启动 postgrest


运行
wang@wang003:~/postgrest$ ./postgrest myetf.conf
29/Dec/2021:11:05:15 +0800: Attempting to connect to the database...
29/Dec/2021:11:05:15 +0800: Connection successful
29/Dec/2021:11:05:15 +0800: Listening on port 3000
29/Dec/2021:11:05:15 +0800: Config re-loaded
29/Dec/2021:11:05:15 +0800: Listening for notifications on the pgrst channel
29/Dec/2021:11:05:15 +0800: Schema cache loaded




## 测试查询


### 查询 ETF 主表.

http://192.168.1.140:3000/etf_master



### 查询 SH510050 的日K线数据. (按 交易日逆序，获取前30行)

http://192.168.1.140:3000/etf_day_line?etf_code=eq.SH510050&order=trading_date.desc&limit=30



### 查询 SH510050 的周K线数据. (按 交易日逆序，获取前30行)

http://192.168.1.140:3000/etf_week_line?etf_code=eq.SH510050&order=trading_date.desc&limit=30



### 查询 SH510050 的 周 MACD 数据. (这里是调用名称为 get_week_macd 的函数.)

http://192.168.1.140:3000/rpc/get_week_macd?p_etf_code=SH510050

