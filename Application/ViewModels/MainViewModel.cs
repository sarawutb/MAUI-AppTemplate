using MAUIPos.Application.Models;
using MAUIPos.Application.Views.Screen;
using MAUIPos.Application.Views.Widget;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;
using UraniumUI.Icons.MaterialIcons;
namespace MAUIPos.Application.ViewModels
{
    public partial class MainViewModel : BaseViewModel
    {
        public ICommand GoToPageCommand => new RelayCommand<Type>(OnGoToPage);
        public ICommand GoToScreen2Command => new RelayCommand(OnGoToScreen2);
        public ICommand Tab1TappedCommand => new RelayCommand(() => OnTabTapped(1));
        public ICommand Tab2TappedCommand => new RelayCommand(() => OnTabTapped(2));
        public ICommand Tab3TappedCommand => new RelayCommand(() => OnTabTapped(3));
        public ICommand Tab4TappedCommand => new RelayCommand(() => OnTabTapped(4));

        public MainViewModel()
        {
            SelectedTab1 = true;
            CreateAppMenu();
            CreateAppMenuAppShell();
        }

        [ObservableProperty]
        public ObservableCollection<MenuItemModel> menuItems;

        private bool _selectedTab1;
        public bool SelectedTab1
        {
            get => _selectedTab1;
            set
            {
                _selectedTab1 = value;
                OnPropertyChanged();
            }
        }
        private bool _selectedTab2;
        public bool SelectedTab2
        {
            get => _selectedTab2;
            set
            {
                _selectedTab2 = value;
                OnPropertyChanged();
            }
        }
        private bool _selectedTab3;
        public bool SelectedTab3
        {
            get => _selectedTab3;
            set
            {
                _selectedTab3 = value;
                OnPropertyChanged();
            }
        }
        private bool _selectedTab4;
        public bool SelectedTab4
        {
            get => _selectedTab4;
            set
            {
                _selectedTab4 = value;
                OnPropertyChanged();
            }
        }

        private async void OnGoToPage(Type Page)
        {
            try
            {
                //var MainPage = Shell.Current.CurrentPage;
                //await MainPage.Navigation.PushAsync(new ChatView());
                //await MainPage.Navigation.PushAsync(new ChatRoomView());
                //await MainPage.Navigation.PushAsync(new Screen2());
                //await Shell.Current.GoToAsync(nameof(ChatRoomView)); // Navigate to Page2
                //
                //await Shell.Current.GoToAsync(nameof(Screen2)); // Navigate to Page3
                //await Shell.Current.Navigation.PopAsync(true); // Back one step
                //await Shell.Current.Navigation.PopAsync(true); // Back another step
                //await Shell.Current.GoToAsync("..", true);
                //await Shell.Current.GoToAsync("..", true);
                //await Route.GoToAsync(Page);
                //await Task.Delay(1000); // รอให้ UI อัปเดต
                //await Route.GoToAsync<ChatRoomView>();
                //await Task.Delay(1000); // รอให้ UI อัปเดต
                //await Route.GoToAsync<Screen2>();
                //return;
                MainThread.BeginInvokeOnMainThread(() => { Route.GoToAsync(Page); });
            }
            catch (Exception ex)
            {
                await SnackbarWidget.ShowSnackbar(ex.Message);
            }
        }

        private async void OnGoToScreen2()
        {
            try
            {
                await Route.GoToAsync<Screen2>();
            }
            catch (Exception ex)
            {
                await SnackbarWidget.ShowSnackbar(ex.Message);
            }
        }

        private void CreateAppMenu()
        {
            MenuItems = new ObservableCollection<MenuItemModel>
            {
                new MenuItemModel { Title = "Chat", Icon = MaterialRegular.Chat,Color = ((Color)App.Current.Resources["MaterialDanger"]).ToHex() , Page = typeof(ChatView)},
                new MenuItemModel { Title = "Page", Icon = MaterialRegular.Pages,Color = ((Color)App.Current.Resources["MaterialDanger"]).ToHex() , Page = typeof(Page1)},
                // new MenuItemModel { Title = "MainPage", Icon = MaterialRegular.Network_wifi,Color = ((Color)App.Current.Resources["MaterialDanger"]).ToHex() , Page = typeof(MainPage)},
            //    new MenuItemModel { Title = "Sales", Icon = MaterialRegular.Point_of_sale,Color = ((Color)App.Current.Resources["MaterialDanger"]).ToHex()},
            //    new MenuItemModel { Title = "Customers", Icon = MaterialRegular.Verified_user,Color = ((Color)App.Current.Resources["MaterialSuccess"]).ToHex() },
            //    new MenuItemModel { Title = "Appointments", Icon = MaterialRegular.Apple,Color = ((Color)App.Current.Resources["MaterialWarning"]).ToHex() },
            //    new MenuItemModel { Title = "Tickets", Icon = MaterialRegular.Tiktok,Color = ((Color)App.Current.Resources["MaterialInfo"]).ToHex() },
            //    new MenuItemModel { Title = "Purchase", Icon = MaterialRegular.Star_border_purple500,Color = ((Color)App.Current.Resources["MaterialPrimary"]).ToHex() },
            //    new MenuItemModel { Title = "Suppliers", Icon = MaterialRegular.Dashboard_customize,Color = ((Color)App.Current.Resources["MaterialWarning"]).ToHex() },
            //    new MenuItemModel { Title = "Stock", Icon = MaterialRegular.Stop_circle,Color = ((Color)App.Current.Resources["MaterialDanger"]).ToHex() },
            //    new MenuItemModel { Title = "Accounts", Icon = MaterialRegular.Account_box ,Color = ((Color)App.Current.Resources["MaterialInfo"]).ToHex()},
            //    new MenuItemModel { Title = "Production", Icon = MaterialRegular.Production_quantity_limits,Color = ((Color)App.Current.Resources["MaterialWarning"]).ToHex() },
            //    new MenuItemModel { Title = "Maintenance", Icon =MaterialRegular.Domain ,Color = ((Color)App.Current.Resources["MaterialInfo"]).ToHex()},
            //    new MenuItemModel { Title = "Assignments", Icon = MaterialRegular.Assessment,Color = ((Color)App.Current.Resources["MaterialPrimary"]).ToHex() },
            //    new MenuItemModel { Title = "Status", Icon = MaterialRegular.Wallet,Color = ((Color)App.Current.Resources["MaterialDanger"]).ToHex() },
            //    new MenuItemModel { Title = "HRM", Icon = MaterialRegular.Highlight_remove ,Color = ((Color)App.Current.Resources["MaterialWarning"]).ToHex()},
            //    new MenuItemModel { Title = "Attendance", Icon = MaterialRegular.Attachment ,Color = ((Color)App.Current.Resources["MaterialDanger"]).ToHex()},
            //    new MenuItemModel { Title = "Directory", Icon = MaterialRegular.Directions ,Color = ((Color)App.Current.Resources["MaterialSuccess"]).ToHex()},
            //    new MenuItemModel { Title = "CCTV", Icon = MaterialRegular.Camera,Color = ((Color)App.Current.Resources["MaterialWarning"]).ToHex() },
            //    new MenuItemModel { Title = "Device", Icon = MaterialRegular.Devices,Color = ((Color)App.Current.Resources["MaterialWarning"]).ToHex() }
            };
        }

        private void OnTabTapped(int Tab)
        {
            if (Tab == 1)
            {
                SelectedTab1 = true;
                SelectedTab2 = false;
                SelectedTab3 = false;
                SelectedTab4 = false;
            }
            else if (Tab == 2)
            {
                SelectedTab1 = false;
                SelectedTab2 = true;
                SelectedTab3 = false;
                SelectedTab4 = false;
            }
            else if (Tab == 3)
            {
                SelectedTab1 = false;
                SelectedTab2 = false;
                SelectedTab3 = true;
                SelectedTab4 = false;
            }
            else if (Tab == 4)
            {
                SelectedTab1 = false;
                SelectedTab2 = false;
                SelectedTab3 = false;
                SelectedTab4 = true;
            }
        }
        private void CreateAppMenuAppShell()
        {
            var _appShell = MauiProgram.Services.GetService<AppShell>();
            this.MenuItems.ToList().ForEach(e =>
            {

                //if (!MenuItems.Select(a => a.Route).Contains(e.Route))
                _appShell!.Items.Add(new FlyoutItem
                {
                    Title = e.Title,
                    Icon = e.Icon,
                    Route = e.Route,
                    Items =
                    {
                        new ShellContent { }
                    }
                });
            });
        }
    }
}
