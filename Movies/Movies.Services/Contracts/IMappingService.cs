namespace Movies.Services.Contracts
{
    public interface IMappingService
    {
        TMapTo Map<TMapTo>(object from);
    }
}
