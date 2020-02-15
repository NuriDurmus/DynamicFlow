using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Entity
{
    public class Conditions
    {
        public int Id { get; set; }
        public string PropertyName { get; set; }
        public string ConditionName { get; set; }
        public string ConditionValue { get; set; }
        public string ConditionPropertyValue { get; set; }
        public int ConditionSetId { get; set; }
        public ConditionSet ConditionSet { get; set; }
    }
}
