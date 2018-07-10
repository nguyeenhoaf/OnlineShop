namespace TDShop.Data.Infrastructure
{
    public class UnitofWork : IUnitofWork
    {
        private readonly IDbFactory dbFactory;
        private TDShopDbContext dbContext;

        public UnitofWork(IDbFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }

        public TDShopDbContext DbContext
        {
            get { return dbContext ?? (dbContext = dbFactory.Init()); }
        }

        public void Commit()
        {
            DbContext.SaveChanges();
        }
    }
}