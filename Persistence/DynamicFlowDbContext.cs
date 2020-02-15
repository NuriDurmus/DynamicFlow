using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence
{
    public class DynamicFlowDbContext:DbContext
    {
        public DynamicFlowDbContext(string connString) : base(connString)
        {

        }
    }
}
