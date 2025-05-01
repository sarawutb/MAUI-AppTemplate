using MAUIPos.Application.Services;
using MAUIPos.Application.Constant;
using MAUIPos.Application.Views.Screen;

namespace MAUIPos;

public partial class App : IApplication
{
	 public App()
        {
            InitializeComponent();
            MainPage = new ContentPage();
            _ = SetCurrentPage();
        }

        public static async Task SetCurrentPage()
        {
            var _cacheSystemService = MauiProgram.Services.GetService<CacheSystemService>();
            var _token = _cacheSystemService?.GetCacheString(ConstantString.TOKEN);
            if (string.IsNullOrEmpty(_token))
            {
                await MainThread.InvokeOnMainThreadAsync(() => App.Current!.MainPage = MauiProgram.Services.GetService<LoginView>());
				  }
            else
            {
                var _appShell = MauiProgram.Services.GetService<AppShell>();
                if (_appShell!.FlyoutIsPresented)
                    _appShell!.FlyoutIsPresented = false;
                await MainThread.InvokeOnMainThreadAsync(() => App.Current!.MainPage = _appShell);
            }
        }
}
