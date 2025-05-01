using System.ComponentModel;
using System.Windows.Input;

namespace MAUIPos.Application.Views.Screen.Component;

public partial class MainViewTabComponent : ContentView, INotifyPropertyChanged
{
    public MainViewTabComponent()
    {
        InitializeComponent();
    }

    public static readonly BindableProperty IsSelectedProperty =
        BindableProperty.Create(
            nameof(IsSelected),
            typeof(bool),
            typeof(MainViewTabComponent),
            false,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                var control = (MainViewTabComponent)bindable;
                control.OnPropertyChanged(nameof(IsSelected));
            });

    public bool IsSelected
    {
        get => (bool)GetValue(IsSelectedProperty);
        set => SetValue(IsSelectedProperty, value);
    }

    public static readonly BindableProperty TabTextProperty =
        BindableProperty.Create(nameof(TabText), typeof(string), typeof(MainViewTabComponent), string.Empty, propertyChanged: (b, o, n) => ((MainViewTabComponent)b).OnPropertyChanged(nameof(TabText)));

    public string TabText
    {
        get => (string)GetValue(TabTextProperty);
        set => SetValue(TabTextProperty, value);
    }

    public static readonly BindableProperty TabIconProperty =
        BindableProperty.Create(nameof(TabIcon), typeof(string), typeof(MainViewTabComponent), string.Empty, propertyChanged: (b, o, n) => ((MainViewTabComponent)b).OnPropertyChanged(nameof(TabIcon)));

    public string TabIcon
    {
        get => (string)GetValue(TabIconProperty);
        set => SetValue(TabIconProperty, value);
    }

    public static readonly BindableProperty TabFontFamilyProperty =
        BindableProperty.Create(nameof(TabFontFamily), typeof(string), typeof(MainViewTabComponent), string.Empty, propertyChanged: (b, o, n) => ((MainViewTabComponent)b).OnPropertyChanged(nameof(TabFontFamily)));

    public string TabFontFamily
    {
        get => (string)GetValue(TabFontFamilyProperty);
        set => SetValue(TabFontFamilyProperty, value);
    }

    public static readonly BindableProperty TappedCommandProperty =
        BindableProperty.Create(nameof(TappedCommand), typeof(ICommand), typeof(MainViewTabComponent));

    public ICommand TappedCommand
    {
        get => (ICommand)GetValue(TappedCommandProperty);
        set => SetValue(TappedCommandProperty, value);
    }

    public new event PropertyChangedEventHandler PropertyChanged;
    protected new void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
