using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;
using Atestat4.Models;
using SQLite;

namespace Atestat4.Services
{
    public class UserDatabase
    {
        private readonly SQLiteAsyncConnection _database;

        public UserDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<User>().Wait();
        }

        public Task<List<User>> GetUsersAsync()
        {
            return _database.Table<User>().ToListAsync();
        }

        public Task<User> GetUserByEmailAsync(string email)
        {
            return _database.Table<User>().Where(u => u.Email == email).FirstOrDefaultAsync();
        }

        public Task<int> SaveUserAsync(User user)
        {
            return _database.InsertAsync(user);
        }

        public Task<int> DeleteUserAsync(User user)
        {
            return _database.DeleteAsync(user);
        }

        public async Task<User> GetUserByIdentifierAsync(string identifier, string password)
        {
            var users = await _database.Table<User>().ToListAsync();

            var match = users.FirstOrDefault(u => u.Email == identifier && u.Password == password);

            if (match == null)
            {
                match = users.FirstOrDefault(u => u.FullName == identifier && u.Password == password);
            }

            return match;
        }

        public async Task<bool> IsEmailAlreadyRegisteredAsync(string email)
        {
            var user = await _database.Table<User>().Where(u => u.Email == email).FirstOrDefaultAsync();
            return user != null;
        }

    }
}
