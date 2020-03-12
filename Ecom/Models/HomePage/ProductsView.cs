using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Ecom.EntityF;
using Ecom.Services;
namespace Ecom.Models.HomePage
{
    public class ProductsView
    {
        public GenericUnitOfWork _genericUnitOfWork = new GenericUnitOfWork();
        dbMyOnlineShoppingEntities context = new dbMyOnlineShoppingEntities();
        public IEnumerable<Tbl_Product> ListOfProducts { get; set; }
        public ProductsView CreateModel(string search)
        {
            SqlParameter[] pram = new SqlParameter[] {
                   new SqlParameter("@search",search)
                   };
            IEnumerable<Tbl_Product> data = context.Database.SqlQuery<Tbl_Product>("GetBySearch",pram).ToList();
            return new ProductsView
            {
                ListOfProducts = data
        };
    }
    }
}