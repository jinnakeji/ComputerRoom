using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Hkv
{
    /// <summary>
    /// 视频表
    /// </summary>
    [Table("HkVideo")]
    public class HkVideo
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
        /// 部门Id
        /// </summary>
        public String DepartmentId { get; set; }

        /// <summary>
        /// ip地址
        /// </summary>
        public String Ip { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public Int32? Prot { get; set; }

        /// <summary>
        /// 视频账号
        /// </summary>
        public String ViodeName { get; set; }

        /// <summary>
        /// 视频账号密码
        /// </summary>
        public String ViodePwd { get; set; }

    }
}