using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DanaZhangCms.Domain.DbModels
{
    public class Banner
    {
        public Banner()
        {
            AddTime = System.DateTime.Now;
            UpdateTime = System.DateTime.Now;
        }
        #region Model

        /// <summary>
        /// 自增ID
        /// </summary>
        [Key]
        public int Id
        {
            set;
            get;
        }
        /// <summary>
        /// 标题
        /// </summary>
        [Required]
        public string Title
        {
            set;
            get;
        }
        /// <summary>
        /// 外部链接
        /// </summary>
        public string LinkUrl
        {
            set;
            get;
        }
        /// <summary>
        /// 图片地址
        /// </summary>
        [Required]
        public string ImgUrl
        {
            set;
            get;
        }
        /// <summary>
        /// 排序
        /// </summary>
        public int SortId
        {
            set;
            get;
        }


        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime AddTime
        {
            set;
            get;
        }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? UpdateTime
        {
            set;
            get;
        }


        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDel { get; set; }
        #endregion
    }
}
