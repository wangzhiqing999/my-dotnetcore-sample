using System;
using System.Collections.Generic;

namespace A0008_EF_ExistingDb.Model
{
    public partial class McaCustomAction
    {
        public long Id { get; set; }
        public long CustomId { get; set; }
        public string ModuleCode { get; set; }
        public DateTime EnterTime { get; set; }
        public DateTime LastAccessTime { get; set; }
        public int AccessCount { get; set; }
        public string ExpData { get; set; }

        public virtual McaServiceModule ModuleCodeNavigation { get; set; }
    }
}
