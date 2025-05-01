using MAUIPos.Application.ViewModels;
using MAUIPos.Application.Views.Screen;
using MAUIPos.Application.Views.Widget;
using MAUIPos.Application.Widget;
using CommunityToolkit.Maui.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MAUIPos
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            menuHome.Route = typeof(MainView).Name;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
        }

        private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {

        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
        }

        protected override bool OnBackButtonPressed()
        {
            return base.OnBackButtonPressed();
        }

        protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
        {
            base.OnNavigatedFrom(args);
        }
        protected override void OnNavigatingFrom(NavigatingFromEventArgs args)
        {
            base.OnNavigatingFrom(args);
        }

        //protected override async void OnNavigating(ShellNavigatingEventArgs args)
        //{
        //    base.OnNavigating(args);

        //    ShellNavigatingDeferral token = args.GetDeferral();

        //    var result = await DisplayActionSheet("Navigate?", "Cancel", "Yes", "No");
        //    if (result != "Yes")
        //    {
        //        args.Cancel();
        //    }
        //    token.Complete();
        //}
    }
}

public class ItemSelection
{
    public required string Name { get; set; }
    public required ICommand Command { get; set; }
}
