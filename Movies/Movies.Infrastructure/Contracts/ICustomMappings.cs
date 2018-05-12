using AutoMapper;

namespace Movies.Infrastructure.Contracts
{
    public interface ICustomMappings
    {
        void CreateMappings(IMapperConfigurationExpression configuration);
    }
}
