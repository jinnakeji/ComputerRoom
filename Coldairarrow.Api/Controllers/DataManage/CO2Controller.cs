using Coldairarrow.Business.DataManage;
using Coldairarrow.Entity.DataManage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Coldairarrow.Api.Controllers.DataManage
{
    [Route("/DataManage/[controller]/[action]")]
    public class CO2Controller : BaseApiController
    {
        #region DI

        public CO2Controller(ICO2Business cO2Bus)
        {
            _cO2Bus = cO2Bus;
        }

        ICO2Business _cO2Bus { get; }

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
        public ActionResult<AjaxResult<List<CO2>>> GetDataList(Pagination pagination, string condition, string keyword)
        {
            string uid = Operator.UserId;
            string pid = Operator.Property.DepartmentId;
            string dname = Operator.Property.DepartmentName;
            var dataList = _cO2Bus.GetDataList(pagination, false, uid, pid, keyword = null);

            return DataTable(dataList, pagination);
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<AjaxResult<CO2>> GetTheData(string id)
        {
            var theData = _cO2Bus.GetTheData(id);

            return Success(theData);
        }

        #endregion

        #region 提交

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="data">保存的数据</param>
        [HttpPost]
        public ActionResult<AjaxResult> SaveData(CO2 data)
        {
            AjaxResult res;
            if (data.Id.IsNullOrEmpty())
            {
                data.InitEntity();

                res = _cO2Bus.AddData(data);
            }
            else
            {
                res = _cO2Bus.UpdateData(data);
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
            var res = _cO2Bus.DeleteData(ids.ToList<string>());

            return JsonContent(res.ToJson());
        }

        #endregion
    }
}