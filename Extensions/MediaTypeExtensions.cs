﻿using DanaZhangCms.Services;
using System;
using Autofac;

namespace DanaZhangCms.Extensions
{
    public static class MediaTypeExtensions
    {
        public const string AzureStorage = "AzureStorage";
        public const string LocalStorage = "LocalStorage";
        public static void RegisterMediaType(this ContainerBuilder builder, string serviceName)
        {
            switch (serviceName)
            {
                //case AzureStorage:
                //    builder.RegisterType<AzureMediaService>().As<IMediaService>();
                //    break;
                case LocalStorage:
                    builder.RegisterType<LocalMediaService>().As<IMediaService>();
                    break;
                default:
                    throw new NotImplementedException($"Media type {serviceName} is not supported");
            }
        }
    }
}
