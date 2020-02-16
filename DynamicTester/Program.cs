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
            var watch = System.Diagnostics.Stopwatch.StartNew();
            MasterConditionSet masterConditionSet;
            JObject data;
            GetData(out masterConditionSet, out data);

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("Data:" + elapsedMs);

            //for (int i = 0; i < 3; i++)
            //{
            //    DirectRun(masterConditionSet, data);
            //}

            for (int i = 0; i < 3; i++)
            {
                DynamicRun(out watch, masterConditionSet, data, out elapsedMs);
            }

            Console.WriteLine("Hello World!");
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

        private static void DynamicRun(out System.Diagnostics.Stopwatch watch, MasterConditionSet masterConditionSet, JObject data, out long elapsedMs)
        {
            watch = System.Diagnostics.Stopwatch.StartNew();
            Dictionary<string, MethodInfo> methodInfos = new Dictionary<string, MethodInfo>();
            Dictionary<string, object> classes = new Dictionary<string, object>();
            for (int i = 0; i < 1000000; i++)
            {
                var mainConditionResult = false;
                Task<bool> methodMainResult = null;
                foreach (var conditionSet in masterConditionSet.ConditionSet)
                {
                    var result = true;
                    foreach (var condition in conditionSet.Conditions)
                    {
                        var className = "DynamicTester.Business.ConditionHelper";
                        var fullMethodName = className + "," + condition.ConditionName;
                        MethodInfo methodInfo = null;
                        if (methodInfos.ContainsKey(fullMethodName))
                        {
                            methodInfo = methodInfos[fullMethodName];
                        }
                        else
                        {
                            Type type = Type.GetType(className);
                            // Create an instance of that type
                            Object obj = Activator.CreateInstance(type);
                            // Retrieve the method you are looking for
                            methodInfo = type.GetMethod(condition.ConditionName);
                            methodInfos.Add(fullMethodName, methodInfo);
                            if (!classes.ContainsKey(className))
                            {
                                classes.Add(className, obj);
                            }
                        }

                        // Invoke the method on the instance we created above
                        methodMainResult = (Task<bool>)methodInfo.Invoke(classes[className], new object[] { data, condition.PropertyName, condition.ConditionValue });
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
            }
            watch.Stop();
            elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("Business:" + elapsedMs);
        }

        private static long DirectRun(MasterConditionSet masterConditionSet, JObject data)
        {
            long elapsedMs;
            var watch2 = System.Diagnostics.Stopwatch.StartNew();
            ConditionHelper helper = new ConditionHelper();
            for (int i = 0; i < 1000000; i++)
            {
                var mainConditionResult = false;
                Task<bool> methodMainResult = null;
                foreach (var conditionSet in masterConditionSet.ConditionSet)
                {
                    var result = true;
                    foreach (var condition in conditionSet.Conditions)
                    {
                        if (!helper.Contains(data, "PersonName", "Nuri").GetAwaiter().GetResult())
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
            }
            watch2.Stop();
            elapsedMs = watch2.ElapsedMilliseconds;
            Console.WriteLine("Direct run:" + elapsedMs);
            return elapsedMs;
        }

    }
}
