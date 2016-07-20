using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DanaZhangCms.Domain.ViewModel
{
 
    public class AddCategoryViewModel
    {
        public string text { get; set; }
        public string href { get; set; }
        public List<string> tags { get; set; }
        public List<AddCategoryViewModel> nodes { get; set; }
    }

    //public class AddCategoryViewModelNode
    //{
    //    public string text { get; set; }
    //    public string href { get; set; }
    //    public List<string> tags { get; set; }
    //    public List<AddCategoryViewModelNode> nodes { get; set; }
    //}
}
