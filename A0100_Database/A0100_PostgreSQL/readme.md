

## Notify

此目录下的代码，为测试 PostgreSQL 的 LISTEN / NOTIFY 处理的例子代码.
 
C# 这里，是 LISTEN 一个 test_event， 当接收到 NOTIFY 的时候， 简单输出。
 
测试的方式：
1. 运行 C# 项目. 

2. 数据库客户端那里，分别运行 
```
NOTIFY test_event
```
与
```
NOTIFY test_event 'test 123'
```

3. 观察 C# 程序控制台上的输出
```
PostgresNotification (PID = 74, Channel = test_event, Payload =
PostgresNotification (PID = 74, Channel = test_event, Payload = test 123
```


