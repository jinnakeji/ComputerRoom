using Autofac;
using Autofac.Extras.DynamicProxy;
using Coldairarrow.Api.Models;
using Coldairarrow.DataRepository;
using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Coldairarrow.Console1
{
    class Program
    {
        static Program()
        {
            var builder = new ContainerBuilder();

            var baseType = typeof(IDependency);
            //自动注入IDependency接口,支持AOP,生命周期为InstancePerDependency
            var diTypes = GlobalData.FxAllTypes
                .Where(x => baseType.IsAssignableFrom(x) && x != baseType)
                .ToArray();
            builder.RegisterTypes(diTypes)
                .AsImplementedInterfaces()
                .PropertiesAutowired()
                .InstancePerDependency()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(Interceptor));

            //AOP
            builder.RegisterType<Interceptor>();

            AutofacHelper.Container = builder.Build();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("完成");
            Console.ReadLine();
        }

        static void Main1()
        {

            Business.Enum.DataTypeEnum @enum = new Business.Enum.DataTypeEnum();
            var datatype = @enum.GetEnumName(Business.Enum.DataTypeEnum.Enum.@string);

            var json = @"D:\项目\智慧机房\Wisdomroom\WisdomComputerRoom\Coldairarrow.Console\data.json";
            var text = System.IO.File.ReadAllText(json);
            var aa = ConvertData(text);
        }


        public static List<RemoteModel> ConvertData(string json)
        {
            JObject jObject = JObject.Parse(json);
            var datas = new List<RemoteModel>();
            foreach (var item in jObject)
            {
                var model = new RemoteModel();
                datas.Add(model);
                model.DeviceType = item.Key;
                item.Value.ToArray().ForEach(aa =>
                 {
                     var device = new Device()
                     {
                         NodeNumber = Convert.ToInt32(aa["nodeNumber"]),
                         TimeStamp = Convert.ToInt32(aa["timeStamp"])
                     };
                     model.DeviceList.Add(device);
                     foreach (JProperty b in aa)
                     {
                         if (b.Name == "nodeNumber" || b.Name == "timeStamp")
                             continue;
                         device[b.Name] = b.Value.ToString();
                     }
                 });
            }

            return datas;
        }
    }
}