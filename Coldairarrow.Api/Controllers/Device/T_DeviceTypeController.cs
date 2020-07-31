using Coldairarrow.Business.Device;
using Coldairarrow.Entity.Device;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Coldairarrow.Api.Controllers.Device
{
    [Route("/Device/[controller]/[action]")]
    public class T_DeviceTypeController : BaseApiController
    {
        #region DI

        public T_DeviceTypeController(IT_DeviceTypeBusiness t_DeviceTypeBus)
        {
            _t_DeviceTypeBus = t_DeviceTypeBus;
        }

        IT_DeviceTypeBusiness _t_DeviceTypeBus { get; }

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
        public ActionResult<AjaxResult<List<T_DeviceType>>> GetDataList(Pagination pagination, string condition, string keyword)
        {
            var dataList = _t_DeviceTypeBus.GetDataList(pagination, condition, keyword);

            return DataTable(dataList, pagination);
        }
        [HttpPost]
        public ActionResult<AjaxResult<T_DeviceType>> GetDataViewListAll()
        {
            var result = _t_DeviceTypeBus.GetDataList(new Pagination() { PageIndex = 1, PageRows = int.MaxValue }, null, null);
            return Success(result);
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<AjaxResult<T_DeviceType>> GetTheData(int id)
        {
            var theData = _t_DeviceTypeBus.GetTheData(id);

            return Success(theData);
        }

        #endregion

        #region 提交

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="data">保存的数据</param>
        [HttpPost]
        public ActionResult<AjaxResult> SaveData(T_DeviceType data)
        {
            AjaxResult res;
            if (data.Id.IsNullOrEmpty())
            {
                data.InitEntity();

                res = _t_DeviceTypeBus.AddData(data);
            }
            else
            {
                res = _t_DeviceTypeBus.UpdateData(data);
            }

            return JsonContent(res.ToJson());
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="ids">id数组,JSON数组</param>
        [HttpPost]
        public ActionResult<AjaxResult> DeleteData(string ids)
        {
            var res = _t_DeviceTypeBus.DeleteData(ids.ToList<string>());

            return JsonContent(res.ToJson());
        }

        #endregion
    }
}