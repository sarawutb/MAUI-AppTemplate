namespace MAUIPos.Application.Views.Screen;

public partial class Page3 : ContentPage
{
	public Page3()
	{
		InitializeComponent();
	}

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Route.GoBackAsync();
    }
}