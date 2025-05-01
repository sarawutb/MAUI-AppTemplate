//using CommunityToolkit.Maui.Views;
//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace MAUIPos.Application.Widget
//{
//    public class ItemSelectionPopup : Popup
//    {
//        public ObservableCollection<ItemSelection> Items { get; set; }

//        public ItemSelectionPopup(ObservableCollection<ItemSelection> items)
//        {
//            Items = items;

//            var listView = new ListView
//            {
//                ItemsSource = Items,
//            };

//            listView.ItemTapped += (s, e) =>
//            {
//                var item = e.Item as ItemSelection;
//                item?.Callback.Invoke();
//                Close(e.Item); // Close popup and return the selected item
//            };

//            Content = new VerticalStackLayout
//            {
//                Margin = 10,
//                Padding = 10,
//                BackgroundColor = Colors.Red,
//                Children =
//                    {
//                        //new Label { Text = "Popup Menu", FontSize = 20 },
//                        new Frame
//                        {
//                            Background = Colors.Red,
//                            Content = listView,
//                            Padding = 10,
//                            CornerRadius = 10,
//                            BackgroundColor = Colors.White,
//                            VerticalOptions = LayoutOptions.Center,
//                            HorizontalOptions = LayoutOptions.Center
//                        }
//                    }
//            };
//        }
//    }
//}
