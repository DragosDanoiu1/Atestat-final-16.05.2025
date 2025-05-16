namespace Atestat4;

public partial class SignInPage : ContentPage
{
    bool isPasswordVisible = false;

    public SignInPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        NameOrEmail.Text = string.Empty;
        Password.Text = string.Empty;
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
        string email = NameOrEmail.Text?.Trim() ?? "";

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

    private void Password_TextChanged(object sender, EventArgs e)
    {
    }

    private void TogglePasswordVisibilityButton_Clicked(object sender, EventArgs e)
    {
        isPasswordVisible = !isPasswordVisible;
        Password.IsPassword = !isPasswordVisible;
        TogglePasswordVisibilityButton.ImageSource = isPasswordVisible ? "eye_off.png" : "eye_on.png";
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegisterPage());
    }

    private async void SignIn_Clicked(object sender, EventArgs e)
    {
        string identifier = NameOrEmail.Text?.Trim();
        string password = Password.Text;

        if (string.IsNullOrWhiteSpace(identifier) || string.IsNullOrEmpty(password))
        {
            await DisplayAlert("Error", "Please enter both identifier and password.", "OK");
            return;
        }

        var user = await App.Database.GetUserByIdentifierAsync(identifier, password);

        if (user != null)
        {
            await Shell.Current.GoToAsync("//home");
        }
        else
        {
            await DisplayAlert("Login Failed", "Invalid credentials. Please try again.", "OK");
        }
    }
    private void NameOrEmail_Completed(object sender, EventArgs e)
    {
        Password.Focus();  // mută focusul pe câmpul de parolă când apeşi Enter în primul entry
    }

}
