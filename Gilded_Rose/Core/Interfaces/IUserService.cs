using Gilded_Rose.Core.Models;

namespace Gilded_Rose.Core.Interfaces
{
    public interface IUserService    {
        ApiUser Authenticate(string userName, string password);
    }
}
