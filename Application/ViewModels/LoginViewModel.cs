using MAUIPos.Application.Constant;
using MAUIPos.Application.Services;
using MAUIPos.Application.Views.Widget;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Windows.Input;
using UraniumUI.Dialogs;

namespace MAUIPos.Application.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {
        private readonly IDialogService _dialogService = MauiProgram.Services.GetService<IDialogService>()!;
        private readonly CacheSystemService _cacheSystemService = MauiProgram.Services.GetService<CacheSystemService>()!;
        private readonly AppShellViewModel _appShellViewModel = MauiProgram.Services.GetService<AppShellViewModel>()!;
        private readonly AppConfigService _appConfigService = MauiProgram.Services.GetService<AppConfigService>()!;
        private readonly LoadingService _loading = MauiProgram.Services.GetService<LoadingService>()!;

        public ICommand LoginCommand { get; }
        public ICommand FromValidateCommand { get; }
        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(OnLogin);
        }

        protected async override void OnStart()
        {
            Mode = _appConfigService.APP_MODE;
            Version = $"ver. {_appConfigService.APP_VERSION}";
            Username = "admin@test.com";
            Password = "12345678";
            base.OnStart();
        }

        [ObservableProperty]
        private string mode, version;

        [EmailAddress(ErrorMessage = "อีเมลไม่ถูกต้อง")]
        public string Email { get; set; } = "admin@test.com";

        [ObservableProperty]
        private string username, password;

        public async Task Login(CancellationToken cancellationToken)
        {
            try
            {
                _cacheSystemService.SetCache(ConstantString.TOKEN, "test");
                await Task.Delay(3000);
                await App.SetCurrentPage();
            }
            catch (TaskCanceledException)
            {
                Console.WriteLine("Test task was canceled.");
            }
        }


        private async void OnLogin()
        {
            //var x = MauiProgram.Services.GetService<LoadingWidget>();
            await _loading.ShowLoading(Login, true);
            return;
            var cts = new CancellationTokenSource();
            await UraniumUI.Dialogs.CommunityToolkit.CommunityToolkitDialogExtensions.DisplayFormViewAsync(App.Current.MainPage, "Test", new ContentView(), "OK");

            using (await _dialogService.DisplayProgressCancellableAsync(null, "Work in progress, please wait...", "Cancel", cts))
            {
                try
                {
                    // Indicate a long running operation
                    //await Task.Delay(3000, cts.Token);
                    _cacheSystemService.SetCache(ConstantString.TOKEN, "test");
                    await App.SetCurrentPage();
                }
                catch (TaskCanceledException)
                {
                    // Handle cancellation
                    Console.WriteLine("Progress dialog cancelled");
                }
            }
        }
    }
}
