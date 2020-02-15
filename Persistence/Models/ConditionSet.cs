using System;
using System.Collections.Generic;

namespace Persistence.Models
{
    public partial class ConditionSet
    {
        public ConditionSet()
        {
            Conditions = new HashSet<Condition>();
        }

        public int Id { get; set; }
        public int? MasterConditionSetId { get; set; }

        public virtual MasterConditionSet MasterConditionSet { get; set; }
        public virtual ICollection<Condition> Conditions { get; set; }
    }
}
