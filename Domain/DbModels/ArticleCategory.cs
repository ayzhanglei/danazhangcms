using DanaZhangCms.Domain.DbModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace DanaZhangCms.Domain.DbModels
{
    /// <summary>
    /// 文章分类表
    /// </summary>
    public partial class ArticleCategory
    {
        public ArticleCategory()
        {
            AddTime = System.DateTime.Now;
            UpdateTime = System.DateTime.Now;
        }
        #region Model
        private int _id;
        private int _channel_id;
        private string _title;
        private int _parent_id = 0;
        private string _class_list = "";
        private int _class_layer = 0;
        private int _sort_id = 99;
        private string _link_url = "";
        private string _img_url = "";
        private string _content;
        private string _seo_title = "";
        private string _seo_keywords = "";
        private string _seo_description = "";
        /// <summary>
        /// 自增ID
        /// </summary>
        [Key]
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 频道ID
        /// </summary>
        public int ChannelId
        {
            set { _channel_id = value; }
            get { return _channel_id; }
        }
        /// <summary>
        /// 频道
        /// </summary>
        public virtual Channel Channel { get; set; }

        /// <summary>
        /// 类别标题
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }

        /// <summary>
        /// 父类别ID
        /// </summary>
        public int ParentId
        {
            set { _parent_id = value; }
            get { return _parent_id; }
        }
       

        /// <summary>
        /// 类别ID列表(逗号分隔开)
        /// </summary>
        public string ClassList
        {
            set { _class_list = value; }
            get { return _class_list; }
        }
        /// <summary>
        /// 类别深度
        /// </summary>
        public int ClassLayer
        {
            set { _class_layer = value; }
            get { return _class_layer; }
        }
        /// <summary>
        /// 排序数字
        /// </summary>
        public int SortId
        {
            set { _sort_id = value; }
            get { return _sort_id; }
        }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImgUrl
        {
            set { _img_url = value; }
            get { return _img_url; }
        }
        /// <summary>
        /// 备注说明
        /// </summary>
        public string Content
        {
            set { _content = value; }
            get { return _content; }
        }
        /// <summary>
        /// SEO标题
        /// </summary>
        public string SeoTitle
        {
            set { _seo_title = value; }
            get { return _seo_title; }
        }
        /// <summary>
        /// SEO关健字
        /// </summary>
        public string SeoKeywords
        {
            set { _seo_keywords = value; }
            get { return _seo_keywords; }
        }
        /// <summary>
        /// SEO描述
        /// </summary>
        public string SeoDescription
        {
            set { _seo_description = value; }
            get { return _seo_description; }
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