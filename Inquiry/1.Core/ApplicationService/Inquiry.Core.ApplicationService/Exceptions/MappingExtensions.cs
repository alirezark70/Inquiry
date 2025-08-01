using AutoMapper;
using AutoMapper.QueryableExtensions;
using Inquiry.Core.Domain.Models.Response.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inquiry.Core.ApplicationService.Exceptions
{
    public static class MappingExtensions
    {
        /// <summary>
        /// Maps object to destination type
        /// </summary>
        public static TDestination MapTo<TDestination>(this object source, IMapper mapper)
        {
            return mapper.Map<TDestination>(source);
        }

        /// <summary>
        /// Maps object to existing destination instance
        /// </summary>
        public static TDestination MapTo<TDestination>(this object source, TDestination destination, IMapper mapper)
        {
            return mapper.Map(source, destination);
        }

        /// <summary>
        /// Maps collection to destination type collection
        /// </summary>
        public static List<TDestination> MapToList<TDestination>(
            this IEnumerable<object> source,
            IMapper mapper)
        {
            return mapper.Map<List<TDestination>>(source);
        }

        /// <summary>
        /// Maps object with custom configuration
        /// </summary>
        public static TDestination MapTo<TSource, TDestination>(
            this TSource source,
            IMapper mapper,
            Action<IMappingOperationOptions<TSource, TDestination>> options)
        {
            return mapper.Map<TSource, TDestination>(source, options);
        }
    }
}
