using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DanaZhangCms.Domain.ViewModel
{
    public class ArticleCategoryViewModel
    {
        private int _id;
        private int _channel_id;
        private string _title;
        private int _parent_id = 0;
        private string _class_list = "";
        private int _class_layer = 0;
        private int _sort_id ;
        private string _link_url = "";
        private string _img_url = "";
        private string _content;
        private string _seo_title = "";
        private string _seo_keywords = "";
        private string _seo_description = "";
        /// <summary>
        /// 频道ID
        /// </summary>
        public int ChannelId
        {
            set { _channel_id = value; }
            get { return _channel_id; }
        }
    

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
    }
}
