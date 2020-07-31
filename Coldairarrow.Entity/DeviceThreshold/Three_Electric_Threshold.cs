using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.DeviceThreshold
{
    /// <summary>
    /// 三相电阈值设置
    /// </summary>
    [Table("Three_Electric_Threshold")]
    public class Three_Electric_Threshold
    {

        /// <summary>
        /// 主键
        /// </summary>
        public String Id { get; set; }

        /// <summary>
        /// 设备节点号
        /// </summary>
        public String DeviceNode { get; set; }

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
        /// A相电压最高电压
        /// </summary>
        public String A_Voltage_Highest { get; set; }

        /// <summary>
        /// A相电压最低电压
        /// </summary>
        public String A_Voltage_Lowest { get; set; }

        /// <summary>
        /// B相电压最高电压
        /// </summary>
        public String B_Voltage_Highest { get; set; }

        /// <summary>
        /// B相电压最低电压
        /// </summary>
        public String B_Voltage_Lowest { get; set; }

        /// <summary>
        /// C相电压最高电压
        /// </summary>
        public String C_Voltage_Highest { get; set; }

        /// <summary>
        /// C相电压最低电压
        /// </summary>
        public String C_Voltage_Lowest { get; set; }

        /// <summary>
        /// A相电流最高电流
        /// </summary>
        public String A_Current_Highest { get; set; }

        /// <summary>
        /// A相电流最低电流
        /// </summary>
        public String A_Current_Lowest { get; set; }

        /// <summary>
        /// B相电流最高电流
        /// </summary>
        public String B_Current_Highest { get; set; }

        /// <summary>
        /// B相电流最低电流
        /// </summary>
        public String B_Current_Lowest { get; set; }

        /// <summary>
        /// C相电流最高电流
        /// </summary>
        public String C_Current_Highest { get; set; }

        /// <summary>
        /// C相电流最低电流
        /// </summary>
        public String C_Current_Lowest { get; set; }

        /// <summary>
        /// 零线电流最高电流
        /// </summary>
        public String ZeroCurrent_Highest { get; set; }

        /// <summary>
        /// 零线电流最低电流
        /// </summary>
        public String ZeroCurrent_Lowest { get; set; }

        /// <summary>
        /// 总有功率最高
        /// </summary>
        public String TotalPower_Highest { get; set; }

        /// <summary>
        /// 总有功率最低
        /// </summary>
        public String TotalPower_Lowest { get; set; }

        /// <summary>
        /// 阈值设置时间
        /// </summary>
        public DateTime? DeviceUpDateTime { get; set; }

    }
}