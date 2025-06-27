using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A0100_PostgreSQL.Notify
{

    /// <summary>
    /// 用于测试的 实现 【处理 PostgresSQL 的通知】 的接口的处理。
    /// <br/>
    /// 这里是接收到的 PostgresSQL 的通知后，简单的打印一下。
    /// </summary>
    public class TestPostgresNotificationHandler : IPostgresNotificationHandler
    {
        public ValueTask HandleNotificationAsync(PostgresNotification notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"PostgresNotification (PID = {notification.PID}, Channel = {notification.Channel}, Payload = {notification.Payload}");

            return ValueTask.CompletedTask;
        }

    }
}
