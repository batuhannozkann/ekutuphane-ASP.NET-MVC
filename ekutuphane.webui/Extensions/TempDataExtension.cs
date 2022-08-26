using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;

namespace ekutuphane.webui.Extensions
{
    public static class TempDataExtension
    {
        public static void Put<T>(this ITempDataDictionary tempData,string key,T value)
        where T:class
        {
            tempData[key]=JsonConvert.SerializeObject(value);
        }
        public static T Get<T>(this ITempDataDictionary tempData,string key)
        where T:class
        {
            tempData.TryGetValue(key, out object result);
            return result==null?null:JsonConvert.DeserializeObject<T>((string)result);
        }
    }
}