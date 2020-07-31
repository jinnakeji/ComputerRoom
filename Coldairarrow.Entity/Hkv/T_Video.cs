using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Hkv
{
    /// <summary>
    /// T_Video
    /// </summary>
    [Table("T_Video")]
    public class T_Video
    {

        /// <summary>
        /// Id
        /// </summary>
        [Key, Column(Order = 1)]
        public Int32 Id { get; set; }

        /// <summary>
        /// 视频流地址
        /// </summary>
        public String RtspUrl { get; set; }

        /// <summary>
        /// 视频流名称
        /// </summary>
        public String Name { get; set; }

    }
}