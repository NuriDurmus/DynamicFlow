using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Persistence.Models;
using System;
using System.Collections.Generic;
using System.Text;
using DynamicBuilder;
using System.Linq;

namespace DynamicTester
{
    public class BuilderTest
    {
        public void Run()
        {
            MasterConditionSet masterConditionSet;
            JObject data;
            GetData(out masterConditionSet, out data);
            Builder.RunCondition(masterConditionSet, data);
        }

        public void GetData(out MasterConditionSet masterConditionSet, out JObject data)
        {
            var context = new ConditionHandlerContext();
            //context.Database.EnsureCreated();
            //context.SeedDatabase(context);
            masterConditionSet = context.MasterConditionSet.Include(i => i.ConditionActions).Include(i => i.ConditionSet).ThenInclude(i => i.Conditions).FirstOrDefault();
            //işlem yapılacak veri
            data = new JObject();
            data["PersonName"] = "Nuri";
            data["EmailAddress"] = "nuri.durmus@kizilay.org.tr";
            data["Age"] = "19";
        }
    }
}
