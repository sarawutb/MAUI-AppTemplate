using MAUIPos.Application.Views.Screen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIPos.Application
{
    public static class Route
    {
        private static readonly Dictionary<Type, string> _registeredRoutes = new();

        public static string RouteParameters(string routeName, Dictionary<string, object> parameters)
        {
            if (parameters == null || parameters.Count == 0)
                return routeName;

            var queryParams = string.Join("&", parameters.Select(kv => $"{kv.Key}={kv.Value}"));
            return $"{routeName}?{queryParams}";
        }
        private static void RegisterRoute<T>(string? routeName = null)
        {
            if(routeName == null)
                routeName = typeof(T).Name;
            Routing.RegisterRoute(routeName, typeof(T));
            _registeredRoutes[typeof(T)] = routeName;
        }

        public static void RegisterRoute()
        {
            RegisterRoute<MainView>("//MainView");
            RegisterRoute<LoginView>("//LoginView");
            RegisterRoute<ToDoView>();
            RegisterRoute<ChatView>();
            RegisterRoute<ChatRoomView>();
            RegisterRoute<Screen2>();
            //RegisterRoute<Page1>("//MainView/Page1");
            //RegisterRoute<Page2>("//MainView/Page1/Page2");
            //RegisterRoute<Page3>("//MainView/Page1/Page2/Page3");
            RegisterRoute<Page1>();
            RegisterRoute<Page2>();
            RegisterRoute<Page3>();
            // RegisterRoute<MainPage>();

        }

        public static async Task GoBackAsync(bool animate = true)
        {
            if (App.Current.MainPage is AppView)
            {
                var mainPage = App.Current.MainPage as AppView;
                await mainPage.Navigation.PopAsync(animate);
            }
            else if (App.Current.MainPage is AppShell)
            {
                await Shell.Current.GoToAsync("..", animate);
            }
        }

        public static async Task GoBackWithDataAsync(Dictionary<string, object>? parameters = null, bool animate = true)
        {
            ////ex
            //await Shell.Current.GoToAsync($"..?parameterToPassBack={parameterValueToPassBack}");
            if (App.Current.MainPage is AppView)
            {
                var mainPage = App.Current.MainPage as AppView;
                await mainPage.Detail.Navigation.PopAsync(animate);
            }
            else if (App.Current.MainPage is AppShell)
            {
                await Shell.Current.GoToAsync("..", animate, parameters);
            }
        }

        public static async Task GoToAsync(Type pageType, Dictionary<string, object>? parameters = null, bool animate = true)
        {
            await _GoToAsync(pageType, parameters);
        }

        public static async Task GoToAsync<T>(Dictionary<string, object>? parameters = null, bool animate = true)
        {
            await _GoToAsync(typeof(T), parameters);
        }

        public static async Task PopToRootAsync(bool animate = true)
        {
            await Shell.Current.Navigation.PopToRootAsync(animate);
        }

        public static async Task PushModalAsync<T>(bool animate = true)
        {
            var page = (Page)Activator.CreateInstance(typeof(T))!;
            await Shell.Current.Navigation.PushModalAsync(page, animate);
        }

        public static async Task PushModalAsync(Type pageType, bool animate = true)
        {
            var page = (Page)Activator.CreateInstance(pageType)!;
            await Shell.Current.Navigation.PushModalAsync(page, animate);
        }

        public static async Task PopModalAsync(bool animate = true)
        {
            await Shell.Current.Navigation.PopModalAsync(animate);
        }

        private static async Task _GoToAsync(Type pageType, Dictionary<string, object>? parameters, bool animate = true)
        {
            try
            {
                if (parameters == null)
                    parameters = new Dictionary<string, object>();
                //if (parameters != null)
                //{
                //    var mainPage = App.Current.MainPage as AppView;
                //    var pageTo = (Page)Activator.CreateInstance(pageType);
                //    await mainPage.Detail.Navigation.PushAsync(pageTo, animate);
                //    //await Shell.Current.GoToAsync(route, true, parameters);
                //}
                //else
                //{
                var pageName = pageType.Name;
                if (App.Current.MainPage is AppView)
                {
                    var _appFlyout = App.Current.MainPage as AppView;
                    var pageTo = (Page)Activator.CreateInstance(pageType);
                    await _appFlyout.Detail.Navigation.PushAsync(pageTo, animate);
                }
                else if (App.Current.MainPage is AppShell)
                {
                    if (Shell.Current.CurrentPage.GetType() == pageType)
                        return;
                    //if (!_registeredRoutes.TryGetValue(pageType, out string route))
                    //    throw new Exception($"Route for {pageType.Name} is not registered.");

                    //if (_registeredRoutes.TryGetValue(pageType, out var routes) && routes.TryGetValue(pageName, out string route))
                    if (_registeredRoutes.TryGetValue(pageType, out string? route))
                    {
                        await Shell.Current.GoToAsync(route, animate, parameters);
                    }
                    else
                    {
                        throw new Exception($"Route for {pageType.Name} is not registered.");
                    }

                    //route = (pageType == typeof(MainView) || pageType == typeof(LoginView)) ? $"//{route}" : $"{route}";
                    //await Shell.Current.GoToAsync(route, true);
                }

                //x.Detail = new AppShell();
                //var appSell = ((AppShell)x.Detail as AppShell);
                //if (Shell.Current.CurrentPage.GetType() == pageType)
                //    return;

                //if (!_registeredRoutes.TryGetValue(pageType, out string route))
                //    throw new Exception($"Route for {pageType.Name} is not registered.");

                //route = (pageType == typeof(MainView) || pageType == typeof(LoginView)) ? $"//{route}" : $"{route}";  
                //await Shell.Current.GoToAsync(route, true);
                //}
                //string navigationPath = (pageType == typeof(MainView) || pageType == typeof(LoginView)) ? $"//{route}" : route;
                //await Shell.Current.GoToAsync(navigationPath, true, parameters ?? new Dictionary<string, object>());
                //Page pageInstance = (Page)Activator.CreateInstance(pageType);
                //await Shell.Current.Navigation.PushAsync(pageInstance);
                if (Shell.Current.FlyoutIsPresented)
                    Shell.Current.FlyoutIsPresented = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static string GetRouteName<T>()
        {
            return "";
            //return _registeredRoutes.FirstOrDefault(r => r.Key == typeof(T)).Value;
        }
    }
}
