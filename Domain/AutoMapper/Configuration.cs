using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DanaZhangCms.Domain.ViewModel;
using DanaZhangCms.Domain.DbModels;
using DanaZhangCms.DbModels;

namespace DanaZhangCms.Domain.AutoMapper
{
    public class Configuration
    {






        public static void RegisterConfigure()
        {


            #region 
            Mapper.Initialize(
               cfg =>
               {
                   cfg.CreateMap<ChannelViewModel, Channel>();
                   cfg.CreateMap<Channel, ChannelViewModel>();

                   cfg.CreateMap<ArticleCategoryViewModel, ArticleCategory>();
                   cfg.CreateMap<ArticleCategory, ArticleCategoryViewModel>();

                   cfg.CreateMap<ArticleViewModel, Article>();
                   cfg.CreateMap<Article, ArticleViewModel>();

                   cfg.CreateMap<Banner, BannerViewModel>();
                   cfg.CreateMap<BannerViewModel, Banner>();
                   

               }
            );



            #endregion

        }


    }
}
