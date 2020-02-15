using DynamicTester.Business;
using DynamicTester.Helpers;
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
        // The delegate must have the same signature as the method
        // it will call asynchronously.
        public delegate string AsyncMethodCaller(int callDuration, out int threadId);
        static void Main(string[] args)
        {
            var context = new ConditionHandlerContext();
            //context.Database.EnsureCreated();
            //context.SeedDatabase(context);
            var masterConditionSet = context.MasterConditionSet.Include(i => i.ConditionActions).Include(i => i.ConditionSet).ThenInclude(i => i.Conditions).FirstOrDefault();
            JObject data = new JObject();
            data["PersonName"] = "Nuri";
            data["EmailAddress"] = "nuri.durmus@kizilay.org.tr";
            data["Age"] = "5";
            var tasks = new List<Task>();
            var mainConditionResult = false;
            Task<bool> methodMainResult =null;
            foreach (var conditionSet in masterConditionSet.ConditionSet)
            {
                var result = true;
                foreach (var condition in conditionSet.Conditions)
                {
                    Type type = Type.GetType("DynamicTester.Business.ConditionHelper");
                    // Create an instance of that type
                    Object obj = Activator.CreateInstance(type);
                    // Retrieve the method you are looking for
                    MethodInfo methodInfo = type.GetMethod(condition.ConditionName);
                    // Invoke the method on the instance we created above
                    methodMainResult= (Task<bool>)methodInfo.Invoke(obj, new object[] { data, condition.PropertyName, condition.ConditionValue });
                    if (!methodMainResult.GetAwaiter().GetResult().ToBoolean())
                    {
                        result = false;
                        break;
                    }
                }
                if (result)
                {
                    mainConditionResult = true;
                    break;
                }
            }
            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }


    }
}
