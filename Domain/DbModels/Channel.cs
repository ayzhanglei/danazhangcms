using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DanaZhangCms.Domain.DbModels
{
    /// <summary>
    /// 系统频道表
    /// </summary>
    public class Channel
    {
        public Channel()
        { }
        #region Model
        private int _id;
        private string _name = "";
        private string _title = "";
        private bool _is_albums ;
        private int _is_attach ;
        private int _is_spec ;
        private int _sort_id = 99;
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
        /// 频道名称
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 频道标题
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 是否开启相册功能
        /// </summary>
        public bool IsAlbums
        {
            set { _is_albums = value; }
            get { return _is_albums; }
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
