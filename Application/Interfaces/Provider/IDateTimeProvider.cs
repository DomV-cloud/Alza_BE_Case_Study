namespace Application.Interfaces.Provider
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}
