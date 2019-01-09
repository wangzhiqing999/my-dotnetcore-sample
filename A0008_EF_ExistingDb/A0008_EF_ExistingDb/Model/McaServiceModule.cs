using System;
using System.Collections.Generic;

namespace A0008_EF_ExistingDb.Model
{
    public partial class McaServiceModule
    {
        public McaServiceModule()
        {
            McaCustomAction = new HashSet<McaCustomAction>();
        }

        public string ModuleCode { get; set; }
        public string ModuleName { get; set; }

        public virtual ICollection<McaCustomAction> McaCustomAction { get; set; }
    }
}
