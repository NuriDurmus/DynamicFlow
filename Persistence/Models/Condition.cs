using System;
using System.Collections.Generic;

namespace Persistence.Models
{
    public partial class Condition
    {
        public int Id { get; set; }
        public string PropertyName { get; set; }
        public string ConditionName { get; set; }
        public string ConditionValue { get; set; }
        public string ConditionPropertyValue { get; set; }
        public int? ConditionSetId { get; set; }

        public virtual ConditionSet ConditionSet { get; set; }
    }
}
