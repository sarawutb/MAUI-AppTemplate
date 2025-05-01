using CommunityToolkit.Maui.Views;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MAUIPos.Application.Widget
{
    public class CustomPopup : Popup
    {
        public bool IsDisposed;
        public CustomPopup(View view)
        {
            CanBeDismissedByTappingOutsideOfPopup = true;
            Size = new Size(DeviceDisplay.MainDisplayInfo.Width, DeviceDisplay.MainDisplayInfo.Height);
            Color = Colors.Transparent;
            Content = new ContentView
            {
                BackgroundColor = Colors.Transparent,
                Content = view
            };
        }

        protected override Task OnClosed(object? result, bool wasDismissedByTappingOutsideOfPopup, CancellationToken token = default)
        {
            IsDisposed = false;
            return base.OnClosed(result, wasDismissedByTappingOutsideOfPopup, token);
        }
    }
}
