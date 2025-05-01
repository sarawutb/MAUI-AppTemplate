namespace MAUIPos.Application.Views.Screen;

public partial class Page1 : ContentPage
{
    public Page1()
    {
        InitializeComponent();
    }

    private async void OnNextPageClicked(object sender, EventArgs e)
    {
        //await Shell.Current.GoToAsync(nameof(Page2), new Dictionary<string, object>
        //{
        //    { "id", 1234 },
        //    { "name", "John" }
        //});
        //return;
        await Route.GoToAsync<Page2>(new Dictionary<string, object>
        {
            { "id", 1234 },
            { "name", "John" }
        });
        //await Shell.Current.GoToAsync(nameof(Page2));
    }
}