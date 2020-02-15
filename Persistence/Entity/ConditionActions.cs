using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Entity
{
    public class ConditionActions
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ActionName { get; set; }
        public string ActionParameterValues { get; set; }
        public int MasterConditionSetId { get; set; }
        public string CronExpression { get; set; }
        public int OrderId { get; set; }
        public bool StopOnException { get; set; }
        public int RetryCount { get; set; }
        public MasterConditionSet MasterConditionSet { get; set; }
    }
}
