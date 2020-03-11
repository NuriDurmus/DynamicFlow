using DynamicBuilder.Helpers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DynamicBuilder.Business
{
    public class ConditionHelper
    {
        public async Task<bool> Contains(JObject jsonValue,string parameterName,string value)
        {
            return jsonValue[parameterName].CheckContains(value);
        }

        public async Task<bool> Equal(JObject jsonValue, string parameterName, string value)
        {
            return jsonValue[parameterName].CheckContainsEqual(value);
        }

        public async Task<bool> Bigger(JObject jsonValue, string parameterName, string value)
        {
            return jsonValue[parameterName].ToInt() > value.ToInt();
        }

        public async Task<bool> Smaller(JObject jsonValue, string parameterName, string value)
        {
            return jsonValue[parameterName].ToInt() < value.ToInt();
        }
    }
}
