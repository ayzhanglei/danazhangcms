using DanaZhangCms.Domain.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DanaZhangCms.Domain.ViewModel
{
    public class HomeIndexViewModel
    {
        public List<Banner> Banners { get; set; }

        public List<Article> Anli { get; set; }

        public List<Article> News { get; set; }
    }
}
