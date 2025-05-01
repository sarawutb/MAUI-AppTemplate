using Newtonsoft.Json;
using System.Reflection;
using System.Text.Json;

namespace MAUIPos.Application.Services
{
    public class AppConfigService
    {
        public string API_BASEURL { get; private set; }
        public string APP_VERSION { get; private set; }
        public bool IS_FEATURE_ENABLED { get; private set; }
        public string WS_SERVER_URL { get; private set; }
        public string APP_MODE { get; private set; }

        public AppConfigService()
        {
        }

        public async Task LoadConfigAsync()
        {
            try
            {
#if DEBUG
                using var stream = await FileSystem.OpenAppPackageFileAsync("appsettings.Development.json");
#else
                using var stream = await FileSystem.OpenAppPackageFileAsync("/Config/appsettings.Production.json");
#endif
                using var reader = new StreamReader(stream);
                var json = reader.ReadToEnd();

                var config = JsonConvert.DeserializeObject<dynamic>(json);
                if (config != null)
                {
                    API_BASEURL = config.API_BASEURL;
                    APP_VERSION = config.APP_VERSION;
                    IS_FEATURE_ENABLED = config.IS_FEATURE_ENABLED;
                    WS_SERVER_URL = config.WS_SERVER_URL;
                    APP_MODE = config.APP_MODE;
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Error loading config: {ex.Message}");
            }
        }
    }
}
