using System;
using System.Collections.Generic;
using System.Text;
using PrivateTutoringApplication.Shared.DTO;

namespace PrivateTutoringApplication.IService
{
    public interface IYetkiService
    {
        YetkiDTO GetById(int id);
        YetkiDTO GetByGuid(Guid guid);
        int Create(YetkiDTO dto);
        void Delete(int id);
        int Update(YetkiDTO dto);
    }
}
