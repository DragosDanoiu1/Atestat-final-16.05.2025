using System;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using System.IO;
using Microsoft.Maui.Storage;
using Atestat4.Services;

namespace Atestat4
{
    public partial class App : Application
    {
        static UserDatabase _database;

        public static UserDatabase Database
        {
            get
            {
                if (_database == null)
                {
                    string dbPath = Path.Combine(FileSystem.AppDataDirectory, "users.db3");
                    _database = new UserDatabase(dbPath);
                }
                return _database;
            }
        }
        public App()
        {
            InitializeComponent();

            //MainPage = new NavigationPage(new SignInPage());

            // ↓↓↓ Pornește în AppShell, nu în NavigationPage ↓↓↓
            MainPage = new AppShell();

        }
    }
}