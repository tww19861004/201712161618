using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tww.MinPrice.Services
{
    interface IServerDataRepository
    {
        Object Get(int id);
        IEnumerable<Object> GetAll();
        void Add(Object serverData);
        void Delete(int id);
        bool Update(Object serverData);
    }
}
