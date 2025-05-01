using MAUIPos.Application.Constant;
using MAUIPos.Application.Services;
using MAUIPos.Application.Views.Screen;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MAUIPos.Application.ViewModels
{
    public partial class AppShellViewModel : BaseViewModel
    {
        private readonly CacheSystemService _cacheSystemService;
        private readonly AuthService _authService;
        public ICommand MenuGotoCommand => new Command<string>(OnMenuViewGoto);
        public ICommand LogoutCommand => new Command(OnLogout);

        public AppShellViewModel()
        {
            _cacheSystemService = MauiProgram.Services.GetService<CacheSystemService>();
            _authService = MauiProgram.Services.GetService<AuthService>();
            //_cacheSystemService.SetCache(ConstantString.TOKEN, "tset");
        }

        //public DataTemplate CurrentPage => new DataTemplate(typeof(MainView));

        //[ObservableProperty]
        //private string _test = "1234";
        private async void OnLogout()
        {
            _authService.LogOut();
            await App.SetCurrentPage();
        }

        private async void OnMenuViewGoto(string route)
        {
            Type type = AppDomain.CurrentDomain
                                 .GetAssemblies()
                                 .SelectMany(a => a.GetTypes())
                                 .FirstOrDefault(t => t.Name == route);

            await Route.GoToAsync(type);
            //if (nameMenu == "Home") Route.GoToAsync<MainView>();
            //else if (nameMenu == "Screen2") Route.GoToAsync<Screen2>();
            //else if (nameMenu == "ToDo") Route.GoToAsync<ToDoView>();
            //else if (nameMenu == "LogOut") 
            //{
            //    _authService.LogOut();
            //    App.SetCurrentPage();
            //}
        }
    }
}
