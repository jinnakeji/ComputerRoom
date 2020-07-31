using Coldairarrow.Entity.Device;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Coldairarrow.Business.Device
{
    public class T_ShowDevicePropBusiness : BaseBusiness<T_ShowDeviceProp>, IT_ShowDevicePropBusiness, IDependency
    {
        #region 外部接口

        public List<V_ShowDeviceProp> GetDataList(Pagination pagination, string condition, string keyword)
        {
            var q = Service.GetIQueryable<V_ShowDeviceProp>();
            var where = LinqHelper.True<V_ShowDeviceProp>();

            //筛选
            if (!condition.IsNullOrEmpty() && !keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<V_ShowDeviceProp, bool>(
                    ParsingConfig.Default, false, $@"{condition}.Contains(@0)", keyword);
                where = where.And(newWhere);
            }

            return q.Where(where).OrderBy(x => x.DeviceTypeId).ThenBy(x => x.PropId).GetPagination(pagination).ToList();
        }

        public T_ShowDeviceProp GetTheData(string id)
        {
            return GetEntity(id);
        }

        public AjaxResult AddData(T_ShowDeviceProp data)
        {
            Insert(data);

            return Success();
        }

        public AjaxResult UpdateData(T_ShowDeviceProp data)
        {
            Update(data);

            return Success();
        }

        public AjaxResult DeleteData(List<string> ids)
        {
            Delete(ids);

            return Success();
        }

        public AjaxResult SaveData(T_ShowDeviceProp model)
        {
            var entity = GetIQueryable().FirstOrDefault(x => x.DeviceTypeId == model.DeviceTypeId && x.PropId == model.PropId);
            if (entity != null)
            {
                entity.IsShow = model.IsShow;
                Update(entity);
            }
            else
                Insert(model);

            return Success();
        }

        #endregion

        #region 私有成员

        #endregion

        #region 数据模型

        #endregion
    }
}