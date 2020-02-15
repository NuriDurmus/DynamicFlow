using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Xml.Serialization;

namespace DynamicTester.Helpers
{
    public static class ConverterHelper
    {
        public static string UppercaseFirst(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            return char.ToUpper(s[0]) + s.Substring(1);
        }
        public static decimal ToDecimal(this Object obj)
        {
            decimal result = 0;
            decimal temp = 0;
            if (Decimal.TryParse(obj.ToString(), out temp))
            {
                result = temp;
            }
            return result;
        }
        public static int ToInt(this Object obj)
        {
            int result = 0;
            int temp = 0;
            if (Int32.TryParse(obj.ToString().Trim(), out temp))
            {
                result = temp;
            }
            return result;
        }

        public static long ToLong(this Object obj)
        {
            long result = 0;
            long temp = 0;
            if (long.TryParse(obj.ToString().Trim(), out temp))
            {
                result = temp;
            }
            return result;
        }

        public static uint ToUInt(this Object obj)
        {
            uint result = 0;
            uint temp = 0;
            if (UInt32.TryParse(obj.ToString().Trim(), out temp))
            {
                result = temp;
            }
            return result;
        }

        public static string ToDateTime(this Object obj, string dateFormat)
        {
            string result = string.Empty;
            DateTime date = DateTime.MinValue;
            if (obj != null)
            {
                if (DateTime.TryParse(obj.ToString(), out date))
                {
                    result = date.ToString(string.IsNullOrEmpty(dateFormat) ? "dd/MM/yyyy" : dateFormat);
                }
            }
            return result;
        }

        public static DateTime? ToNullableDateTime(this Object obj)
        {
            string result = string.Empty;

            if (obj.ToCustomString() != string.Empty)
            {
                DateTime date;
                DateTime.TryParse(obj.ToString(), out date);
                return date;
            }
            return null;
        }

        public static DateTime ToDateTime(this Object obj)
        {
            string result = string.Empty;
            DateTime date = new DateTime();
            if (obj != null)
            {
                DateTime.TryParse(obj.ToString(), out date);
                return date;
            }
            return date;
        }


        /// <summary>
        /// Gelen date time culture info bilgisine göre istenilen formatta çeviri yapar.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="cultureInfo">gönderilen tarihin culture info bilgisi Örn:18/01/2018 için new CultureInfo("tr-tr") girilir.</param>
        /// <param name="convertDateFormat"></param>
        /// <returns></returns>
        public static string ToDateTime(this Object obj, CultureInfo cultureInfo, string convertDateFormat)
        {
            string result = string.Empty;
            DateTime dateTime = DateTime.MinValue;
            if (obj != null)
            {
                var currentCulture = Thread.CurrentThread.CurrentCulture;
                Thread.CurrentThread.CurrentCulture = cultureInfo;
                if (DateTime.TryParse(obj.ToString(), out dateTime))
                {
                    result = dateTime.ToString(string.IsNullOrEmpty(convertDateFormat) ? "dd/MM/yyyy" : convertDateFormat);
                    Thread.CurrentThread.CurrentCulture = currentCulture;
                    result = dateTime.ToString(string.IsNullOrEmpty(convertDateFormat) ? "dd/MM/yyyy" : convertDateFormat);
                }
                Thread.CurrentThread.CurrentCulture = cultureInfo;
            }
            return result;
        }

        /// <summary>
        /// herhangi bir parametre değerini içeriyorsa true dönecektir.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static bool CheckContains(this Object obj, params object[] parameters)
        {
            bool check = false;
            string text = obj.ToCustomString();
            if (text != string.Empty)
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    if (text.RemoveDiacritics().Contains(parameters[i].RemoveDiacritics()))
                    {
                        check = true; break;
                    }
                }
            }
            return check;
        }

        /// <summary>
        /// Verilen parametrelerden herhangi birine eşitse
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static bool CheckContainsEqual(this Object obj, params object[] parameters)
        {
            bool check = false;
            string text = obj.ToString();
            if (text != string.Empty)
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    if (text.RemoveDiacritics() == parameters[i].RemoveDiacritics())
                    {
                        check = true; break;
                    }
                }
            }
            return check;
        }



        public static bool CheckStringEqual(this Object obj, object value)
        {
            return obj.ToString().RemoveDiacritics().ToLower().Trim() == value.ToString().RemoveDiacritics().ToLower().Trim();
        }

        /// <summary>
        /// Metindeki türkçe karakterleri ingilizce karakterlere çevirir
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string RemoveDiacritics(this Object obj)
        {
            string text = obj.ToString();
            if (text == string.Empty)
            {
                return string.Empty;
            }
            Encoding srcEncoding = Encoding.UTF8;
            Encoding destEncoding = Encoding.GetEncoding(1252); // Latin alphabet

            text = destEncoding.GetString(Encoding.Convert(srcEncoding, destEncoding, srcEncoding.GetBytes(text)));

            string normalizedString = text.Normalize(NormalizationForm.FormD);
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < normalizedString.Length; i++)
            {
                if (!CharUnicodeInfo.GetUnicodeCategory(normalizedString[i]).Equals(UnicodeCategory.NonSpacingMark))
                {
                    result.Append(normalizedString[i]);
                }
            }
            CultureInfo ci = new CultureInfo("en");
            return result.ToString().ToLower(ci);
        }

        /// <summary>
        /// Null durumunda empty string döndürür.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToCustomString(this Object obj)
        {
            string result = string.Empty;
            if (obj != null)
            {
                result = obj.ToString();
            }
            return result;
        }


        /// <summary>
        /// Verilen objenin string halini döndürür.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToWebString(this Object obj)
        {
            string result = obj.ToCustomString();
            if (obj != "")
            {
                result = System.Web.HttpUtility.JavaScriptStringEncode(result);
            }
            return result;
        }

        public static bool IsEmpty(this Object obj)
        {
            return obj.ToCustomString() == string.Empty;
        }

        public static DateTime ToDateTime(this string obj, char delimeter)
        {
            DateTime result = new DateTime();
            if (obj != null)
            {
                var parts = obj.Split(delimeter);
                result = new DateTime(parts[2].ToInt(), parts[1].ToInt(), parts[0].ToInt());
            }
            return result;
        }

        /// <summary>
        /// Verilen objenin guid halini döndürür.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Guid ToGuid(this Object obj)
        {
            Guid result = Guid.Empty;
            if (obj != null)
            {
                result = Guid.Parse(obj.ToString());
            }
            return result;
        }

        public static double ToDouble(this Object obj)
        {
            double result = 0;
            double temp = 0;
            if (Double.TryParse(obj.ToString(), out temp))
            {
                result = temp;
            }
            return result;
        }

        /// <summary>
        /// belirtilen culture bilgisine göre double tipine çevirir.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public static double ToDouble(this Object obj, CultureInfo culture)
        {
            double result = 0;
            double temp = 0;
            if (Double.TryParse(obj.ToString(), out temp))
            {
                result = Convert.ToDouble(temp, culture);
            }
            return result;
        }

        public static bool ToBoolean(this Object obj)
        {
            bool result = false;
            bool temp;
            if (Boolean.TryParse(obj.ToString(), out temp))
            {
                result = temp;
            }
            return result;
        }


        /// <summary>
        /// İlgili stringin içerisindeki son verilen char indexinden sonraki ifadeyi alır.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>Örn: "test.pdf" pdf</returns>
        public static string GetLastCharsFromLastIndexOfSeperator(this Object obj, char seperator = '.', bool includeSeperator = false)
        {
            string result = string.Empty;
            if (obj != null)
            {
                if (obj.ToString().LastIndexOf(seperator) > 0)
                {
                    result = obj.ToString().Substring(obj.ToString().LastIndexOf(seperator) + 1, obj.ToString().Length - obj.ToString().LastIndexOf(seperator) - 1) + (includeSeperator ? seperator.ToString() : "");
                }
            }
            return result;
        }

        /// <summary>
        /// İlgili stringin içerisindeki son verilen char indexinden önceki ifadeyi alır.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>Örn: "test.pdf" test</returns>
        public static string GetFirstCharsFromLastIndexOfSeperator(this Object obj, char seperator = '.', bool includeSeperator = false)
        {
            string result = string.Empty;
            if (obj != null)
            {
                if (obj.ToString().LastIndexOf(seperator) > 0)
                {
                    result = obj.ToString().Substring(0, obj.ToString().LastIndexOf(seperator)) + (includeSeperator ? seperator.ToString() : "");
                }
            }
            return result;
        }

        public static string GetJsonString<T>(this T entityObject)
        {
            return JsonConvert.SerializeObject(entityObject, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }
        public static string ToXml<T>(this T obj, string strContainer = "")
        {
            XmlSerializer serializer = null;

            if (strContainer.Trim() == "")
                serializer = new XmlSerializer(typeof(T));
            else
                serializer = new XmlSerializer(typeof(T), new XmlRootAttribute(strContainer));

            using (StringWriter sw = new StringWriter())
            {
                serializer.Serialize(sw, obj);
                return sw.ToString();
            }
        }

        public static string GetDescriptionFromEnumValue<T>(int value)
        {
            foreach (var item in Enum.GetValues(typeof(T)))
            {
                if ((int)item == value)
                {
                    return item.GetDescription();
                }
            }
            return "";
        }

        public static string GetDescription<T>(this T value)
        {
            var enumMember = value.GetType().GetMember(value.ToString()).FirstOrDefault();
            var descriptionAttribute =
                enumMember == null
                    ? default(DescriptionAttribute)
                    : enumMember.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute;
            return
                descriptionAttribute == null
                    ? value.ToString()
                    : descriptionAttribute.Description;
        }

        public static T CloneWithSerialization<T>(this T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }

            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            System.Runtime.Serialization.IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }

        public static T Clone<T>(this T o) where T : new()
        {
            return (T)CloneObject(o);
        }
        static object CloneObject(object obj)
        {
            if (ReferenceEquals(obj, null)) return null;

            var type = obj.GetType();
            if (type.IsValueType || type == typeof(string))
                return obj;
            else if (type.IsArray)
            {
                var array = obj as Array;
                var arrayType = Type.GetType(type.FullName.Replace("[]", string.Empty));
                var arrayInstance = Array.CreateInstance(arrayType, array.Length);

                for (int i = 0; i < array.Length; i++)
                    arrayInstance.SetValue(CloneObject(array.GetValue(i)), i);
                return Convert.ChangeType(arrayInstance, type);
            }
            else if (type.IsClass)
            {
                var instance = Activator.CreateInstance(type);
                var fields = type.GetFields(BindingFlags.Public |
                            BindingFlags.NonPublic | BindingFlags.Instance);

                foreach (var field in fields)
                {
                    var fieldValue = field.GetValue(obj);
                    if (ReferenceEquals(fieldValue, null)) continue;
                    field.SetValue(instance, CloneObject(fieldValue));
                }
                return instance;
            }
            else
                return null;
        }

       
}
}
