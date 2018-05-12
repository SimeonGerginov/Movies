namespace Movies.Infrastructure.Providers.Contracts
{
    public interface IServiceLocator
    {
        T ProvideInstance<T>();
    }
}
