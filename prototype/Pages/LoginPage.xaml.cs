namespace prototype.Pages;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

    async void ToGamePage(object sender, EventArgs e)
    {
		await Navigation.PushAsync(new GamePage());
    }
}