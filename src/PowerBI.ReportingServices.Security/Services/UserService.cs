using System.Linq;
using System.Threading.Tasks;

using PowerBI.ReportingServices.Security.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace PowerBI.ReportingServices.Security.Services
{
    sealed class UserService : IUserService
    {
        public Task<int> CreateIfNotExistsAsync(string userName)
        {
            var ctx = ServiceUtility.Provider.GetService<UserAccountContext>();
            if (!ctx.Users.Any(u => string.Equals(u.UserName, userName)))
            {
                ctx.Users.Add(new Entities.User() { UserName = userName });
                return ctx.SaveChangesAsync();
            }

            return Task.FromResult(0);
        }

        public bool Exists(string userName)
        {
            return ServiceUtility.Provider.GetService<UserAccountContext>()
                .Users
                .Any(u => string.Equals(u.UserName, userName));
        }
    }
}
