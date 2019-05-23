using Gilded_Rose.Core.Shared;
using System.Collections.Generic;

namespace Gilded_Rose.Core.Interfaces
{
    public interface IRepository<T> where T : BaseModel
    {
        T GetById(int id);
        List<T> ListAll();
    }
}
