using Coldairarrow.Business;
using System;

namespace Coldairarrow.Util
{
    public static partial class Extention
    {
        public static void InitEntity(this object entity)
        {
            var op = AutofacHelper.GetScopeService<IOperator>();

            if (entity.ContainsProperty("Id"))
            {
                if (entity.GetPropertyType("Id") == typeof(string))
                    entity.SetPropertyValue("Id", IdHelper.GetId());
            }
            if (entity.ContainsProperty("CreateTime"))
                entity.SetPropertyValue("CreateTime", DateTime.Now);
            if (entity.ContainsProperty("CreatorId"))
                entity.SetPropertyValue("CreatorId", op?.UserId);
            if (entity.ContainsProperty("CreatorRealName"))
                entity.SetPropertyValue("CreatorRealName", op?.Property?.RealName);
        }
    }
}
