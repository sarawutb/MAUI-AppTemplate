namespace MAUIPos.Application.Views.Screen;

[QueryProperty(nameof(PageId), "id")]
[QueryProperty(nameof(UserName), "name")]
public partial class Page2 : ContentPage
{
    public int PageId { get; set; }
    public string UserName { get; set; }
    public Page2()
	{
		InitializeComponent();
        BindingContext = this;
    }

    private async void OnNextPageClicked(object sender, EventArgs e)
    {
        await Route.GoToAsync<Page3>();
        //await Shell.Current.GoToAsync(nameof(Page3));
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Route.GoBackAsync();
        //await Shell.Current.GoToAsync(".."); // Navigate back
    }
}