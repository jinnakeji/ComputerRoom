using Coldairarrow.Business.DataManage;
using Coldairarrow.Entity.DataManage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Coldairarrow.Api.Controllers.DataManage
{
    [Route("/DataManage/[controller]/[action]")]
    public class A5NodeOnOffController : BaseApiController
    {
        #region DI

        public A5NodeOnOffController(IA5NodeOnOffBusiness a5NodeOnOffBus)
        {
            _a5NodeOnOffBus = a5NodeOnOffBus;
        }

        IA5NodeOnOffBusiness _a5NodeOnOffBus { get; }

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
        public ActionResult<AjaxResult<List<A5NodeOnOff>>> GetDataList(Pagination pagination, string condition, string keyword)
        {
            string uid = Operator.UserId;
            string pid = Operator.Property.DepartmentId;
            string dname = Operator.Property.DepartmentName;
            var dataList = _a5NodeOnOffBus.GetDataList(pagination, false, uid, pid, keyword = null); 

            return DataTable(dataList, pagination);
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<AjaxResult<A5NodeOnOff>> GetTheData(string id)
        {
            var theData = _a5NodeOnOffBus.GetTheData(id);

            return Success(theData);
        }

        #endregion

        #region 提交

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="data">保存的数据</param>
        [HttpPost]
        public ActionResult<AjaxResult> SaveData(A5NodeOnOff data)
        {
            AjaxResult res;
            if (data.Id.IsNullOrEmpty())
            {
                data.InitEntity();

                res = _a5NodeOnOffBus.AddData(data);
            }
            else
            {
                res = _a5NodeOnOffBus.UpdateData(data);
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
            var res = _a5NodeOnOffBus.DeleteData(ids.ToList<string>());

            return JsonContent(res.ToJson());
        }

        #endregion
    }
}