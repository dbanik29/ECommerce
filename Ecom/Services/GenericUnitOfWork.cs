using Ecom.EntityF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecom.Services
{
    public class GenericUnitOfWork : IDisposable
    {
        private dbMyOnlineShoppingEntities DbShoppingEntity = new dbMyOnlineShoppingEntities();
        public IService<Tbl_EntityType> GetRepositoryInstance<Tbl_EntityType>() where Tbl_EntityType : class
        {
            return new GenericService<Tbl_EntityType>(DbShoppingEntity);
        }
        public void SaveChanges()
        {
            DbShoppingEntity.SaveChanges();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    DbShoppingEntity.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool disposed = false;
    }
}