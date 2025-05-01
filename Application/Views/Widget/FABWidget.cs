using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UraniumUI.Pages;

namespace MAUIPos.Application.Views.Widget
{
    public class FABWidget : ImageButton, IPageAttachment
    {
        public FABWidget()
        {
            //this.Source = new Uri("arrow.png");
            this.WidthRequest = 42;
            this.HeightRequest = 42;
            this.CornerRadius = 21;
            this.BackgroundColor = Colors.Blue;

            this.Clicked += (s, e) => { Console.WriteLine("FAB clicked"); };
        }

        public AttachmentPosition AttachmentPosition => AttachmentPosition.Front;

        public void OnAttached(UraniumContentPage page)
        {
            // Place it right bottom of the page.
            this.TranslationX = page.Width - this.Width - 20;
            this.TranslationY = page.Height - this.Height - 20;
        }
    }
}
