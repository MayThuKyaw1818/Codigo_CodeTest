using StackExchange.Redis;

namespace POS_System.Helper
{
    public class ConnectionHelper
    {
        static ConnectionHelper()
        {
            ConfigurationOptions option = new ConfigurationOptions
            {
                AbortOnConnectFail = false,
                EndPoints = { ConfigurationManager.AppSetting["RedisCacheUrl"] }
            };

            ConnectionHelper.lazyConnection = new Lazy<ConnectionMultiplexer>(() => {
                return ConnectionMultiplexer.Connect(option);
            });
        }
        private static Lazy<ConnectionMultiplexer> lazyConnection;
        public static ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }
    }
}
