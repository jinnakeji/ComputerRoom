using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Device
{
    /// <summary>
    /// 设备信息表
    /// </summary>
    [Table("DeviceInfo")]
    public class DeviceInfo
    {

        /// <summary>
        /// Id
        /// </summary>
        [Key, Column(Order = 1)]
        public String Id { get; set; }

        /// <summary>
        /// 创建人Id
        /// </summary>
        public String CreatorId { get; set; }

        /// <summary>
        /// 创建人姓名
        /// </summary>
        public String CreatorRealName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public String DeviceName
        {
            get;set;
            //get
            //{
            //    return DeviceName = name;
            //}
            //set { }

        }

        /// <summary>
        /// 设备节点号
        /// </summary>
        public String DeviceNode
        {
            get;set;
            //get
            //{
            //    return DeviceNode = number.ToString();
            //}
            //set { }
        }

        /// <summary>
        /// 采集设备Mac地址
        /// </summary>
        public String DeviceMacCode {get;set;}

        /// <summary>
        /// 1显示0不显示
        /// </summary>
        public Int32 IsState
        {
            //get
            //{
            //    return IsState = (isShow == true ? 1 : 0);
            //}
            //set { }
            get;set;
        }

        /// <summary>
        /// 设备数据模块ID
        /// </summary>
        public String DeviceDisplayModuleId { get; set; }

        /// <summary>
        /// 所属部门Id
        /// </summary>
        public String DepartmentId { get; set; }
        /// <summary>
        /// 传感器设备ID
        /// </summary>
        public string deviceid { get; set; } 
        /// <summary>
        /// 设备类型
        /// </summary>
        public String deviceType { get; set; }
        [NotMapped]
        public string name { get; set; }
        [NotMapped]
        public bool isShow { get; set; }
        [NotMapped]
        public int number { get; set; }
        /// <summary>
        /// 是否控制
        /// </summary>
        public int IsControl { get; set; }
        /// <summary>
        /// 是否开启
        /// </summary>
        public int IsOpen { get; set; }


    }
}