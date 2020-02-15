using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Entity
{
    public class ConditionSet
    {
        public int Id { get; set; }
        public int MasterConditionSetId { get; set; }
        public MasterConditionSet MasterConditionSet { get; set; }

        public List<Conditions> Conditions { get; set; }

    }
}
