using Gilded_Rose.Core.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gilded_Rose.Core.Interfaces
{
    public interface IRepository<T> where T : BaseModel
    {
        T GetById(int id);
        List<T> ListAll();
    }
}
