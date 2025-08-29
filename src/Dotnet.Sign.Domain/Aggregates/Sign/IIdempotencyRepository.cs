namespace Dotnet.Sign.Domain.Aggregates.Sign
{
    public interface IIdempotencyRepository
    {
        Task<T?> GetAsync<T>(string key);
        Task SaveAsync<T>(string key, T response);
    }
}
