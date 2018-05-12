namespace Movies.Core.Contracts
{
    public interface IUnitOfWork
    {
        void Complete();
    }
}
