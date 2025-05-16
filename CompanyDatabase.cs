using SQLite;
using Atestat4.Models;

public class CompanyDatabase
{
    private readonly SQLiteAsyncConnection _database;

    public CompanyDatabase(string dbPath)
    {
        _database = new SQLiteAsyncConnection(dbPath);
        _database.CreateTableAsync<Company>().Wait();
    }
    public CompanyDatabase()
            : this(Path.Combine(FileSystem.AppDataDirectory, "companies.db3"))
    {
    }

    public Task<List<Company>> GetCompaniesAsync()
    {
        return _database.Table<Company>().ToListAsync();
    }

    public Task<Company> GetCompanyAsync(int id)
    {
        return _database.Table<Company>().Where(c => c.Id == id).FirstOrDefaultAsync();
    }

    public Task<int> SaveCompanyAsync(Company company)
    {
        if (company.Id != 0)
        {
            return _database.UpdateAsync(company);
        }
        else
        {
            return _database.InsertAsync(company);
        }
    }

    public Task<int> DeleteCompanyAsync(Company company)
    {
        return _database.DeleteAsync(company);
    }
}

