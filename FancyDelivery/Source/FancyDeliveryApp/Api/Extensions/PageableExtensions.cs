using System;
using System.Collections.Generic;
using AutoMapper;
using Infrastructure.Repositories;

namespace FancyDeliveryApp.Api.Extensions
{
    public static class PageableExtensions
    {
        public static Pageable<TTarget> Map<TSource, TTarget>(this Pageable<TSource> pageable) where TTarget : class where TSource : class
        {
            return new Pageable<TTarget>(
                pageable.Total,
                pageable.PageNumber,
                pageable.PageSize,
                Mapper.Map<IList<TSource>, IList<TTarget>>(pageable.Items));
        }
    }
}
