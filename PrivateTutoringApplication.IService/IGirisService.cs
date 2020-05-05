using System;
using System.Collections.Generic;
using System.Text;
using PrivateTutoringApplication.Shared.DTO;

namespace PrivateTutoringApplication.IService
{
    public interface IGirisService
    {
        GirisDTO GetById(int id);
        GirisDTO GetByGuid(Guid guid);
        int Create(GirisDTO dto);
        void Delete(int id);
        int Update(GirisDTO dto);
        bool TokenKontrol(string token);
        GirisDTO KullanicininSonTokenBilgisi(int kullaniciId);
        //GirisDTO GetByTokenAsync(Guid token, int kullaniciId);
    }
}
