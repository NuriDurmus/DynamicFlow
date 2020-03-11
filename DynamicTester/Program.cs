using DynamicBuilder;
using DynamicFlow.Business;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DynamicTester
{
    class Program
    {
        static void Main(string[] args)
        {
            MasterConditionSet masterConditionSet;
            JObject data;
            GetData(out masterConditionSet, out data);
            //Builder.RunCondition(masterConditionSet, data);
            EmailService service = new EmailService();
            for (int i = 0; i < 1; i++)
            {
                Builder.RunAction(masterConditionSet); 
            }
            Console.ReadLine();
        }

        private static void GetData(out MasterConditionSet masterConditionSet, out JObject data)
        {
            var context = new ConditionHandlerContext();
            //context.Database.EnsureCreated();
            //context.SeedDatabase(context);
            masterConditionSet = context.MasterConditionSet.Include(i => i.ConditionActions).Include(i => i.ConditionSet).ThenInclude(i => i.Conditions).FirstOrDefault();
            data = new JObject();
            data["PersonName"] = "Nuri";
            data["EmailAddress"] = "nuri.durmus@kizilay.org.tr";
            data["Age"] = "5";
        }



    }
}
