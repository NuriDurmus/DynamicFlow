using DynamicBuilder.Helpers;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DynamicBuilder
{
    public class Invoker
    {
        public void Invoke(List<Tuple<Task<object>,MethodInfo>> tasks)
        {
            foreach (var task in tasks)
            {
                var taskResult = task.Item1.GetAwaiter().GetResult();
                var resultProperty = taskResult?.GetType().GetProperty("Result");
                var value = resultProperty?.GetValue(taskResult);
                Console.WriteLine("{0} metodu çalıştırması tamamlandı. Result: {1}", task.Item2.Name, value.ToCustomString()); 
            }
        }
    }
}
