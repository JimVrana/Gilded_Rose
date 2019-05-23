using Gilded_Rose.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gilded_Rose.Core.Interfaces
{
    public interface IUserService    {
        ApiUser Authenticate(string userName, string password);
    }
}
