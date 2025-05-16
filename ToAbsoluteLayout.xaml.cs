namespace Atestat4;

public partial class ToAbsoluteLayout : ContentPage
{
	public ToAbsoluteLayout()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AbsoluteLayout());
    }
}