using System;
using Microsoft.Maui.Controls;
using System.Threading.Tasks;
using Atestat4.Services; // pentru clasa UserDatabase
using Atestat4.Models;    // pentru clasa User

namespace Atestat4
{
    public partial class RegisterPage : ContentPage
    {
        private bool isPasswordVisible = false;
        public RegisterPage()
        {
            InitializeComponent();

            // Ascunde bara de navigare
            Shell.SetNavBarIsVisible(this, false);
            Shell.SetFlyoutBehavior(this, FlyoutBehavior.Disabled);
        }

        private bool IsFullNameValid(string fullName)
        {
            // Check if FullName is null, empty, or contains only whitespace
            if (string.IsNullOrWhiteSpace(fullName))
            {
                return false;
            }

            // Split the FullName into parts (expecting two parts: last name and first name)
            var nameParts = fullName.Split(' ');

            // Check if FullName contains exactly two parts (first name and last name)
            return nameParts.Length == 2 && nameParts.All(part => !string.IsNullOrWhiteSpace(part));
        }

        private void FullName_TextChanged(object sender, EventArgs e)
        {
            string name = FullName.Text;

            if (IsFullNameValid(name))
            {
                NameValidationLabel.IsVisible = false;
            }
            else
            {
                NameValidationLabel.IsVisible = true;
            }
        }


        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void Email_TextChanged(object sender, EventArgs e)
        {
            bool ValidEmail;
            string email = Email.Text?.Trim() ?? "";

            if (IsValidEmail(email))
            {
                EmailValidationLabel.IsVisible = false;
                ValidEmail = true;
            }
            else
            {
                EmailValidationLabel.IsVisible = true;
                ValidEmail = false;
            }
        }


        private void Password_TextChanged(object sender, TextChangedEventArgs e)
        {
            string password = e.NewTextValue;

            bool hasMinimumLength = password.Length >= 5;
            bool hasNumber = password.Any(char.IsDigit);
            bool hasSpecialChar = password.Any(ch => !char.IsLetterOrDigit(ch));

            bool isValid = hasMinimumLength && hasNumber && hasSpecialChar;

            PasswordValidationLabel.IsVisible = !isValid;
            PasswordMatchLabel.IsVisible = isValid;
            //RegisterButton.IsEnabled = isValid;
        }

        private void CheckPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool isMatch = Password.Text == CheckPassword.Text;

            PasswordMatchLabel.IsVisible = !isMatch;

            if (isMatch) PasswordMatchLabel.IsVisible = false;

        }

        private void TogglePasswordVisibilityButton_Clicked(object sender, EventArgs e)
        {
            isPasswordVisible = !isPasswordVisible;
            Password.IsPassword = !isPasswordVisible;

            TogglePasswordVisibilityButton.ImageSource = isPasswordVisible ? "eye_off.png" : "eye_on.png";
        }

        private bool IsPasswordValid(string password)
        {
            if (string.IsNullOrEmpty(password) || password.Length < 5)
                return false;

            bool hasNumber = password.Any(char.IsDigit);
            bool hasSpecial = password.Any(ch => !char.IsLetterOrDigit(ch));

            return hasNumber && hasSpecial;
        }

        private async void RegisterButton_Clicked(object sender, EventArgs e)
        {
            string name = FullName.Text;
            string email = Email.Text?.Trim() ?? "";
            string password = Password.Text ?? "";
            string confirmPassword = CheckPassword.Text ?? "";

            if (!IsFullNameValid(name))
            {
                DisplayAlert("Invalid Name", "Please enter your name in the required format.", "OK");
                return;
            }

            if (!IsValidEmail(email))
            {
                DisplayAlert("Invalid Email", "Please enter a valid email address.", "OK");
                return;
            }

            bool isEmailRegistered = await App.Database.IsEmailAlreadyRegisteredAsync(email);
            if (isEmailRegistered)
            {
                DisplayAlert("Email Taken", "This email is already registered with another account.", "OK");
                return;
            }

            if (!IsPasswordValid(password))
            {
                DisplayAlert("Invalid Password", "Password must be at least 5 characters long, include at least one number and one special character.", "OK");
                return;
            }

            if (password != confirmPassword)
            {
                DisplayAlert("Password Mismatch", "Passwords do not match.", "OK");
                return;
            }

            DisplayAlert("Success", "Registration validated successfully.", "OK");

            await App.Database.SaveUserAsync(new User
            {
                FullName = FullName.Text,
                Email = Email.Text,
                Password = Password.Text
            });

            //await Navigation.PushAsync(new SignInPage());
            await Shell.Current.GoToAsync("//signin");
        }
        private async void HaveAccount_Clicked(object obj, EventArgs e)
        {
            //await Navigation.PushAsync(new SignInPage());
            await Shell.Current.GoToAsync("//signin");
        }

    }
}