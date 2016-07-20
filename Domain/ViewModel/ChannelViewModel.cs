using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DanaZhangCms.Domain.ViewModel
{
    public class ChannelViewModel
    {
        #region Model
        private int _id;
        private string _name ;
        private string _title ;
        private bool _is_albums;
        private int _is_attach;
        private int _is_spec;
        private int _sort_id ;
     
       

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
        


        #endregion Model
    }
}
