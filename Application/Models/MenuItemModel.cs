namespace MAUIPos.Application.Models
{
    public class MenuItemModel
    {
        public string Title { get; set; }
        public string Icon { get; set; }
        public string Color { get; set; }
        public Type? Page { get; set; }
        public string Route { get => Page?.GetType().Name; }
    }

}
