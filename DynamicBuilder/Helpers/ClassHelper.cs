using System;
using System.Collections.Generic;
using System.Text;

namespace DynamicBuilder.Helpers
{
    public static class ClassHelper
    {
        public static string GetClassName(this string value)
        {
            return value.Substring(0, value.LastIndexOf('.'));
        }

        public static string GetMethodName(this string value)
        {
            return value.Substring(value.LastIndexOf('.') + 1, value.Length - value.LastIndexOf('.') - 1);
        }
    }
}
