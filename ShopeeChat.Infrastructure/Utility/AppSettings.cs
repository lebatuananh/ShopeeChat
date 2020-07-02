using Microsoft.Extensions.Configuration;
using System.Linq;

namespace ShopeeChat.Infrastructure.Utility
{
    public class AppSettings
    {
        private static AppSettings _instance;
        private static readonly object ObjLocked = new object();

        protected AppSettings()
        {
        }

        public void SetConfiguration(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static AppSettings Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (ObjLocked)
                    {
                        if (null == _instance)
                            _instance = new AppSettings();
                    }
                }
                return _instance;
            }
        }

        public string GetString(string key, string defaultValue = "")
        {
            try
            {
                var value = Configuration.GetSection("StringValue").GetChildren().FirstOrDefault(x => x.Key == key)?.Value;
                return string.IsNullOrEmpty(value) ? defaultValue : value;
            }
            catch
            {
                return defaultValue;
            }
        }

        private IConfiguration Configuration { get; set; }

        public static T Get<T>(string key = null)
        {
            if (string.IsNullOrWhiteSpace(key))
                return Instance.Configuration.Get<T>();
            else
            {
                var section = Instance.Configuration.GetSection(key);
                return section.Get<T>();
            }
        }

        public static T Get<T>(string key, T defaultValue)
        {
            if (Instance.Configuration.GetSection(key) == null)
                return defaultValue;

            if (string.IsNullOrWhiteSpace(key))
                return Instance.Configuration.Get<T>();
            else
                return Instance.Configuration.GetSection(key).Get<T>();
        }
    }
}