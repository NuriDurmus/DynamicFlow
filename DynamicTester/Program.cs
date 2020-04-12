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
            var test = new BuilderTest();
            test.Run();
            Console.ReadLine();
        }
    }
}
