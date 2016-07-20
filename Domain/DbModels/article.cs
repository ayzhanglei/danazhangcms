using DanaZhangCms.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DanaZhangCms.Domain.DbModels
{
    /// <summary>
    /// 文章主实体类
    /// </summary>

    public partial class Article
    {
        public Article()
        {
            AddTime = System.DateTime.Now;
            UpdateTime = System.DateTime.Now;
        }

        private string _zhaiyao = "";

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
        /// 频道ID
        /// </summary>
        [NotMapped]
        public int ChannelId
        {
            set;
            get;
        }
        /// <summary>
        /// 类别ID
        /// </summary>
        [Required]
        public int CategoryId
        {
            set;
            get;
        }

        public ArticleCategory Category
        {
            get;
            set;
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
        public string ZhaiYao
        {
            set { _zhaiyao = value; }
            get
            {
                if (string.IsNullOrEmpty(_zhaiyao))
                {
                    var html = StringHelper.NoHTML(Content);
                    _zhaiyao = html.Length > 20 ? html.Substring(0, 20) + "..." : html;
                }

                return _zhaiyao;
            }
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

        #endregion Model

    }
}