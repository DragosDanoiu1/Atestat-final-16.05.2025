using System;
using System.Linq;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using Atestat4.Models;
using Atestat4.Services;
using Microsoft.Maui.Graphics;


namespace Atestat4.Views
{
    public partial class ClientPage : ContentPage
    {
        readonly CompanyDatabase _db;

        public ClientPage()
        {
            InitializeComponent();

            // ascunde nav bar
            Shell.SetNavBarIsVisible(this, false);
            Shell.SetFlyoutBehavior(this, FlyoutBehavior.Disabled);

            // instanțiem baza
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "companies.db3");
            _db = new CompanyDatabase(dbPath);
        }

        async void OnSearchClicked(object sender, EventArgs e)
        {
            var name = SearchEntry.Text?.Trim();
            if (string.IsNullOrEmpty(name))
            {
                await DisplayAlert("Atenție", "Introduceți un nume de companie.", "OK");
                return;
            }

            // căutăm compania
            var all = await _db.GetCompaniesAsync();
            var comp = all.FirstOrDefault(c =>
              c.CompanyName.Equals(name, StringComparison.InvariantCultureIgnoreCase));

            if (comp == null)
            {
                await DisplayAlert("Nu am găsit", $"Compania “{name}” nu există.", "OK");
                return;
            }

            // setăm titlul paginii
            Title = $"{comp.CompanyName}'s page";

            // golim containerul și afișăm câmp cu câmp
            DetailsContainer.Children.Clear();
            void AddLine(string label, string value)
            {
                DetailsContainer.Children.Add(new Label
                {
                    Text = $"{label}:",
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Color.FromArgb("#007ACC")
                });
                DetailsContainer.Children.Add(new Label
                {
                    Text = value,
                    TextColor = Colors.Black
                });
            }

            AddLine("Name", comp.CompanyName);
            AddLine("Industry", comp.Industry);
            AddLine("Start Date", comp.CollaborationStartDate.ToString("yyyy-MM-dd"));
            AddLine("Annual Revenue", comp.AnnualRevenueBeforeCollaboration.ToString("N0") + " RON");
            AddLine("Annual Profit", comp.AnnualProfitBeforeCollaboration.ToString("N0") + " RON");
            AddLine("Services Provided", comp.ServicesProvided);
            AddLine("Fixed Fee", comp.FixedFee.ToString("N0") + " RON");
            AddLine("Owner Full Name", comp.OwnerFullName);

            // opțional: dai scroll sus
            await ((ScrollView)DetailsContainer.Parent)
                .ScrollToAsync(0, 0, true);
        }
        // în ClientPage.xaml.cs
        private async void OnChatTapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//chat");
        }
        private async void OnSignOutTapped(object sender, EventArgs e)
    => await Shell.Current.GoToAsync("//signin");

        private async void OnClientPageTapped(object sender, EventArgs e)
            => await Shell.Current.GoToAsync("//clientpage");

        private async void OnHomeTapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//home");
        }

    }
}
