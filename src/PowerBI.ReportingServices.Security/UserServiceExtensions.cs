using PowerBI.ReportingServices.Security.Abstractions;

namespace PowerBI.ReportingServices.Security
{
    static class UserServiceExtensions
    {
        public static int CreateIfNotExists(this IUserService userService, string userName)
        {
            return userService.CreateIfNotExistsAsync(userName).Result;
        }
    }
}