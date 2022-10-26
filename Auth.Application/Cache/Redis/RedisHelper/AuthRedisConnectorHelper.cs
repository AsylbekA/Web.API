using StackExchange.Redis;

namespace Auth.Application.Cache.Redis.RedisHelper;

public class AuthRedisConnectorHelper
{
    static AuthRedisConnectorHelper()
    {
        lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            return ConnectionMultiplexer.Connect("localhost");
        });
    }

    private static readonly Lazy<ConnectionMultiplexer> lazyConnection;

    public static ConnectionMultiplexer Connection => lazyConnection.Value;
}
