using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DanaZhangCms.Domain.ViewModel
{
    public class BannerViewModel
    {
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
    }
}
