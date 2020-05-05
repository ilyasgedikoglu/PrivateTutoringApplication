using System;
using System.Collections.Generic;
using System.Text;
using PrivateTutoringApplication.Shared.DTO;

namespace PrivateTutoringApplication.IRepository
{
    public interface IGirisRepository : ICrud<GirisDTO>
    {
        bool TokenKontrol(string token);
        GirisDTO KullanicininSonTokenBilgisi(int kullaniciId);
        //GirisDTO GetByTokenAsync(Guid token, int kullaniciId);
    }
}
