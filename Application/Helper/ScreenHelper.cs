namespace MAUIPos.Application.Helper
{
    public static class ScreenHelper
    {
        public static DisplayInfo DisplayInfo = DeviceDisplay.MainDisplayInfo;
      
        public static double ScreenHeight => GetScreenSize().Height;
       
        public static double SetScreenWidth(double widthPercentage = 0) => GetScreenSize().Width * (widthPercentage / 100);
       
        public static double SetScreenHeight(double heightPercentage = 0) => GetScreenSize().Height * (heightPercentage / 100);

        public static (double Width, double Height) GetScreenSize()
        {
            double width = DisplayInfo.Width / DisplayInfo.Density;
            double height = DisplayInfo.Height / DisplayInfo.Density;
            return (width, height);
        }

        public static Thickness SetMarginOrPadding(double widthPercentage = 0, double heightPercentage = 0)
        {
            var screenSize = GetScreenSize();

            double marginWidth = screenSize.Width * (widthPercentage / 100);
            double marginHeight = screenSize.Height * (heightPercentage / 100);

            return new Thickness(marginWidth, marginHeight);
        }

        public static Thickness SetMarginOrPadding(double leftPercent = 0, double topPercent = 0, double rightPercent = 0, double bottomPercent = 0)
        {
            var screenSize = GetScreenSize();
            return new Thickness(
                screenSize.Width * (leftPercent / 100),
                screenSize.Height * (topPercent / 100),
                screenSize.Width * (rightPercent / 100),
                screenSize.Height * (bottomPercent / 100)
            );
        }
    }
}
