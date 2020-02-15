using System;
using System.Collections.Generic;

namespace Persistence.Models
{
    public partial class MasterConditionSet
    {
        public MasterConditionSet()
        {
            ConditionActions = new HashSet<ConditionActions>();
            ConditionSet = new HashSet<ConditionSet>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<ConditionActions> ConditionActions { get; set; }
        public virtual ICollection<ConditionSet> ConditionSet { get; set; }
    }
}
