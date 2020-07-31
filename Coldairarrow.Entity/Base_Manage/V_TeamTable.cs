﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Base_Manage
{
    public class V_TeamTable
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
        /// 用户ID
        /// </summary>
        public String UserId { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public String UserName { get; set; }

        /// <summary>
        /// 班组名称
        /// </summary>
        public String TeamTableName { get; set; }
    }
}
