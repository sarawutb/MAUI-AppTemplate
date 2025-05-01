using CommunityToolkit.Maui.Alerts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIPos.Application.Views.Widget
{
    public class DialogWidget
    {
        public static async Task<bool> DialogYesOrNo(string title, string message)
        {
            return await App.Current.MainPage.DisplayAlert(title, message, "Yes", "No", FlowDirection.MatchParent);
        }

        public static async Task<string> DialogMultiResult(string title, string destruction, params string[] buttons)
        {
            return await App.Current.MainPage.DisplayActionSheet(title, "Cancel", destruction, FlowDirection.MatchParent, buttons);
        }

        public static async Task<string> DialogYesOrNoResult(string title, string message)
        {
            return await App.Current.MainPage.DisplayPromptAsync(title, message, "Yes", "No");
        }
    }
}
