using Coldairarrow.Business.Base_Manage;
using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Coldairarrow.Api.Controllers.Base_Manage
{
    [Route("/Base_Manage/[controller]/[action]")]
    public class SchedulingController : BaseApiController
    {
        #region DI

        public SchedulingController(ISchedulingBusiness schedulingBus)
        {
            _schedulingBus = schedulingBus;
        }

        ISchedulingBusiness _schedulingBus { get; }

        #endregion

        #region 获取

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="condition">查询字段</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<AjaxResult<List<Scheduling>>> GetDataList(Pagination pagination, string condition, string keyword)
        {
            var dataList = _schedulingBus.GetDataList(pagination, condition, keyword);

            return DataTable(dataList, pagination);
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<AjaxResult<Scheduling>> GetTheData(string id)
        {
            var theData = _schedulingBus.GetTheData(id);

            return Success(theData);
        }

        #endregion

        #region 提交

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="data">保存的数据</param>
        [HttpPost]
        public ActionResult<AjaxResult> SaveData(Scheduling data,string[] OnOffDate, string[] TeamTableId, string[] ShiftsId, string[] strRestDay)
        {
            AjaxResult res;
            //var roleIds = hiftsIdsJson?.ToList<string>() ?? new List<string>();
            if (data.Id.IsNullOrEmpty())
            {
                data.InitEntity();

                res = _schedulingBus.AddData(data, OnOffDate, TeamTableId,ShiftsId,strRestDay);
            }
            //else
            //{
            //    res = _schedulingBus.UpdateData(data);
            //} 
            return Success();
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="ids">id数组,JSON数组</param>
        [HttpPost]
        public ActionResult<AjaxResult> DeleteData(string ids)
        {
            var res = _schedulingBus.DeleteData(ids.ToList<string>());

            return JsonContent(res.ToJson());
        }
        /// <summary>
        /// 删除全部数据
        /// </summary>
        /// <param name="ids">id数组,JSON数组</param>
        [HttpPost]
        public ActionResult<AjaxResult> DeleteDataAll()
        {
            
            var res = _schedulingBus.DeleteData();

            return JsonContent(res.ToJson());
        }

        #endregion
    }
}