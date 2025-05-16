using System;
using System.Globalization;
using Atestat4.Models;
using Atestat4.Services;
using Microsoft.Maui.Controls;

namespace Atestat4.Views
{
    public partial class AddClientPage : ContentPage
    {
        CompanyDatabase _clientDb;

        public AddClientPage()
        {
            InitializeComponent();

            // Ascunde bara de navigare
            Shell.SetNavBarIsVisible(this, false);
            Shell.SetFlyoutBehavior(this, FlyoutBehavior.Disabled);

            _clientDb = new CompanyDatabase();
        }

        // Butonul Back
        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//home");
        }

        private void OnAddOwnerClicked(object sender, EventArgs e)
        {
            // deocamdată nu facem nimic aici
        }

        async void OnSaveClientClicked(object sender, EventArgs e)
        {
            // 1. Citești textul brut din entry-uri
            string revText = RevenueEntry.Text?.Trim() ?? "0";
            string profText = ProfitEntry.Text?.Trim() ?? "0";
            string feeText = DealValueEntry.Text?.Trim() ?? "0";
            string dateText = CollaborationDateEntry.Text?.Trim() ?? "";

            // 2. Parsează valorile numerice
            if (!decimal.TryParse(revText, NumberStyles.Number, CultureInfo.InvariantCulture, out var revenue))
            {
                await DisplayAlert("Eroare", "Venitul anual trebuie să fie un număr valid.", "OK");
                return;
            }
            if (!decimal.TryParse(profText, NumberStyles.Number, CultureInfo.InvariantCulture, out var profit))
            {
                await DisplayAlert("Eroare", "Profitul anual trebuie să fie un număr valid.", "OK");
                return;
            }
            if (!decimal.TryParse(feeText, NumberStyles.Number, CultureInfo.InvariantCulture, out var fixedFee))
            {
                await DisplayAlert("Eroare", "Comisionul fix trebuie să fie un număr valid.", "OK");
                return;
            }

            // 3. Parsează data colaborării
            if (!DateTime.TryParse(dateText, CultureInfo.InvariantCulture, DateTimeStyles.None, out var collaborationDate))
            {
                await DisplayAlert("Eroare", "Data începerii colaborării nu este într-un format valid (ex: 2025-05-01).", "OK");
                return;
            }

            // 4. Construiește obiectul Company
            var company = new Company
            {
                CompanyName = CompanyEntry.Text?.Trim() ?? "",
                Industry = IndustryEntry.Text?.Trim() ?? "",
                AnnualRevenueBeforeCollaboration = revenue,
                AnnualProfitBeforeCollaboration = profit,
                CollaborationStartDate = collaborationDate,
                ServicesProvided = ServicesEntry.Text?.Trim() ?? "",
                FixedFee = fixedFee,
                NumberOfOwners = int.TryParse(OwnersCountEntry.Text, out var cnt) ? cnt : 0,
                OwnerFullName = OwnerNameEntry.Text?.Trim() ?? ""
            };

            // 5. Salvează în baza de date
            await _clientDb.SaveCompanyAsync(company);

            // 6. Notificare și navigare la HomePage
            await DisplayAlert("Success", "Company saved successfully!", "OK");
            await Shell.Current.GoToAsync("//home");

            // 7. Golește câmpurile
            CompanyEntry.Text = "";
            IndustryEntry.Text = "";
            CollaborationDateEntry.Text = "";
            RevenueEntry.Text = "";
            ProfitEntry.Text = "";
            ServicesEntry.Text = "";
            DealValueEntry.Text = "";
            OwnersCountEntry.Text = "";
            OwnerNameEntry.Text = "";

        }
    }
}
