using System.Collections.Generic;
using System.Linq;

using Movies.Services.Mappings;

namespace Movies.Infrastructure.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T2> Map<T1, T2>(this IEnumerable<T1> collection)
        {
            return collection.Select(e => MappingService.MappingProvider.Map<T2>(e));
        }
    }
}
