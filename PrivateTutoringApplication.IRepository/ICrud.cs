using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateTutoringApplication.IRepository
{
    public interface ICrud<T>
    {
        T GetById(int id);
        T GetByGuid(Guid guid);
        int Create(T dto);
        void Delete(int id);
        int Update(T dto);
    }
}
