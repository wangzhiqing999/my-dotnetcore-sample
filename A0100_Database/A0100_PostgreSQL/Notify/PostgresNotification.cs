using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A0100_PostgreSQL.Notify
{
    public record PostgresNotification
    {
        /// <summary>
        /// Gets or sets the PID.
        /// </summary>
        public required int PID { get; set; }

        /// <summary>
        /// Gets or sets the Channel.
        /// </summary>
        public required string Channel { get; set; }

        /// <summary>
        /// Gets or sets the Payload.
        /// </summary>
        public required string Payload { get; set; }
    }
}
