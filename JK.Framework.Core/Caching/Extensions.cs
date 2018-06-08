using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JK.Framework.Core.Caching;

namespace JK.Framework.Core.Caching
{
    /// <summary>
    /// Extensions
    /// </summary>
    public static class CacheExtensions
    {
        /// <summary>
        /// Get a cached item. If it's not in the cache yet, then load and cache it
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="key">Cache key</param>
        /// <param name="acquire">Function to load item if it's not in the cache yet</param>
        /// <returns>Cached item</returns>
        public static T Get<T>(this ICacheManager cacheManager, string key, Func<T> acquire)
        {
            return Get(cacheManager, key, 60, acquire);
        }

        /// <summary>
        /// Get a cached item. If it's not in the cache yet, then load and cache it
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="key">Cache key</param>
        /// <param name="cacheTime">Cache time in minutes (0 - do not cache)</param>
        /// <param name="acquire">Function to load item if it's not in the cache yet</param>
        /// <returns>Cached item</returns>
        public static T Get<T>(this ICacheManager cacheManager, string key, int cacheTime, Func<T> acquire)
        {
            if (cacheManager.IsSet(key))
            {
                return cacheManager.Get<T>(key);
            }

            var result = acquire();
            if (cacheTime > 0)
                cacheManager.Set(key, result, cacheTime);
            return result;
        }

        //public class ProductSupplierImpl : IProductSupplier
        //{
        //    private IRepository<ProductSupplier> _ProductSupplierRepository;
        //    private ICacheManager _CacheManager;
        //    private const string PRODUCTS_BY_ID_KEY = "MMY.productSupplier.uid-{0}";

        //    public ProductSupplierImpl(IRepository<ProductSupplier> useraccountRepository, ICacheManager cacheManager)
        //    {
        //        _ProductSupplierRepository = useraccountRepository;
        //        _CacheManager = cacheManager;
        //    }
        //    使用实例
        //    public ProductSupplier FindWithCache(Guid uid)
        //    {
        //        string key = String.Format(PRODUCTS_BY_ID_KEY, uid);
        //        return _CacheManager.Get(key, () => _ProductSupplierRepository.Table.FirstOrDefault(q => q.Guid == uid && !q.IsDeleted));
        //    }
        //}
    }
}

