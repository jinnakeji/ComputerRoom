using Coldairarrow.Entity.Device;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Coldairarrow.Business.Device
{
    public class T_DeviceDataBusiness : BaseBusiness<T_DeviceData>, IT_DeviceDataBusiness, IDependency
    {
        #region 外部接口

        public List<T_DeviceData> GetDataList(Pagination pagination, string condition, string keyword)
        {
            var q = GetIQueryable();
            var where = LinqHelper.True<T_DeviceData>();

            //筛选
            if (!condition.IsNullOrEmpty() && !keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<T_DeviceData, bool>(
                    ParsingConfig.Default, false, $@"{condition}.Contains(@0)", keyword);
                where = where.And(newWhere);
            }

            return q.Where(where).GetPagination(pagination).ToList();
        }

        public List<V_DeviceData> GetDataViewList(Pagination pagination, string condition, string keyword)
        {
            var q = Service.GetIQueryable<V_DeviceData>();
            var where = LinqHelper.True<V_DeviceData>();

            //筛选
            if (!condition.IsNullOrEmpty() && !keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<V_DeviceData, bool>(
                    ParsingConfig.Default, false, $@"{condition}.Contains(@0)", keyword);
                where = where.And(newWhere);
            }

            return q.Where(where).GetPagination(pagination).ToList();
        }

        public List<V_LastData> GetLastData()
        {
            return Service.GetIQueryable<V_LastData>().ToList();
        }

        public List<T_DeviceData> GetLastDataByDepartmentId(string departmentId)
        {
            var datas = Service.GetListBySql<T_DeviceData>("EXEC P_GetDataForModuleId @departmentId=@departmentId",
                 new List<System.Data.Common.DbParameter>() {
                 new System.Data.SqlClient.SqlParameter("@departmentId",departmentId)
                 });

            return datas;
        }

        public T_DeviceData GetTheData(string id)
        {
            return GetEntity(id);
        }

        public AjaxResult AddData(T_DeviceData data)
        {
            Insert(data);

            return Success();
        }

        public AjaxResult UpdateData(T_DeviceData data)
        {
            Update(data);

            return Success();
        }

        public AjaxResult DeleteData(List<string> ids)
        {
            Delete(ids);

            return Success();
        }

        public AjaxResult AddData(List<T_DeviceData> datas)
        {
            Insert(datas);
            return Success();
        }

        //public void BulkInsert(List<T_DeviceData> datas)
        //{
        //    Service.BulkInsert(datas);
        //}

        #endregion

        #region 私有成员

        #endregion

        #region 数据模型

        #endregion
    }
}