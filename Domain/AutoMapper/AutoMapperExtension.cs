using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using System.Collections;

namespace DanaZhangCms.Domain.AutoMapper
{
    public static class AutoMapperExtension
    {


        /// <summary>
        /// 集合对集合
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static List<TResult> MapTo<TResult>(this IEnumerable self)
        {
            if (self == null) throw new ArgumentNullException();
            return (List<TResult>)Mapper.Map(self, self.GetType(), typeof(List<TResult>));
        }


        /// <summary>
        /// 对象对对象
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static TResult MapTo<TResult>(this object self)
        {
            if (self == null) throw new ArgumentNullException();
            return (TResult)Mapper.Map(self, self.GetType(), typeof(TResult));
        }

        ///// <summary>
        ///// 自动Map
        ///// <para>此方式极易覆盖预期的【已Map】的设置，调用前请确定映射从未被创建</para>
        ///// </summary>
        ///// <typeparam name="TResult"></typeparam>
        ///// <param name="self"></param>
        ///// <returns></returns>
        //[Obsolete("此方式极易覆盖预期的【已Map】的设置，调用前请确定映射从未被创建", false)]
        //public static TResult AutoMapTo<TResult>(this object self)
        //{
        //    if (self == null) throw new ArgumentNullException();
        //    Mapper.CreateMap(self.GetType(), typeof(TResult));
        //    return (TResult)Mapper.Map(self, self.GetType(), typeof(TResult));
        //}




        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="result"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static void MapFrom<TSource, TResult>(this TResult result, TSource source)
        {
            result = Mapper.Map<TSource, TResult>(source, result);
        }



    }
}
