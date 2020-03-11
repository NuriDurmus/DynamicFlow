using DynamicBuilder.Helpers;
using Newtonsoft.Json.Linq;
using Persistence.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DynamicBuilder
{
    public static class Builder
    {
        public static void RunCondition(MasterConditionSet masterConditionSet, JObject data)
        {
            Dictionary<string, MethodInfo> methodInfos = new Dictionary<string, MethodInfo>();
            Dictionary<string, object> classes = new Dictionary<string, object>();
            var mainConditionResult = false;
            Task<bool> methodMainResult = null;
            foreach (var conditionSet in masterConditionSet.ConditionSet)
            {
                var result = true;
                foreach (var condition in conditionSet.Conditions)
                {
                    var className = condition.ConditionName.GetClassName();
                    var methodName = condition.ConditionName.GetMethodName();
                    MethodInfo methodInfo = null;
                    if (methodInfos.ContainsKey(condition.ConditionName))
                    {
                        methodInfo = methodInfos[condition.ConditionName];
                    }
                    else
                    {
                        Type type = Type.GetType(className);
                        Object obj = Activator.CreateInstance(type);
                        methodInfo = type.GetMethod(methodName);
                        methodInfos.Add(condition.ConditionName, methodInfo);
                        if (!classes.ContainsKey(className))
                        {
                            classes.Add(className, obj);
                        }
                    }

                    methodMainResult = (Task<bool>)methodInfo.Invoke(classes[className], new object[] { data, condition.PropertyName, condition.ConditionValue });
                    if (!methodMainResult.GetAwaiter().GetResult().ToBoolean())
                    {
                        result = false;
                        Console.WriteLine(className + "data {0}, condition property name:{1} value {2} conditionname {3} ", data[condition.PropertyName], condition.PropertyName, condition.ConditionValue, methodName);
                        break;
                    }
                }
                if (result)
                {
                    mainConditionResult = true;
                    break;
                }
            }
            Console.WriteLine("main result:" + mainConditionResult);
        }

        public async static void RunAction(MasterConditionSet masterConditionSet)
        {
            Dictionary<string, MethodInfo> methodInfos = new Dictionary<string, MethodInfo>();
            Dictionary<string, object> classes = new Dictionary<string, object>();
            var mainConditionResult = false;
            Task<object> methodMainResult = null;
            foreach (var conditionSet in masterConditionSet.ConditionActions.OrderBy(i => i.OrderId))
            {
                var result = true;
                //TODO: assembly ile ilgili optimizasyon yapılacak.
                Assembly assembly = Assembly.LoadFrom("DynamicFlow.Business.dll");
                var className = conditionSet.ActionName.GetClassName();
                var methodName = conditionSet.ActionName.GetMethodName();
                MethodInfo methodInfo = null;
                if (methodInfos.ContainsKey(conditionSet.ActionName))
                {
                    methodInfo = methodInfos[conditionSet.ActionName];
                }
                else
                {
                    Type type = assembly.GetType(className);
                    Object obj = Activator.CreateInstance(type);
                    methodInfo = type.GetMethod(methodName);
                    methodInfos.Add(conditionSet.ActionName, methodInfo);
                    if (!classes.ContainsKey(className))
                    {
                        classes.Add(className, obj);
                    }
                }
                var parameters = new List<object>();
                parameters.AddRange(conditionSet.ActionParameterValues.Split(',').ToList());
                parameters.Add(true);
                try
                {
                    //TODO: exception yakalama hatası çözülecek.
                    Task task = Task.Run(() =>
                    {
                        methodInfo.Invoke(classes[className], parameters.ToArray());
                    });
                    task.GetAwaiter().GetResult();
                    //methodMainResult = (Task<object>)methodInfo.Invoke(classes[className], parameters.ToArray());
                    //if (!methodMainResult.GetAwaiter().IsCompleted)
                    //{
                    //    result = false;
                    //    break;
                    //}
                    Console.WriteLine("tamamlandı");
                }
                catch (Exception ex)
                {
                    result = false;
                    Console.WriteLine("custom mesaj:"+ex.Message);
                    break;
                }
            }
        }
    }
}
