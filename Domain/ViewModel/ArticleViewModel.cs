using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DanaZhangCms.Domain.ViewModel
{
    public class ArticleViewModel
    {
        /// <summary>
        /// 频道ID
        /// </summary>
        //[Required]
        //public int ChannelId
        //{
        //    set;
        //    get;
        //}
        /// <summary>
        /// 类别ID
        /// </summary>
        [Required]
        public int CategoryId
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
        public string ImgUrl
        {
            set;
            get;
        }
        /// <summary>
        /// SEO标题
        /// </summary>
        public string SeoTitle
        {
            set;
            get;
        }
        /// <summary>
        /// SEO关健字
        /// </summary>
        public string SeoKeywords
        {
            set;
            get;
        }
        /// <summary>
        /// SEO描述
        /// </summary>
        public string SeoDescription
        {
            set;
            get;
        }
        /// <summary>
        /// 内容摘要
        /// </summary>
        //[Required]
        public string ZhaiYao
        {
            set;
            get;
        }
        /// <summary>
        /// 详细内容
        /// </summary>
        [Required]
        public string Content
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
        /// 浏览次数
        /// </summary>
        public int Click
        {
            set;
            get;
        }
        /// <summary>
        /// 状态0正常1未审核2锁定
        /// </summary>
        public int Status
        {
            set;
            get;
        }

        /// <summary>
        /// 是否置顶
        /// </summary>
        public int IsTop
        {
            set;
            get;
        }
        /// <summary>
        /// 是否推荐
        /// </summary>
        public int IsRed
        {
            set;
            get;
        }
        /// <summary>
        /// 是否热门
        /// </summary>
        public int IsHot
        {
            set;
            get;
        }

    }
}
