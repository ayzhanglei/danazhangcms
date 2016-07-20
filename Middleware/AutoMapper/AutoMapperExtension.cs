using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using System.Collections;
using Microsoft.AspNetCore.Builder;

namespace DanaZhangCms.Middleware.AutoMapper
{
    public static class AutoMapperExtension
    {
        public static IApplicationBuilder UseAutoMapper(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AutoMapperMiddleware>();
        }
    }
}
