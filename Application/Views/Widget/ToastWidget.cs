using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIPos.Application.Views.Widget
{
    public class ToastWidget : Toast
    {
        public static async Task ShowToast(string message, ToastDuration duration = ToastDuration.Short)
        {
            var toast = Toast.Make(message, duration);
            await toast.Show();
        }
    }
}
