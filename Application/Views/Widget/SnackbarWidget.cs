using MAUIPos.Application.Extensions;
using MAUIPos.Application.Views.Screen;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIPos.Application.Views.Widget
{
    public class SnackbarWidget : Snackbar
    {
        public static async Task ShowSnackbar(string message, Action? action = null, string actionButtonText = "OK", TimeSpan? duration = null, SnackbarOptions? visualOptions = null, IView? anchor = null)
        {
            CancellationTokenSource cancellationTokenSource = new();

            var snackbar = Snackbar.Make(message, action, actionButtonText, TimeSpan.FromSeconds(3));

            //var snackbar = new Snackbar
            //{
            //    Text = message,
            //    Action = action,
            //    ActionButtonText = IconFont.Add,
            //    Duration = (duration ?? TimeSpan.FromSeconds(3)),
            //    VisualOptions = (visualOptions ?? new SnackbarOptions
            //    {
            //        BackgroundColor = ColorHelper.GetResourceColor("Gray100"),
            //        ActionButtonTextColor = Colors.Green,
            //        CornerRadius = 5,
            //        Font = new Microsoft.Maui.Font(),
            //        TextColor = Colors.Blue,
            //        ActionButtonFont = Microsoft.Maui.Font.SystemFontOfSize(14.0),
            //    }),
            //};

            await snackbar.Show(cancellationTokenSource.Token);
        }
    }
}
