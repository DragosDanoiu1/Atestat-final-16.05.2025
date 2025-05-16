using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atestat4.Models;
using SQLite;

namespace Atestat4.Services
{
    public class CompanyRevenueDatabase
    {
        private readonly SQLiteAsyncConnection _database;

        public CompanyRevenueDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<CompanyRevenue>().Wait();
        }

        public Task<List<CompanyRevenue>> GetRevenuesForCompanyAsync(int companyId)
        {
            return _database.Table<CompanyRevenue>()
                            .Where(r => r.CompanyId == companyId)
                            .OrderBy(r => r.Month)
                            .ToListAsync();
        }

        public Task<int> SaveRevenueAsync(CompanyRevenue revenue)
        {
            return _database.InsertOrReplaceAsync(revenue);
        }

        public Task<int> DeleteRevenueAsync(CompanyRevenue revenue)
        {
            return _database.DeleteAsync(revenue);
        }
    }
}
