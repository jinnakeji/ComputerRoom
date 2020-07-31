using Coldairarrow.Business.Base_Manage;
using Coldairarrow.Business.MeterReaDing;
using Coldairarrow.Entity.MeterReaDing;
using Coldairarrow.Util;
using Coldairarrow.Util.Helper;
using libzkfpcsharp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Coldairarrow.Api.Controllers.MeterReaDing
{
    [Route("/MeterReaDing/[controller]/[action]")]
    public class MeterReaDingOnDutyController : BaseApiController
    {
        #region DI
        static IntPtr dbHandler;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public MeterReaDingOnDutyController(IMeterReaDingOnDutyBusiness meterReaDingOnDutyBus, IBase_UserBusiness userBus, IChangeShiftsBusiness changeShiftsBus, IWebHostEnvironment webHostEnvironment, IBase_DepartmentBusiness departmentBus, IMeterReaDingTimeSetUpBusiness meterReaDingTimeSetUpBus)
        {
            _meterReaDingOnDutyBus = meterReaDingOnDutyBus;
            _userBus = userBus;
            _changeShiftsBus = changeShiftsBus;
            _webHostEnvironment = webHostEnvironment;
            _departmentBus = departmentBus;
            _meterReaDingTimeSetUpBus = meterReaDingTimeSetUpBus;
            zkfp2.Init();
           dbHandler = zkfp2.DBInit();
        }

        IMeterReaDingOnDutyBusiness _meterReaDingOnDutyBus { get; }
        IBase_UserBusiness _userBus { get; }
        IChangeShiftsBusiness _changeShiftsBus { get; }
        IBase_DepartmentBusiness _departmentBus { get; }
        IMeterReaDingTimeSetUpBusiness _meterReaDingTimeSetUpBus { get; }
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
        public ActionResult<AjaxResult<List<MeterReaDingOnDuty>>> GetDataList(Pagination pagination, string condition, string keyword)
        {
            var dataList = _meterReaDingOnDutyBus.GetDataList(pagination, condition, keyword);

            return DataTable(dataList, pagination);
        }
        /// <summary>
        /// 获取报告数据
        /// </summary>
        /// <param name="deptId">部门Id</param> 
        /// <returns></returns>
        [HttpPost]
        public ActionResult<AjaxResult> GetReportTable(string deptId)
        {
            // List<Base_DepartmentTreeDTO> dataList = _departmentBus.GetTreeDataList(deptId);
            var quuser = _userBus.GetTheData(Operator.UserId) ?? new Base_UserDTO();
            //获取通班组人员
            string temlist = null;
            if (quuser.TeamTableId != null)
            {

                List<Base_UserDTO> team = _userBus.GetUserByTeamIdDataList(quuser.TeamTableId, false);
                //获取班组内人员

                foreach (var vtaem in team)
                {
                    temlist += vtaem.RealName + ",";
                }
            }
            else
            {
                temlist = "未分配班组";
            }

            List<MeterReaDingOnDutyDTO> qdreport = _meterReaDingOnDutyBus.GetDataDtoList(deptId);
            return Success(qdreport);
        }

        [HttpPost]
        public ActionResult<AjaxResult> PostReport(string id, string deptId, string remarks)
        {
            var Base_Department = _departmentBus.GetTheData(deptId);
            var quuser = _userBus.GetTheData(Operator.UserId) ?? new Base_UserDTO();
            //获取通班组人员
            string temlist = null;
            if (quuser.TeamTableId != null)
            {

                List<Base_UserDTO> team = _userBus.GetUserByTeamIdDataList(quuser.TeamTableId, false);
                //获取班组内人员

                foreach (var vtaem in team)
                {
                    temlist += vtaem.RealName + ",";
                }
            }
            else
            {
                temlist = "未分配班组";
            }

            List<MeterReaDingOnDutyDTO> qdreport = _meterReaDingOnDutyBus.GetDataDtoList(deptId);

            string exclName = "值班报表_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
            string path = _webHostEnvironment.ContentRootPath + "/Templates/";
            new ExcelHelper().ExcelDataExport(quuser.RealName, Base_Department.Name, qdreport, temlist.TrimEnd(','), exclName, path);

            //    //添加交接班日志
            ChangeShifts changemode = new ChangeShifts();
            changemode.CreateTime = DateTime.Now;
            changemode.UserId = id;
            changemode.ChangeUserId = Operator.UserId;
            changemode.remarks = remarks;
            changemode.filePath = "/Templates/" + exclName;
            _changeShiftsBus.AddData(changemode);


            return Success();
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<AjaxResult<MeterReaDingOnDuty>> GetTheData(string id)
        {
            var theData = _meterReaDingOnDutyBus.GetTheData(id);

            return Success(theData);
        }

        #endregion

        #region 提交

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="jsondata">保存的数据</param>
        /// <param name="mid">模块id</param> 
        /// <param name="deptId">部门Id</param> 
        /// <param name="people">1人抄表 0 机器抄表</param>
        /// <param name="timesetupid">抄表时间ID</param>
        /// <param name="confirmuserid">指纹确认Id</param>
        [HttpPost]
        public ActionResult<AjaxResult> SaveData(string jsondata, string mid, string deptId, string people, string timesetupid, string confirmuserid)
        {
            CacheHelper.RedisCache.SetCache("state", 0); //抄表 未生成抄表记录
            List<MeterReaDingOnDuty> meterlist = new List<MeterReaDingOnDuty>();
            // int people = 0; //1人抄表 0 机器抄表

            // List<MeterReaDingOnDuty> datalist = new List<MeterReaDingOnDuty>();
            // timesetupid = _meterReaDingTimeSetUpBus.GetListByUserId(Operator.Property.Id).Id;
            AjaxResult res = new AjaxResult() { Success = false, Msg = "抄表数据失败" };
            if (!jsondata.IsNullOrEmpty() && !mid.IsNullOrEmpty())
            {
                #region MyRegion

                //var qu = JsonHelper.DeserializeJSON<DailyReport>(jsondata);

                //foreach (var item in qu.deviceList)
                //{
                //    if (item.propList!=null)
                //    {
                //        foreach (var proitem in item.propList)
                //        {
                //            MeterReaDingOnDuty data = new MeterReaDingOnDuty();
                //            data.CreatorId = Operator.Property.Id;
                //            data.CreatorRealName = Operator.Property.RealName;
                //            data.UserId = people == "1" ? Operator.UserId : people.ToString();
                //            data.CreateTime = DateTime.Now;
                //            data.DataContent = jsondata;
                //            data.DeviceDisplayModuleID = mid == null ? qu.moduleId : mid;
                //            data.deptId = deptId;
                //            data.MeterReaDingTimeSetUpId = timesetupid;
                //            data.moduleName = qu.moduleName;
                //            data.deviceName = item.deviceName;
                //            data.propName = proitem.displayName;
                //            data.propValue = proitem.value;
                //            datalist.Add(data);
                //        }

                //    }
                //}

                #endregion
                //数据先存 reds 
                CacheHelper.RedisCache.SetCache("MeterReaDingOnDuty." + mid, jsondata);
                res = new AjaxResult() { Success = true, Msg = "抄表完成" };
            }

            if (!confirmuserid.IsNullOrEmpty())
            {
                var uuser = _userBus.GetTheData(Operator.UserId) ?? new Base_UserDTO();
                var quuser = _userBus.GetTheData(confirmuserid) ?? new Base_UserDTO();
                if (uuser.TeamTableId != quuser.TeamTableId)
                {
                    res = new AjaxResult() { Success = false, Msg = "请本组人员确认!" };
                }
                else if (uuser.DepartmentId != quuser.DepartmentId)
                {
                    res = new AjaxResult() { Success = false, Msg = "请本部门人确认!" };
                }
                else
                {
                    //获取指定的key
                    var listkey = CacheHelper.RedisCache.GetCacheKeyList("MeterReaDingOnDuty.*").ToString().TrimEnd(',').Split(',');
                    DateTime CreateTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                    //获取value
                    foreach (var item in listkey)
                    {
                        List<MeterReaDingOnDuty> datainfo = new List<MeterReaDingOnDuty>();
                        #region MyRegion
                        // datainfo = (List<MeterReaDingOnDuty>)CacheHelper.RedisCache.GetCache(item); 
                        //foreach (var iteminfo in datainfo)
                        //{
                        //    iteminfo.ConfirmUserId = confirmuserid == "" ? "0" : confirmuserid;
                        //    //meterlist.Add(datainfo);
                        //    if (iteminfo.Id.IsNullOrEmpty())
                        //    {
                        //        iteminfo.InitEntity(); _meterReaDingOnDutyBus.AddData(iteminfo);

                        //    }
                        //} 

                        #endregion
                        var data = CacheHelper.RedisCache.GetCache(item).ToString();
                        var qu = JsonHelper.DeserializeJSON<DailyReport>(data);
                        foreach (var jitem in qu.deviceList)
                        {
                            if (jitem.propList != null)
                            {
                                foreach (var proitem in jitem.propList)
                                {
                                    MeterReaDingOnDuty jdata = new MeterReaDingOnDuty();
                                    if (proitem.propName != "updateTime")
                                    {
                                        jdata.CreatorId = Operator.Property.Id;
                                        jdata.CreatorRealName = Operator.Property.RealName;
                                        jdata.UserId = "1";
                                        jdata.CreateTime = CreateTime;
                                        jdata.DataContent = data;
                                        jdata.DeviceDisplayModuleID = mid == null ? qu.moduleId : mid;
                                        jdata.deptId = deptId;
                                        jdata.MeterReaDingTimeSetUpId = timesetupid;
                                        jdata.moduleName = qu.moduleName;
                                        jdata.deviceName = jitem.deviceName;
                                        jdata.propName = proitem.displayName;
                                        if (jitem.deviceTypeName == "aaNodeOnOff") //输出：0是关 -1是开  
                                        {
                                            jdata.propValue = proitem.value == "0" ? "关" : proitem.value == "-1" ? "开" : proitem.value;
                                        }
                                        else if (jitem.deviceTypeName == "a5NodeOnOff") //输入：0报警  -1正常 
                                        {
                                            jdata.propValue = proitem.value == "0" ? "报警" : proitem.value == "-1" ? "正常" : proitem.value;
                                        }
                                        else if (jitem.deviceTypeName == "airOnOff") //240是合闸，15是分闸
                                        {
                                            jdata.propValue = proitem.value == "240" ? "合闸" : proitem.value == "15" ? "分闸" : proitem.value;
                                        }
                                        else
                                        {
                                            jdata.propValue = proitem.value;
                                        }
                                        jdata.propValue = proitem.value;
                                        jdata.ConfirmUserId = confirmuserid == "" ? "0" : confirmuserid;
                                        //jdata.InitEntity();
                                        _meterReaDingOnDutyBus.AddData(jdata);

                                    }
                                }

                            }
                        }
                        CacheHelper.RedisCache.RemoveCache(item);
                        CacheHelper.RedisCache.SetCache("state", 1); //已抄表 
                    }
                    res = new AjaxResult() { Success = true, Msg = "抄表数据已确认！" }; 
                }
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
            var res = _meterReaDingOnDutyBus.DeleteData(ids.ToList<string>());

            return JsonContent(res.ToJson());
        }

        /// <summary>
        /// 通过指纹模板 获取用户ID
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<AjaxResult> GetUserIdByFingerTemplate(string confirmuserid)
        {
            string userid = null;
            /*****
                   * 
                   * 指纹模板  获取用户信息
                   * 
                   * *****/
            var userDtoList = _userBus.GetDataList(new Pagination()
            {
                PageIndex = 1,
                PageRows = int.MaxValue,
                SortField = "Id",
                SortType = "ASC"
            }, true).Where(x => x.FingerTemplate != null);

            foreach (var userDto in userDtoList)
            {
                var ret = zkfp2.DBMatch(dbHandler, zkfp2.Base64ToBlob(userDto.FingerTemplate), zkfp2.Base64ToBlob(confirmuserid));
                if (ret > 0)
                {
                    userid = userDto.Id;
                }
            }
            return Success<object>(new { UserId = userid });
        }
        /// <summary>
        /// 通过用户名获取 用户Id
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<AjaxResult> GetUserIdByUserNameByPwd(string userName, string password)
        {
            var userId = _userBus.GetUserListByNameByPwd(userName, password).Id;
            return Success(new { UserId = userId });
        }

        #endregion
    }
}