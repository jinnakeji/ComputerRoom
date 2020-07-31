using Coldairarrow.Business.DataManage;
using Coldairarrow.Entity.DataManage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Coldairarrow.Api.Controllers.DataManage
{
    [Route("/DataManage/[controller]/[action]")]
    public class ThreeElecController : BaseApiController
    {
        #region DI

        public ThreeElecController(IThreeElecBusiness threeElecBus)
        {
            _threeElecBus = threeElecBus;
        }

        IThreeElecBusiness _threeElecBus { get; }

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
        public ActionResult<AjaxResult<List<ThreeElec>>> GetDataList(Pagination pagination, string condition, string keyword)
        {
            string uid = Operator.UserId;
            string pid = Operator.Property.DepartmentId;
            string dname = Operator.Property.DepartmentName;
            var dataList = _threeElecBus.GetDataList(pagination, false, uid, pid, keyword = null);

            return DataTable(dataList, pagination);
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<AjaxResult<ThreeElec>> GetTheData(string id)
        {
            var theData = _threeElecBus.GetTheData(id);

            return Success(theData);
        }

        #endregion

        #region 提交

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="data">保存的数据</param>
        [HttpPost]
        public ActionResult<AjaxResult> SaveData(ThreeElec data)
        {
            AjaxResult res;
            if (data.Id.IsNullOrEmpty())
            {
                data.InitEntity();

                res = _threeElecBus.AddData(data);
            }
            else
            {
                res = _threeElecBus.UpdateData(data);
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
            var res = _threeElecBus.DeleteData(ids.ToList<string>());

            return JsonContent(res.ToJson());
        }

        #endregion
    }
}