using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Entity
{
    public class MasterConditionSet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<ConditionSet> ConditionSets { get; set; }
    }
}
