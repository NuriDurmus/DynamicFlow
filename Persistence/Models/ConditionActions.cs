using System;
using System.Collections.Generic;

namespace Persistence.Models
{
    public partial class ConditionActions
    {
        public int Id { get; set; }
        public string ActionName { get; set; }
        public string ActionParameterValues { get; set; }
        public int? MasterConditionSetId { get; set; }
        public string CronExpression { get; set; }
        public int? OrderId { get; set; }
        public bool? StopOnException { get; set; }
        public int? RetryCount { get; set; }

        public virtual MasterConditionSet MasterConditionSet { get; set; }
    }
}
