﻿using Newtonsoft.Json;
using SiliconSpace.Domain.Commons;
using SiliconSpace.Service.Configurations;
using SiliconSpace.Service.Exceptions;
using SiliconSpace.Service.Helpers;

namespace SiliconSpace.Service.Extensions
{
    public static class CollectionExtension
    {
        public static IQueryable<TEntity> ToPagedList<TEntity>(this IQueryable<TEntity> source, PaginationParams @params)
                 where TEntity : Auditable
        {
            var metaData = new PaginationMetaData(source.Count(), @params);
            var json = JsonConvert.SerializeObject(metaData);

            // Set X-Pagination header in the response
            if (HttpContextHelper.ResponseHeaders != null)
            {
                HttpContextHelper.ResponseHeaders.Remove("X-Pagination");
                HttpContextHelper.ResponseHeaders.Add("X-Pagination", json);
            }

            return @params.PageIndex > 0 && @params.PageSize > 0 ?
                source
                    .OrderBy(s => s.Id)
                    .Skip((@params.PageIndex - 1) * @params.PageSize).Take(@params.PageSize)
                : throw new SiliconSpaceException(400, "Please, enter valid numbers");
        }

        public static IEnumerable<TEntity> ToPagedList<TEntity>(this IEnumerable<TEntity> source, PaginationParams @params)
        {
            if (@params.PageIndex < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(@params.PageIndex), "The page index must be greater than or equal to 1.");
            }

            if (@params.PageSize < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(@params.PageSize), "The page size must be greater than or equal to 1.");
            }

            return source.Take((@params.PageSize * (@params.PageIndex - 1))..(@params.PageSize * (@params.PageIndex - 1) + @params.PageSize));
        }

    }

}
