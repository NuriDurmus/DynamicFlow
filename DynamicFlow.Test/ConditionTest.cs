using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Persistence.Models;
using System;
using System.Linq;
using Xunit;
using DynamicBuilder;
using Xunit.Abstractions;
using System.Diagnostics;

namespace DynamicFlow.Test
{
    public class ConditionTest
    {
        private readonly ITestOutputHelper output;

        public JObject data;
        public MasterConditionSet masterConditionSet;
        public ConditionTest(ITestOutputHelper output)
        {
            this.output = output;
            GetData(out masterConditionSet, out data);
        }
        [Fact]
        public void Condition_Should_Return_False_When_Given_False_Conditions()
        {
            //masterConditionSet
            //PersonName contains nuri
            //Age bigger 18
            data["Age"] = 5;
            Assert.False(Builder.RunCondition(masterConditionSet, data));

            data["PersonName"] = "feawf";
            data["Age"] = 19;
            Assert.False(Builder.RunCondition(masterConditionSet, data));
        }

        [Fact]
        public void Condition_Should_Return_True_When_Given_True_Conditions()
        {
            //masterConditionSet
            //PersonName contains nuri
            //Age bigger 18
            data["Age"] = 19;
            data["PersonName"] = "Nuri";
            Assert.True(Builder.RunCondition(masterConditionSet, data));
            output.WriteLine("tamamlandı");
        }

        //TODO:Metot parametreleri (bigger gibi kontrol metotları) test edilecek
        private void GetData(out MasterConditionSet masterConditionSet, out JObject data)
        {
            var context = new ConditionHandlerContext();
            masterConditionSet = context.MasterConditionSet.Include(i => i.ConditionActions).Include(i => i.ConditionSet).ThenInclude(i => i.Conditions).FirstOrDefault();
            //işlem yapılacak veri
            data = new JObject();
            data["PersonName"] = "Nuri";
            data["EmailAddress"] = "nuri.durmus@kizilay.org.tr";
            data["Age"] = "5";
        }

    }
}
