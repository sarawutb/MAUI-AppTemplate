using System.Collections.ObjectModel;

namespace MAUIPos.Application.Views.Screen.Component;

public partial class PopUpToDoComponent : ContentView
{
    public ObservableCollection<ItemSelection> Items { get; set; }
    public PopUpToDoComponent(ObservableCollection<ItemSelection> items)
    {
        InitializeComponent();
        ListViewItem.ItemsSource = items;
    }

    private void ListViewItem_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var x = e.SelectedItem as ItemSelection;
        if (x != null)
        {
            x.Command.Execute(null);
        }
    }
}