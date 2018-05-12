using AutoMapper;
using Bytes2you.Validation;
using Movies.Services.Contracts;

namespace Movies.Services.Mappings
{
    public class MappingService : IMappingService
    {
        static MappingService()
        {
            MappingProvider = new MappingService();
        }

        public static IMappingService MappingProvider { get; set; }

        public TMapTo Map<TMapTo>(object from)
        {
            Guard.WhenArgument(from, "from").IsNull().Throw();

            return Mapper.Map<TMapTo>(from);
        }
    }
}
