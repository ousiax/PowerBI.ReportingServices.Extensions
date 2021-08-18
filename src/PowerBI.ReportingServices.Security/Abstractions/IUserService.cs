using System.Threading.Tasks;

namespace PowerBI.ReportingServices.Security.Abstractions
{
    public interface IUserService
    {
        bool Exists(string userName);

        Task<int> CreateIfNotExistsAsync(string userName);
    }
}
