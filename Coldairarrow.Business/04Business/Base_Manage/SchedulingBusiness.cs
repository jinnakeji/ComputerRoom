using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace Coldairarrow.Business.Base_Manage
{
    public class SchedulingBusiness : BaseBusiness<Scheduling>, ISchedulingBusiness, IDependency
    {
        #region 外部接口

        public List<SchedulingDTO> GetDataList(Pagination pagination, string condition, string keyword)
        {
            //var q = GetIQueryable();
            //var where = LinqHelper.True<Scheduling>();
            Expression<Func<Scheduling, TeamTable, Shifts, SchedulingDTO>> select = (a, b, c) => new SchedulingDTO
            {
                TeamTableName = b.TeamTableName,
                ShiftsName = c.ShiftsName
            };
            select = select.BuildExtendSelectExpre();
            var q_User = false ? Service.GetIQueryable<Scheduling>() : GetIQueryable();
            var q = from a in q_User.AsExpandable()
                    join b in Service.GetIQueryable<TeamTable>() on a.TeamTableId equals b.Id into ab
                    from b in ab.DefaultIfEmpty()
                    join c in Service.GetIQueryable<Shifts>() on a.ShiftsId equals c.Id into abc
                    from e in abc.DefaultIfEmpty()
                    select @select.Invoke(a, b, e);
            var where = LinqHelper.True<SchedulingDTO>();
            //筛选
            if (!condition.IsNullOrEmpty() && !keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<Scheduling, bool>(
                    ParsingConfig.Default, false, $@"{condition}.Contains(@0)", keyword);
                // where = where.And(newWhere);
                where = where.And(x =>
                    EF.Functions.Like(x.TeamTableName, keyword)
                    || EF.Functions.Like(x.ShiftsName, keyword));
            }

            return q.ToList();//Where(where).GetPagination(pagination).OrderBy(x => x.OfficeDate).ToList();
        }

        public Scheduling GetTheData(string id)
        {
            return GetEntity(id);
        }

        public AjaxResult AddData(Scheduling data, string[] OnOffDate, string[] TeamTableId, string[] ShiftsId, string[] strRestDay)
        {
            var teamlist = Service.GetIQueryable<TeamTable>().Where(x => TeamTableId.Contains(x.Id)).ToList<TeamTable>();
            var shiflist = Service.GetIQueryable<Shifts>().Where(x => ShiftsId.Contains(x.Id)).ToList<Shifts>();

            Schedu(data, teamlist, OnOffDate, shiflist, strRestDay);

            // Insert(data);

            return Success();
        }

        public AjaxResult UpdateData(Scheduling data)
        {
            Update(data);

            return Success();
        }

        public AjaxResult DeleteData(List<string> ids)
        {
            Delete(ids);

            return Success();
        }

        public AjaxResult DeleteData()
        {
            DeleteAll();

            return Success();
        }

        #endregion 外部接口

        #region 私有成员

        /// <summary>
        /// 排班
        /// </summary>
        /// <param name="data">创建人信息</param>
        /// <param name="teamlist">班组</param>
        /// <param name="OnOffDate">开始时间/结束时间</param>
        /// <param name="shiflist">班次(早中晚)</param>
        /// <param name="strRestDay">周末</param>

        public void Schedu(Scheduling data, List<TeamTable> teamlist, string[] OnOffDate, List<Shifts> shiflist, string[] strRestDay)
        {
            int? changeShiftsCount = 1;
            List<Scheduling> schedulist = new List<Scheduling>();

            DateTime startDate = Convert.ToDateTime(OnOffDate[0]);//初始化一个日期
            DateTime endDate = Convert.ToDateTime(OnOffDate[1]).AddDays(1);//获取今天日期
            var currentDate = startDate;
            var index = 0;

            while (true)
            {
                var currentWeek = this.GetWeekNow(currentDate);

                if (strRestDay != null && strRestDay.Length > 0 && strRestDay.Contains(currentWeek))
                {
                    goto label;
                }

                foreach (var shift in shiflist)
                {
                    var treamId = teamlist[index % teamlist.Count].Id;

                    var model = new Scheduling();
                    model.Id = Guid.NewGuid().ToString();
                    model.CreatorRealName = data.CreatorRealName;
                    model.CreatorId = data.CreatorId;
                    model.CreateTime = data.CreateTime;
                    model.ShiftsId = shift.Id;
                    model.OfficeDate = AddTimeSpan(currentDate, shift.WorkTimeStart);

                    if ((ConvertTimeSpan(shift.WorkTimeEnd) - ConvertTimeSpan(shift.WorkTimeStart)).Ticks > 0)
                        model.GoOffWork = AddTimeSpan(currentDate, shift.WorkTimeEnd);
                    else
                        model.GoOffWork = AddTimeSpan(currentDate.AddDays(1), shift.WorkTimeEnd);

                    model.TeamTableId = treamId;
                    schedulist.Add(model);
                    index++;
                }

                label:
                if (currentDate >= endDate)
                    break;
                currentDate = currentDate.AddDays(1);
            }

            BulkInsert(schedulist);
            return;

            DateTime AddTimeSpan(DateTime dateTime, string timespan)
            {
                var arr = timespan.Split(':');
                dateTime = dateTime.AddHours(int.Parse(arr[0]));
                dateTime = dateTime.AddMinutes(int.Parse(arr[1]));
                dateTime = dateTime.AddSeconds(int.Parse(arr[2]));
                return dateTime;
            }

            TimeSpan ConvertTimeSpan(string timespan)
            {
                return TimeSpan.Parse(timespan);
            }
        }

        // 判断当前时间是周几
        private string GetWeekNow(DateTime d)
        {
            string strWeek = d.DayOfWeek.ToString();// DateTime.Now.DayOfWeek.ToString();
            switch (strWeek)
            {
                case "Monday":
                    return "1";

                case "Tuesday":
                    return "2";

                case "Wednesday":
                    return "3";

                case "Thursday":
                    return "4";

                case "Friday":
                    return "5";

                case "Saturday":
                    return "6";

                case "Sunday":
                    return "7";
            }
            return "0";
        }

        #endregion 私有成员
    }

    public class SchedulingDTO : Scheduling
    {
        /// <summary>
        /// 班组名称
        /// </summary>
        public String TeamTableName { get; set; }

        /// <summary>
        /// 班次名称
        /// </summary>
        public String ShiftsName { get; set; }

        public string OfficeDateText { get => OfficeDate.ToString("yyyy-MM-dd"); }
    }
}