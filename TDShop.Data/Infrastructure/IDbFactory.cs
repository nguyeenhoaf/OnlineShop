using System;

namespace TDShop.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        TDShopDbContext Init();
    }
}