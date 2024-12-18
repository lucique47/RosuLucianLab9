using System;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RosuLucianLab7.Models;
using System.Collections;

namespace RosuLucianLab7.Data
{
    public class ShoppingListDatabase
    {
        private SQLiteAsyncConnection _database;
        public ShoppingListDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<ShopList>().Wait();
            _database.CreateTableAsync<Product>().Wait();
            _database.CreateTableAsync<ListProduct>().Wait();
            _database.CreateTableAsync(typeof(Shop)).Wait();
        }

        public Task<int> SaveProductAsync(Product product)
        {
            if (product.ID != 0)
            {
                return _database.UpdateAsync(product);
            }
            else
            {
                return _database.InsertAsync(product);
            }
        }
        public Task<int> DeleteProductAsync(Product product)
        {
            return _database.DeleteAsync(product);
        }
        public Task<List<Product>> GetProductsAsync()
        {
            return _database.Table<Product>().ToListAsync();
        }
        public Task<int> SaveListProductAsync(ListProduct listp)
        {
            if (listp.ID != 0)
            {
                return _database.UpdateAsync(listp);
            }
            else
            {
                return _database.InsertAsync(listp);
            }
        }
        public Task<List<Product>> GetListProductsAsync(int shoplistid)
        {
            return _database.QueryAsync<Product>(
            "select P.ID, P.Description from Product P"
            + " inner join ListProduct LP"
            + " on P.ID = LP.ProductID where LP.ShopListID = ?",
            shoplistid);
        }

        internal async Task<IEnumerable> GetShopListsAsync()
        {
            return await _database.Table<ShopList>().ToListAsync();
        }

        internal async Task SaveShopListAsync(ShopList slist)
        {
            if (slist.ID != 0)
            {
                await _database.UpdateAsync(slist);
            }
            else
            {
                await _database.InsertAsync(slist);
            }
        }

        internal async Task DeleteShopListAsync(ShopList slist)
        {
            await _database.DeleteAsync(slist);
        }
        public Task<List<Shop>> GetShopsAsync() => _database.QueryAsync<Shop>("SELECT ID, ShopName, Adress FROM Shop");

        public Task<int> SaveShopAsync(Shop shop)
        {
            if (shop.ID != 0)
            {
                return _database.UpdateAsync(shop);
            }
            else
            {
                return _database.InsertAsync(shop);
            }
        }
    }
}

