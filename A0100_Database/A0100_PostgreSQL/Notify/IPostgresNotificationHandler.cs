using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A0100_PostgreSQL.Notify
{

    /// <summary>
    /// 处理 PostgresSQL 的通知接口.
    /// </summary>
    public interface IPostgresNotificationHandler
    {
        ValueTask HandleNotificationAsync(PostgresNotification notification, CancellationToken cancellationToken);
    }
}
