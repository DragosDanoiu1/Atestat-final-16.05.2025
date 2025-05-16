using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using Atestat4.Models;
using Atestat4.Services;

namespace Atestat4.Views
{
    public partial class NewPage1 : ContentPage
    {
        public IList<Company> Clients { get; private set; }
        readonly CompanyDatabase _companyDb;

        public NewPage1()
        {
            InitializeComponent();
            _companyDb = new CompanyDatabase(
                Path.Combine(FileSystem.AppDataDirectory, "companies.db3"));
            Shell.SetNavBarIsVisible(this, false);
            Shell.SetFlyoutBehavior(this, FlyoutBehavior.Disabled);
            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadClientsAsync();
        }

        async Task LoadClientsAsync()
        {
            Clients = await _companyDb.GetCompaniesAsync();
            CompaniesView.ItemsSource = Clients;

            CountLabel.Text = Clients.Count.ToString();
            var totalFee = Clients.Sum(c => c.FixedFee);
            TotalFeeLabel.Text = totalFee.ToString("N0", CultureInfo.InvariantCulture) + " RON";
            var totalRevenue = Clients.Sum(c => c.AnnualRevenueBeforeCollaboration);
            IncasariLabel.Text = totalRevenue.ToString("N0", CultureInfo.InvariantCulture) + " RON";
        }

        private async void OnAddClientClicked(object sender, EventArgs e)
            => await Shell.Current.GoToAsync("//addclient");

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.CommandParameter is Company comp)
            {
                bool confirm = await DisplayAlert(
                    "Confirm Delete",
                    $"Delete “{comp.CompanyName}”?",
                    "Yes", "No");
                if (!confirm) return;

                await _companyDb.DeleteCompanyAsync(comp);
                await LoadClientsAsync();
            }
        }

        private async void OnHomeTapped(object sender, EventArgs e)
            => await Shell.Current.GoToAsync("//home");

        private async void OnClientPageTapped(object sender, EventArgs e)
            => await Shell.Current.GoToAsync("//clientpage");

        private async void OnChatTapped(object sender, EventArgs e)
            => await Shell.Current.GoToAsync("//chat");

        private async void OnSignOutTapped(object sender, EventArgs e)
            => await Shell.Current.GoToAsync("//signin");
    }
}
