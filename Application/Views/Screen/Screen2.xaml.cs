namespace MAUIPos.Application.Views.Screen;

public partial class Screen2 : ContentPage
{
	public Screen2()
	{
		InitializeComponent();
	}

    protected override bool OnBackButtonPressed()
    {
        Route.GoBackAsync();
        return base.OnBackButtonPressed();
    }
}