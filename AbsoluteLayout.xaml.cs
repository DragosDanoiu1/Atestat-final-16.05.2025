using System.Threading.Tasks;

namespace Atestat4;

public partial class AbsoluteLayout : ContentPage
{
	public AbsoluteLayout()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
		DisplayAlert("Message", "Button Pressed", "OK");
    }

}