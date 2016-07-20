using AutoMapper;
using DanaZhangCms.Domain.DbModels;
using DanaZhangCms.Domain.ViewModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DanaZhangCms.Middleware.AutoMapper
{
    public class AutoMapperMiddleware
    {
        private readonly RequestDelegate _next;
        public AutoMapperMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            Mapper.Initialize(
               cfg =>
               {
                   cfg.CreateMap<ChannelViewModel, Channel>();
               }
            );
            await _next.Invoke(context);
        }
    }

}
