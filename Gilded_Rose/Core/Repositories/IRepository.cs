using Gilded_Rose.Core.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gilded_Rose.Core.Repositories
{
    public interface IRepository<T> where T : BaseModel
    {
        Task<T> GetById(int id);
        Task<List<T>> ListAll();
    }
}
