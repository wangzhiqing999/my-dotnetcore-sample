﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A0100_PostgreSQL.Notify
{

    /// <summary>
    /// Options to configure the <see cref="PostgresNotificationService"/>.
    /// </summary>
    public class PostgresNotificationServiceOptions
    {

        /// <summary>
        /// Gets or sets the Channel the Service is listening to.
        /// </summary>
        public required string ChannelName { get; set; }

        /// <summary>
        /// Gets or sets the Maximum Capacity of unhandled Notifications. Default is 10,000 notifications. 
        /// </summary>
        public int MaxCapacity { get; set; } = 10_000;

    }
}
