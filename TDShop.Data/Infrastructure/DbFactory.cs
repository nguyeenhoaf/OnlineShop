using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDShop.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private TDShopDbContext dbContext;
        public TDShopDbContext Init()
        {
            return dbContext ?? (dbContext = new TDShopDbContext());
        }
        protected override void DisposeCore()
        {
           if(dbContext!=null)
            {
                dbContext.Dispose();
            }
        }
    }
}
