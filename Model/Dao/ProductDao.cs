using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ProductDao
    {
        OnlineShopDbContext db = null;

        public ProductDao()
        {
            db = new OnlineShopDbContext();
        }   

        public List<Product> ListNewProduct(int top)
        {
            return db.Product.OrderByDescending(x => x.CreatedDate).Take(top).ToList();
        }

        /// <summary>
        /// get list products by category
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public List<Product> ListByCategoryId(long categoryID,ref int totalRecord, int pageIndex = 1,int pageSize = 2)
        {
            totalRecord = db.Product.Where(x => x.CategoryID == categoryID).Count();
            var model = db.Product.Where(x => x.CategoryID == categoryID).Skip((pageIndex-1)*pageSize).Take(pageSize).ToList();

            return model;
        }

        /// <summary>
        /// list features product
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public List<Product> ListFeatureProduct(int top)
        {
            return db.Product.Where(x => x.TopHot != null && x.TopHot > DateTime.Now).OrderByDescending(x => x.CreatedDate).Take(top).ToList();
        }

        public List<Product> ListRelatedProduct(long productId)
        {
            var product = db.Product.Find(productId);
            return db.Product.Where(x => x.ID != productId && x.CategoryID == product.CategoryID).ToList();
        }

        public Product ViewDetail(long id)
        {
            return db.Product.Find(id);
        }
    }
}
