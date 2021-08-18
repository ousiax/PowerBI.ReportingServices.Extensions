namespace PowerBI.ReportingServices.Security.Abstractions
{
    public interface IOptions<T> where T : class
    {
        T Value { get; }
    }
}
