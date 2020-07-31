using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Entity.MeterReaDing;
using Coldairarrow.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace Coldairarrow.Business.MeterReaDing
{
    public class MeterReaDingOnDutyBusiness : BaseBusiness<MeterReaDingOnDuty>, IMeterReaDingOnDutyBusiness, IDependency
    {
        #region 外部接口

        public List<MeterReaDingOnDuty> GetDataList(Pagination pagination, string condition, string keyword)
        {
            var q = GetIQueryable();
            var where = LinqHelper.True<MeterReaDingOnDuty>();

            //筛选
            if (!condition.IsNullOrEmpty() && !keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<MeterReaDingOnDuty, bool>(
                    ParsingConfig.Default, false, $@"{condition}.Contains(@0)", keyword);
                where = where.And(newWhere);
            }

            return q.Where(where).GetPagination(pagination).ToList();
        }

        public List<MeterReaDingOnDutyDTO> GetDataDtoList(string deptId)
        {
            //Expression<Func<MeterReaDingOnDuty, MeterReaDingTimeSetUp, Base_User, MeterReaDingOnDutyDTO>> select = (a, b, c) => new MeterReaDingOnDutyDTO
            //{
            //    MeterName = b == null ? "" : b.MeterName,
            //    MeterTime = a == null ? "" : a.CreateTime.ToString(),
            //    RangeTime = b == null ? "" : b.RangeTime,
            //    RealName = c == null ? "" : c.RealName

            //};
            //select = select.BuildExtendSelectExpre();
            //var q = from a in Service.GetIQueryable<MeterReaDingOnDuty>()
            //        join b in Service.GetIQueryable<MeterReaDingTimeSetUp>() on a.MeterReaDingTimeSetUpId equals b.Id into ab
            //        from b in ab.DefaultIfEmpty()
            //        join e in Service.GetIQueryable<Base_User>() on a.ConfirmUserId equals e.Id into ae
            //        from e in ae.DefaultIfEmpty()
            //        select @select.Invoke(a, b, e);
            //var where = LinqHelper.True<MeterReaDingOnDutyDTO>();

            //var list = q.Where(where).Where(x => x.deptId == deptId && x.CreateTime.Value.Date == DateTime.Now.Date).ToList();

            var current = DateTime.Now;
            var startDate = Convert.ToDateTime(current.ToString("yyyy-MM-dd"));
            var endDate = startDate.AddDays(1);


            string sql = string.Format(@"
                                   select 
	                    c.*,
 	                    (case when c.ConfirmUserId is null then '自动抄表' else bu.RealName end) RealName
 	                    from 
		                    (select  b.* from 
                                ( SELECT 
				                    moduleName, 
				                    ConfirmUserId, 
				                    CreateTime, 
				                    deptid,deviceName, 
	                                stuff(
		                                (
			                                SELECT distinct
				                                ',' +propName+':'+propValue
			                                FROM
				                                MeterReaDingOnDuty a
			                                WHERE
				                                a.deviceName = temp.deviceName
			                                FOR xml path ('')
		                                ),
		                                1,
		                                1,
		                                ''
	                                ) AS MeterReaDingOnDutyData
                                    FROM MeterReaDingOnDuty temp
                                    GROUP BY 
					                    temp.CreateTime,
					                    temp.moduleName,
					                    temp.deptid,
					                    temp.deviceName,
					                    temp.ConfirmUserId 
			                    ) as b ,Base_User u
			                    where b.deptId=u.DepartmentId  and b.CreateTime>='{0}' and b.CreateTime<'{1}'
		                    )c ,Base_User bu 
		                    where c.ConfirmUserId=bu.Id  or c.ConfirmUserId is null and bu.DepartmentId=c.deptId
					                and c.deptId={2}", startDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd"), deptId);


            var dataTable = Service.GetDataTableWithSql(sql);

            List<MeterReaDingOnDutyDTO> list = new List<MeterReaDingOnDutyDTO>();

            foreach (var item in dataTable.Rows.Cast<DataRow>())
            {
                MeterReaDingOnDutyDTO model = new MeterReaDingOnDutyDTO();

                model.deptId = item["deptId"].ToString();
                model.ConfirmUserId = item["ConfirmUserId"].ToString();
                model.CreateTime = Convert.ToDateTime(item["CreateTime"]);
                model.deviceName = item["deviceName"].ToString();
                model.MeterReaDingOnDutyData = item["MeterReaDingOnDutyData"].ToString();
                model.RealName = item["RealName"].ToString();
                model.moduleName = item["moduleName"].ToString();
                list.Add(model);
            }

            return list;
        }

        public List<MeterReaDingOnDuty> GetDataReport(string userid)
        {
            var q = GetIQueryable();
            var where = LinqHelper.True<MeterReaDingOnDuty>();

            //筛选
            if (!userid.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<MeterReaDingOnDuty, bool>(
                    ParsingConfig.Default, false, $@"UserId==(@0)", userid);
                where = where.And(newWhere);
            }
            //return q.Where(where).ToList();
            var reportlist = q.Where(where).Where(x => x.CreateTime.Value.Date == DateTime.Now.Date).ToList();

            return reportlist;


        }
        public MeterReaDingOnDuty GetTheData(string id)
        {
            return GetEntity(id);
        }

        public AjaxResult AddData(MeterReaDingOnDuty data)
        {
            Insert(data);

            return Success();
        }

        public AjaxResult UpdateData(MeterReaDingOnDuty data)
        {
            Update(data);

            return Success();
        }

        public AjaxResult DeleteData(List<string> ids)
        {
            Delete(ids);

            return Success();
        }

        public List<MeterReaDingOnDuty> GetMeterReaDingBydeptIdBystartTimeByendTime(string deptId, string startTime, string endTime)
        {

            var q = GetIQueryable();
            var where = LinqHelper.True<MeterReaDingOnDuty>();

            //筛选
            if (!deptId.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<MeterReaDingOnDuty, bool>(
                    ParsingConfig.Default, false, $@"deptId==(@0)", deptId);
                where = where.And(newWhere);
            }
            var reportlist = q.Where(where).Where(x => x.CreateTime.Value.Date > Convert.ToDateTime(startTime) && x.CreateTime < Convert.ToDateTime(endTime)).ToList();

            return reportlist;

        }

        #endregion

        #region 私有成员

        #endregion

        #region 数据模型

        #endregion
    }
    public class MeterReaDingOnDutyDTO : MeterReaDingOnDuty
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string MeterName { get; set; }
        /// <summary>
        /// 抄表时间
        /// </summary>
        public string MeterTime { get; set; }

        public string RangeTime { get; set; }
        /// <summary>
        /// 抄表姓名
        /// </summary>
        public String RealName { get; set; }
        // public string CreateTimeText { get => CreateTime.ToString("yyyy-MM-dd"); }
        public string MeterReaDingOnDutyData { get; set; }
    }

}